using CTP.Application.Interfaces.Services;
using CTP.Domain.Constants;
using CTP.Domain.Enums;
using CTP.Web.Areas.Committee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Areas.Committee.Controllers
{
    [Area("Committee")]
    [Authorize(Roles = AppRoles.CommitteeChair)]
    public class ImpactController : Controller
    {
        private readonly IImpactMeasurementService _impactService;
        private readonly IRecommendationService _recommendationService;

        public ImpactController(
            IImpactMeasurementService impactService,
            IRecommendationService recommendationService)
        {
            _impactService = impactService;
            _recommendationService = recommendationService;
        }

        // ═══════════════════════════════════════════════════════
        // اللوحة الرئيسية
        // ═══════════════════════════════════════════════════════
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["EntityTitle"] = "لوحة الأثر المتحقق";
            ViewData["EntityHeaderSubtitle"] = "مقارنة قبل/بعد للتوصيات المعتمدة — الأثر القابل للقياس";
            ViewData["EntityHeaderIcon"] = "bi-graph-up-arrow";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["Title"] = "لوحة الأثر";

            var summary = await _impactService.GetSummaryAsync();
            var allRecs = await _recommendationService.GetCommitteeRecommendationsAsync();

            // التوصيات المعتمدة فقط + المُدرجة في لوحة الأثر
            var approvedRecs = allRecs
                .Where(r => r.Status == RecommendationStatus.Approved && r.IncludeInImpactDashboard)
                .ToList();

            var measurementsDict = summary.AllMeasurements
                .ToDictionary(m => m.RecommendationId);

            var rows = new List<ImpactRow>();

            foreach (var rec in approvedRecs)
            {
                var hasMeasurement = measurementsDict.TryGetValue(rec.Id, out var measurement);

                rows.Add(new ImpactRow
                {
                    RecommendationId = rec.Id,
                    RecommendationNumber = rec.RecommendationNumber,
                    EntityName = rec.SourceReport?.Organization?.EntityName ?? "—",
                    ReportNumber = rec.SourceReport?.ReportNumber ?? "—",
                    SuggestedAction = rec.SuggestedAction,
                    SuccessIndicator = rec.SuccessIndicator,
                    HasMeasurement = hasMeasurement,
                    IndicatorName = measurement?.IndicatorName ?? rec.SuccessIndicator,
                    BeforeValue = measurement?.BeforeValue ?? 0,
                    AfterValue = measurement?.AfterValue ?? 0,
                    Delta = measurement?.Delta ?? 0,
                    Status = measurement?.Status ?? "بانتظار القياس",
                    StatusBadgeClass = GetStatusBadge(measurement?.Status),
                    ImprovementReason = measurement?.ImprovementReason,
                    NextStep = measurement?.NextStep,
                    Lesson = measurement?.Lesson,
                    MeasuredDateFormatted = measurement?.MeasuredDate?.ToString("yyyy/MM/dd")
                });
            }

            summary.PendingCount = rows.Count(r => !r.HasMeasurement);

            var vm = new ImpactDashboardViewModel
            {
                Summary = summary,
                Rows = rows.OrderBy(r => r.HasMeasurement).ThenByDescending(r => r.Delta).ToList()
            };

            return View(vm);
        }

        // ═══════════════════════════════════════════════════════
        // إدخال/تعديل القياس
        // ═══════════════════════════════════════════════════════
        [HttpGet]
        public async Task<IActionResult> Measure(int recommendationId)
        {
            var allRecs = await _recommendationService.GetCommitteeRecommendationsAsync();
            var rec = allRecs.FirstOrDefault(r => r.Id == recommendationId);

            if (rec == null || rec.Status != RecommendationStatus.Approved)
            {
                TempData["Error"] = "التوصية غير متاحة للقياس.";
                return RedirectToAction(nameof(Index));
            }

            var existing = await _impactService.GetByRecommendationIdAsync(recommendationId);

            ViewData["EntityTitle"] = "قياس أثر التوصية";
            ViewData["EntityHeaderSubtitle"] = $"التوصية {rec.RecommendationNumber} — {rec.SourceReport?.Organization?.EntityName}";
            ViewData["EntityHeaderIcon"] = "bi-bar-chart-line";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["Title"] = "قياس الأثر";

            ViewBag.Recommendation = rec;
            ViewBag.Existing = existing;

            return View(existing ?? new CTP.Domain.Entities.ImpactMeasurement
            {
                RecommendationId = recommendationId,
                IndicatorName = rec.SuccessIndicator
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Measure(
            int recommendationId,
            string indicatorName,
            int beforeValue,
            int afterValue,
            string? improvementReason,
            string? nextStep,
            string? lesson)
        {
            if (string.IsNullOrWhiteSpace(indicatorName))
            {
                TempData["Error"] = "اسم المؤشر مطلوب.";
                return RedirectToAction(nameof(Measure), new { recommendationId });
            }

            // نطاق 0-100
            beforeValue = Math.Max(0, Math.Min(100, beforeValue));
            afterValue = Math.Max(0, Math.Min(100, afterValue));

            var measurement = new CTP.Domain.Entities.ImpactMeasurement
            {
                RecommendationId = recommendationId,
                IndicatorName = indicatorName,
                BeforeValue = beforeValue,
                AfterValue = afterValue,
                ImprovementReason = improvementReason,
                NextStep = nextStep,
                Lesson = lesson,
                MeasuredDate = afterValue > 0 ? DateTime.Now : null
            };

            await _impactService.SaveAsync(measurement);

            TempData["Success"] = "تم حفظ قياس الأثر بنجاح.";
            return RedirectToAction(nameof(Index));
        }

        // ═══════════════════════════════════════════════════════
        // Helpers
        // ═══════════════════════════════════════════════════════
        private static string GetStatusBadge(string? status) => status switch
        {
            "تحقق" => "bg-success",
            "جزئي" => "bg-warning text-dark",
            "لم يتحقق بعد" => "bg-danger",
            _ => "bg-secondary"
        };
    }
}