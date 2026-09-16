using CTP.Application.Helpers;
using CTP.Application.Interfaces.Services;
using CTP.Application.Services;
using CTP.Domain.Constants;
using CTP.Domain.Entities;
using CTP.Web.Areas.UnitLeader.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CTP.Web.Areas.UnitLeader.Controllers
{

    [Area("UnitLeader")]
    [Authorize(Roles = AppRoles.UnitLeader)]
    public class DashboardController : Controller
    {
        private readonly IMonthlyReportService _reportService;
        private readonly INotificationService _notificationService; // تم إضافة خدمة الإشعارات هنا

        public DashboardController(
            IMonthlyReportService reportService,
            INotificationService notificationService)
        {
            _reportService = reportService;
            _notificationService = notificationService;
            
        }    

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["EntityTitle"] = "مساحة قائد الوحدة / الإدارة";
            ViewData["EntityHeaderSubtitle"] = "موجز الموقف واعتماد التقارير الشهرية الواردة من سفراء التغيير";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["EntityHeaderIcon"] = "bi-building-check";

            var orgIdStr = User.FindFirst("OrganizationId")?.Value;
            if (!int.TryParse(orgIdStr, out int orgId)) return RedirectToAction("Login", "Account", new { area = "" });

            var reports = await _reportService.GetPendingReportsForLeaderAsync(orgId);

            var viewModel = reports.Select(r => new PendingReportViewModel
            {
                Id = r.Id,
                ReportNumber = r.ReportNumber,
                MonthYear = $"{r.Month} {r.Year}",
                PreparerName = r.Preparer?.FullName ?? "غير محدد",
                ChangeName = r.ChangeName,
                ReadinessScore = r.ReadinessScore,
                SubmittedDate = r.SubmittedDate.ToHijri()
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(int id, bool isApproved, string? notes)
        {
            // 1. جلب التقرير أولاً (مشترك بين الكودين)
            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null) return NotFound();

            // 2. فحص الجودة عند الاعتماد (من الصورة الأولى)
           

            // 3. تنفيذ عملية المراجعة والحفظ (من الصورة الثانية)
            var success = await _reportService.ReviewReportAsync(id, isApproved, notes);

            if (success)
            {
                TempData["Success"] = isApproved ? "تم اعتماد التقرير وإحالته للجنة بنجاح" : "تم إعادة التقرير للاستكمال بنجاح";

                // إرسال الإشعار الآلي للسفير بناءً على القرار (من الصورة الثانية)
                string notifTitle = isApproved ? "اعتماد تقرير" : "إعادة تقرير للاستكمال";
                string notifMessage = isApproved
                    ? $"تم اعتماد تقرير التغيير {report.ChangeName} من قبل القيادة"
                    : $"تم إعادة تقرير {report.ChangeName} لوجود ملاحظات: {notes ?? "لا توجد ملاحظات"}";

                string icon = isApproved ? "bi-check-circle-fill" : "bi-exclamation-triangle-fill";
                string color = isApproved ? "text-success" : "text-danger";

                await _notificationService.CreateNotificationAsync(
                    userId: report.PreparerId,
                    title: notifTitle,
                    message: notifMessage,
                    url: "/Ambassador/Report/Index",
                    iconClass: icon,
                    colorClass: color
                );

                return RedirectToAction(nameof(Details), new { id }); // أو التوجيه المناسب لديك
            }
            else
            {
                TempData["Error"] = "حدث خطأ أثناء معالجة التقرير.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var orgIdStr = User.FindFirst("OrganizationId")?.Value;
            if (!int.TryParse(orgIdStr, out int orgId)) return RedirectToAction("Login", "Account", new { area = "" });

            var report = await _reportService.GetReportByIdAsync(id);

            if (report == null || report.OrganizationEntityId != orgId || report.Status != CTP.Domain.Enums.ReportStatus.Submitted)
            {
                TempData["Error"] = "التقرير غير متاح أو لا تملك صلاحية الوصول إليه.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["EntityTitle"] = "مراجعة تقرير التغيير";
            ViewData["EntityHeaderSubtitle"] = $"مراجعة تفاصيل التقرير رقم {report.ReportNumber}";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["EntityHeaderIcon"] = "bi-search";

            var viewModel = new ReportDetailsViewModel
            {
                Id = report.Id,
                ReportNumber = report.ReportNumber,
                PreparerName = report.Preparer?.FullName ?? "غير محدد",
                MonthYear = $"{report.Month} {report.Year}",
                ChangeName = report.ChangeName,
                ChangeType = report.ChangeType,
                CurrentStage = report.CurrentStage,
                ChangeSummary = report.ChangeSummary,
                ReadinessScore = report.ReadinessScore,
                AdoptionScore = report.AdoptionScore,
                AdkarAwareness = report.AdkarAwareness,
                AdkarDesire = report.AdkarDesire,
                AdkarKnowledge = report.AdkarKnowledge,
                AdkarAbility = report.AdkarAbility,
                AdkarReinforcement = report.AdkarReinforcement,
                Obstacles = report.Obstacles,
                InitialRecommendation = report.InitialRecommendation,
                CommitteeRecommendation = report.CommitteeRecommendation,
                EvidenceLinks = report.EvidenceLinks
            };

            return View(viewModel);
        }
    }
}