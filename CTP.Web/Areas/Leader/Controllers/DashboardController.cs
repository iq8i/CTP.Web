using CTP.Application.Interfaces.Services;
using CTP.Domain.Constants;
using CTP.Domain.Enums;
using CTP.Web.Areas.Leader.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Areas.Leader.Controllers
{
    [Area("Leader")]
    [Authorize(Roles = AppRoles.Leader)]
    public class DashboardController : Controller
    {
        private readonly IMonthlyReportService _reportService;
        private readonly IRecommendationService _recommendationService;
        private readonly IImpactMeasurementService _impactService;

        public DashboardController(
            IMonthlyReportService reportService,
            IRecommendationService recommendationService,
            IImpactMeasurementService impactService)
        {
            _reportService = reportService;
            _recommendationService = recommendationService;
            _impactService = impactService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["EntityTitle"] = "الموجز الاستراتيجي";
            ViewData["EntityHeaderSubtitle"] = "نظرة معالي القائد على برنامج التغيير والتحول";
            ViewData["EntityHeaderIcon"] = "bi-award";
            ViewData["ThemeColor"] = "#073B49";
            ViewData["Title"] = "الموجز الاستراتيجي";

            var vm = await BuildViewModelAsync();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Impact()
        {
            ViewData["EntityTitle"] = "الأثر المتحقق";
            ViewData["EntityHeaderSubtitle"] = "قياس الأثر على مستوى المنظومة";
            ViewData["EntityHeaderIcon"] = "bi-graph-up-arrow";
            ViewData["ThemeColor"] = "#073B49";
            ViewData["Title"] = "الأثر";

            var summary = await _impactService.GetSummaryAsync();
            return View(summary);
        }

        private async Task<LeaderDashboardViewModel> BuildViewModelAsync()
        {
            var allReports = (await _reportService.GetCommitteeInboxReportsAsync()).ToList();
            var allRecs = (await _recommendationService.GetCommitteeRecommendationsAsync()).ToList();
            var impactSummary = await _impactService.GetSummaryAsync();

            // ═══ KPIs ═══
            var vm = new LeaderDashboardViewModel
            {
                TotalReports = allReports.Count,
                ApprovedReports = allReports.Count(r => r.Status == ReportStatus.Approved),
                AvgReadiness = allReports.Any() ? Math.Round(allReports.Average(r => (double)r.ReadinessScore), 0) : 0,
                AvgAdoption = allReports.Any() ? Math.Round(allReports.Average(r => (double)r.AdoptionScore), 0) : 0,
                TotalRecommendations = allRecs.Count,
                ApprovedRecommendations = allRecs.Count(r => r.Status == RecommendationStatus.Approved),

                // ═══ حالة البرنامج ═══
                CurrentPhase = "مراجعة المادة العلمية واعتمادها",
                RoadmapProgressPercent = 38,
                RoadmapStatus = "البرنامج يسير وفق خارطة الطريق المعتمدة",

                // ═══ الملخص التنفيذي المعتمد ═══
                ExecutiveSummary = BuildExecutiveSummary(allReports, allRecs, impactSummary),

                // ═══ قرارات الدعم المطلوبة ═══
                SupportDecisions = allRecs
    .Where(r => r.Status == RecommendationStatus.EscalatedToLeader
             || (r.Status == RecommendationStatus.Approved
                 && r.Escalation != null
                 && r.Escalation.Contains("القائد")))
                    .Take(4)
                    .Select(r => new SupportDecisionItem
                    {
                        Title = r.SuggestedAction,
                        Entity = r.SourceReport?.Organization?.EntityName ?? "—",
                        RequestedBy = r.ActionOwner
                    })
                    .ToList(),

                // ═══ المخاطر الحرجة ═══
                CriticalRisks = allReports
                    .Where(r => !string.IsNullOrWhiteSpace(r.Risks)
                             && r.Risks != "—"
                             && r.Risks.Length > 10
                             && r.Status == ReportStatus.Approved)
                    .Take(3)
                    .Select(r => new RiskItem
                    {
                        Title = r.Risks,
                        Entity = r.Organization?.EntityName ?? "—",
                        Level = r.AdoptionScore < 50 ? "مرتفع" : "متوسط",
                        BadgeClass = r.AdoptionScore < 50 ? "bg-danger" : "bg-warning text-dark"
                    })
                    .ToList(),

                // ═══ الأثر ═══
                ImpactVerifiedCount = impactSummary.VerifiedCount,
                ImpactPartialCount = impactSummary.PartialCount,
                AvgImpactDelta = impactSummary.AvgDelta,
                TopImpacts = impactSummary.AllMeasurements
                    .OrderByDescending(m => m.Delta)
                    .Take(4)
                    .Select(m => new ImpactPreviewItem
                    {
                        IndicatorName = m.IndicatorName,
                        Before = m.BeforeValue,
                        After = m.AfterValue,
                        Delta = m.Delta,
                        Status = m.Status
                    })
                    .ToList()
            };

            // ═══ آخر النشاطات ═══
            vm.RecentActivities = BuildRecentActivities(allReports, allRecs);

            return vm;
        }

        private static string BuildExecutiveSummary(
            List<CTP.Domain.Entities.MonthlyReport> reports,
            List<CTP.Domain.Entities.Recommendation> recs,
            ImpactSummaryResult impact)
        {
            if (!reports.Any())
                return "لم تُستقبل تقارير بعد. البرنامج في مرحلة التأسيس.";

            var approvedCount = reports.Count(r => r.Status == ReportStatus.Approved);
            var avgReadiness = Math.Round(reports.Average(r => (double)r.ReadinessScore), 0);
            var avgAdoption = Math.Round(reports.Average(r => (double)r.AdoptionScore), 0);
            var approvedRecs = recs.Count(r => r.Status == RecommendationStatus.Approved);

            return $@"استقبلت لجنة شركاء التغيير خلال الفترة المنقضية {reports.Count} تقريراً من مختلف الوحدات والهيئات والإدارات، اعتُمد منها {approvedCount}.
بلغ متوسط الجاهزية العامة {avgReadiness}% والتبني العام {avgAdoption}% على مستوى المنظومة.
اعتمدت اللجنة {approvedRecs} توصية رئيسية جاهزة للتنفيذ، وقد تم قياس الأثر على {impact.TotalMeasurements} مؤشر بمتوسط تحسن +{impact.AvgDelta} نقطة.
البرنامج يسير وفق خارطة الطريق المعتمدة، والمرحلة الحالية هي مراجعة المادة العلمية واعتمادها.";
        }

        private static List<ActivityItem> BuildRecentActivities(
            List<CTP.Domain.Entities.MonthlyReport> reports,
            List<CTP.Domain.Entities.Recommendation> recs)
        {
            var activities = new List<ActivityItem>();

            // أحدث التقارير المعتمدة
            foreach (var report in reports
                .Where(r => r.Status == ReportStatus.Approved && r.ApprovedDate.HasValue)
                .OrderByDescending(r => r.ApprovedDate)
                .Take(3))
            {
                activities.Add(new ActivityItem
                {
                    Icon = "bi-check-circle-fill",
                    Color = "#3E7C3E",
                    Title = $"اعتُمد تقرير: {report.ChangeName} — {report.Organization?.EntityName}",
                    TimeAgo = GetTimeAgo(report.ApprovedDate ?? report.CreatedDate)
                });
            }

            // أحدث التوصيات المعتمدة
            foreach (var rec in recs
                .Where(r => r.Status == RecommendationStatus.Approved && r.ReviewedDate.HasValue)
                .OrderByDescending(r => r.ReviewedDate)
                .Take(2))
            {
                var action = rec.SuggestedAction.Length > 60
                    ? rec.SuggestedAction.Substring(0, 60) + "..."
                    : rec.SuggestedAction;

                activities.Add(new ActivityItem
                {
                    Icon = "bi-journal-check",
                    Color = "#A98418",
                    Title = $"اعتُمدت توصية: {action}",
                    TimeAgo = GetTimeAgo(rec.ReviewedDate ?? rec.CreatedDate)
                });
            }

            return activities.Take(5).ToList();
        }

        private static string GetTimeAgo(DateTime date)
        {
            var span = DateTime.Now - date;
            if (span.TotalMinutes < 60) return $"منذ {(int)span.TotalMinutes} دقيقة";
            if (span.TotalHours < 24) return $"منذ {(int)span.TotalHours} ساعة";
            if (span.TotalDays < 30) return $"منذ {(int)span.TotalDays} يوم";
            return date.ToString("yyyy/MM/dd");
        }
    }
}