using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CTP.Application.Interfaces.Services;
using CTP.Web.Areas.Committee.Models;

namespace CTP.Web.Areas.Committee.Controllers
{
    [Area("Committee")]
    [Authorize(Roles = "COMMITTEE_CHAIR,COMMITTEE_MEMBER,LEADER")]
    public class WorkspaceController : Controller
    {
        private readonly IMonthlyReportService _reportService;
        private readonly IRecommendationService _recommendationService;

        public WorkspaceController(IMonthlyReportService reportService, IRecommendationService recommendationService)
        {
            _reportService = reportService;
            _recommendationService = recommendationService;
        }

        // 1. شاشة صندوق الوارد
        [HttpGet]
        public async Task<IActionResult> Inbox()
        {
            ViewData["EntityTitle"] = "صندوق وارد اللجنة";
            ViewData["EntityHeaderSubtitle"] = "التقارير المرفوعة والجاهزة للتحليل";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["EntityHeaderIcon"] = "bi-inbox-fill";

            var reports = await _reportService.GetCommitteeInboxReportsAsync();
            return View(reports);
        }
        // 1.5. شاشة استعراض تفاصيل التقرير
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var report = await _reportService.GetReportByIdAsync(id);

            // التأكد من أن التقرير متاح للجنة
            if (report == null || report.Status != CTP.Domain.Enums.ReportStatus.UnderAnalysis)
            {
                TempData["Error"] = "التقرير غير متاح للمراجعة حالياً.";
                return RedirectToAction(nameof(Inbox));
            }

            ViewData["EntityTitle"] = "النسخة التحليلية للتقرير";
            ViewData["EntityHeaderSubtitle"] = $"استعراض تقرير التغيير رقم {report.ReportNumber}";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["EntityHeaderIcon"] = "bi-file-text";

            return View(report);
        }
        // 2. شاشة أداة التحليل (GET)
        [HttpGet]
        public async Task<IActionResult> Analyze(int id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null || report.Status != CTP.Domain.Enums.ReportStatus.UnderAnalysis)
            {
                TempData["Error"] = "التقرير غير متاح للتحليل.";
                return RedirectToAction(nameof(Inbox));
            }

            ViewData["EntityTitle"] = "أداة التحليل وشجرة القرار";
            ViewData["EntityHeaderSubtitle"] = $"تحليل التقرير: {report.ReportNumber}";
            ViewData["ThemeColor"] = "#0B4F61";

            var vm = new AnalyzeReportViewModel
            {
                MonthlyReportId = report.Id,
                ReportDetails = report
            };

            return View(vm);
        }

        // 3. حفظ التحليل (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Analyze(AnalyzeReportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ReportDetails = await _reportService.GetReportByIdAsync(model.MonthlyReportId) ?? new CTP.Domain.Entities.MonthlyReport();
                return View(model);
            }

            string recNumber = await _recommendationService.ProcessAndSaveAnalysisAsync(
                model.MonthlyReportId,
                model.WhyItMatters,
                model.SuggestedAction,
                model.Owner,
                model.ExpectedImpact,
                model.RequiresSupport
            );

            // اعتماد التقرير لإغلاقه وإخفائه من صندوق الوارد
            await _reportService.ApproveByCommitteeAsync(model.MonthlyReportId, model.SuggestedAction);

            TempData["Success"] = $"تم حفظ التحليل وتوليد التوصية بنجاح برقم {recNumber}.";
            return RedirectToAction(nameof(Recommendations));
        }

        // 4. شاشة سجل التوصيات
        [HttpGet]
        public async Task<IActionResult> Recommendations()
        {
            ViewData["EntityTitle"] = "سجل التوصيات والقرارات";
            ViewData["EntityHeaderSubtitle"] = "قاعدة بيانات التدخلات والتوصيات المعتمدة";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["EntityHeaderIcon"] = "bi-journal-check";

            var recs = await _recommendationService.GetCommitteeRecommendationsAsync();
            return View(recs);
        }
    }
}