using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;
using CTP.Web.Areas.Committee.Models;

namespace CTP.Web.Areas.Committee.Controllers
{
    [Area("Committee")]
    [Authorize(Roles = "COMMITTEE_CHAIR,COMMITTEE_MEMBER")]
    public class WorkspaceController : Controller
    {
        private readonly IMonthlyReportService _reportService;
        private readonly IRecommendationService _recommendationService;
        private readonly IEntityInputService _inputService;

        public WorkspaceController(IMonthlyReportService reportService, IRecommendationService recommendationService, IEntityInputService inputService)
        {
            _reportService = reportService;
            _recommendationService = recommendationService;
            _inputService = inputService;
        }

        // 1. شاشة صندوق الوارد
        #region مسار العضو (التحليل)
        [HttpGet]
        public async Task<IActionResult> Inbox()
        {
            ViewData["EntityTitle"] = "صندوق الوارد (التحليل)";
            ViewData["EntityHeaderSubtitle"] = "التقارير التي بانتظار التحليل والتوصيات";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["EntityHeaderIcon"] = "bi-inboxes";

            var allReports = await _reportService.GetCommitteeInboxReportsAsync();
            var allRecs = await _recommendationService.GetCommitteeRecommendationsAsync();

            // ═══════════════════════════════════════════════════════
            // 1. استبعاد التقارير التي لها توصية "قيد الانتظار" أو "معتمدة"
            //    (المرفوضة تعود للـ Inbox)
            // ═══════════════════════════════════════════════════════
            var reportsWithActiveRecs = allRecs
                .Where(r => r.Status == CTP.Domain.Enums.RecommendationStatus.ReadyForChair
                         || r.Status == CTP.Domain.Enums.RecommendationStatus.AwaitingSupport
                         || r.Status == CTP.Domain.Enums.RecommendationStatus.Approved)
                .Select(r => r.MonthlyReportId)
                .Distinct()
                .ToHashSet();

            var inboxReports = allReports
                .Where(r => !reportsWithActiveRecs.Contains(r.Id))
                .ToList();

            // ═══════════════════════════════════════════════════════
            // 2. جمع ملاحظات الرفض لكل تقرير (لعرضها في الـ Inbox)
            // ═══════════════════════════════════════════════════════
            var rejectionNotes = allRecs
                .Where(r => r.Status == CTP.Domain.Enums.RecommendationStatus.Rejected)
                .GroupBy(r => r.MonthlyReportId)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderByDescending(r => r.ReviewedDate ?? r.CreatedDate)
                          .First().ChairReviewNotes);

            ViewBag.RejectionNotes = rejectionNotes;

            return View(inboxReports);
        }
        #endregion
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

            // توليد التوصية بحالة "جاهزة لاعتماد رئيس اللجنة" مع بقاء التقرير UnderAnalysis
            string recNum = await _recommendationService.ProcessAndSaveAnalysisAsync(
                model.MonthlyReportId, model.WhyItMatters, model.SuggestedAction,
                model.Owner, model.ExpectedImpact, model.RequiresSupport);

            TempData["Success"] = $"تم إرسال التوصية ({recNum}) بنجاح وهي الآن بانتظار اعتماد رئيس اللجنة.";

            // التوجيه المعماري الصارم: نقل العضو لسجل التوصيات
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
        // 5. شاشة مدخلات الجهات
        [HttpGet]
        public async Task<IActionResult> Inputs()
        {
            ViewData["EntityTitle"] = "مدخلات الجهات (خارج التقرير)";
            ViewData["EntityHeaderSubtitle"] = "قصص النجاح، التحديات، واحتياجات الدعم العاجلة";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["EntityHeaderIcon"] = "bi-chat-left-dots-fill";

            var inputs = await _inputService.GetCommitteeInputsAsync();
            return View(inputs);
        }

        // 6. شاشة تفاصيل ومعالجة المدخل العاجل
        [HttpGet]
        public async Task<IActionResult> InputDetails(int id)
        {
            var input = await _inputService.GetInputByIdAsync(id);

            if (input == null)
            {
                TempData["Error"] = "المدخل غير موجود أو تم حذفه.";
                return RedirectToAction(nameof(Inputs));
            }

            ViewData["EntityTitle"] = "معالجة مدخل الجهة";
            ViewData["EntityHeaderSubtitle"] = $"مراجعة {input.Type} المرفوع من {input.Organization?.EntityName}";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["EntityHeaderIcon"] = "bi-chat-square-text";

            return View(input);
        }

        #region مسار الرئيس (الاعتماد)
        [HttpGet]
        [Authorize(Roles = "COMMITTEE_CHAIR")]
        public async Task<IActionResult> ChairBoard()
        {
            ViewData["EntityTitle"] = "منصة الاعتماد";
            ViewData["EntityHeaderSubtitle"] = "اعتماد التوصيات ثم إدراجها في المخرجات المؤسسية";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["EntityHeaderIcon"] = "bi-shield-check";

            var pendingRecommendations = (await _recommendationService.GetPendingChairApprovalsAsync())
                .OrderByDescending(r => r.CreatedDate)
                .ToList();

            var approvedRecommendations = (await _recommendationService.GetApprovedRecommendationsAsync())
                .OrderByDescending(r => r.ReviewedDate ?? r.CreatedDate)
                .ToList();

            var vm = new ChairBoardViewModel
            {
                PendingRecommendations = pendingRecommendations,
                ApprovedRecommendations = approvedRecommendations
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "COMMITTEE_CHAIR")]
        public async Task<IActionResult> UpdateRecommendationPlacement(int reportId, string placement, bool enabled)
        {
            var updated = await _recommendationService.UpdateRecommendationPlacementAsync(reportId, placement, enabled);
            if (updated)
            {
                TempData["Success"] = enabled ? "تم إدراج التوصية بنجاح." : "تمت إزالة التوصية من الوجهة المحددة.";
            }
            else
            {
                TempData["Error"] = "تعذر تحديث حالة الإدراج للتوصية.";
            }

            return RedirectToAction(nameof(ChairBoard));
        }

        [HttpGet]
        [Authorize(Roles = "COMMITTEE_CHAIR")]
        public async Task<IActionResult> ChairReview(int reportId)
        {
            var draftRec = await _recommendationService.GetRecommendationByReportIdAsync(reportId);

            if (draftRec == null || draftRec.SourceReport == null || draftRec.SourceReport.Status != CTP.Domain.Enums.ReportStatus.UnderAnalysis)
            {
                TempData["Error"] = "التقرير أو التوصية غير متاحين للمراجعة حالياً.";
                return RedirectToAction(nameof(ChairBoard));
            }

            draftRec.IncludeInInstitutionalReport = true;
            draftRec.IncludeInImpactDashboard = true;
            draftRec.IncludeInExecutiveSummary = true;

            ViewData["EntityTitle"] = "مراجعة واعتماد التوصية";
            ViewData["EntityHeaderSubtitle"] = $"التقرير رقم {draftRec.SourceReport.ReportNumber}";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["EntityHeaderIcon"] = "bi-file-earmark-check";

            return View(draftRec);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "COMMITTEE_CHAIR")]
        public async Task<IActionResult> ApproveRecommendation(
            int reportId,
            bool includeInInstitutionalReport,
            bool includeInImpactDashboard,
            bool includeInExecutiveSummary,
            string? chairReviewNotes)
        {
            var draftRec = await _recommendationService.GetRecommendationByReportIdAsync(reportId);
            if (draftRec?.SourceReport == null)
            {
                TempData["Error"] = "تعذر العثور على التوصية المراد اعتمادها.";
                return RedirectToAction(nameof(ChairBoard));
            }

            var reportApproved = await _reportService.ApproveByCommitteeAsync(reportId, draftRec.SuggestedAction);
            var recommendationApproved = await _recommendationService.ApproveForChairAsync(
                reportId,
                includeInInstitutionalReport,
                includeInImpactDashboard,
                includeInExecutiveSummary,
                chairReviewNotes);

            if (reportApproved && recommendationApproved)
            {
                TempData["Success"] = $"تم اعتماد التوصية المرتبطة بالتقرير {draftRec.SourceReport.ReportNumber} بنجاح.";
                return RedirectToAction(nameof(ChairBoard));
            }

            TempData["Error"] = "لم يتمكن النظام من اعتماد التوصية. حاول مرة أخرى.";
            return RedirectToAction(nameof(ChairReview), new { reportId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "COMMITTEE_CHAIR")]
        public async Task<IActionResult> RejectRecommendation(int reportId, string? chairReviewNotes)
        {
            var draftRec = await _recommendationService.GetRecommendationByReportIdAsync(reportId);
            if (draftRec?.SourceReport == null)
            {
                TempData["Error"] = "تعذر العثور على التوصية المراد إرجاعها.";
                return RedirectToAction(nameof(ChairBoard));
            }

            var rejected = await _recommendationService.RejectChairRecommendationAsync(reportId, chairReviewNotes);
            if (rejected)
            {
                TempData["Success"] = $"تم إرجاع التوصية المرتبطة بالتقرير {draftRec.SourceReport.ReportNumber} للمراجعة.";
                return RedirectToAction(nameof(ChairBoard));
            }

            TempData["Error"] = "لم يتمكن النظام من إرجاع التوصية. حاول مرة أخرى.";
            return RedirectToAction(nameof(ChairReview), new { reportId });
        }
        #endregion
    }
}