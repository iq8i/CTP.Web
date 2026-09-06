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
        private readonly IEntityInputService _inputService;

        public WorkspaceController(IMonthlyReportService reportService, IRecommendationService recommendationService, IEntityInputService inputService )
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
            ViewData["ThemeColor"] = "#0B4F61";

            // جلب التقارير التي بحالة UnderAnalysis
            var allReports = await _reportService.GetCommitteeInboxReportsAsync();
            var allRecs = await _recommendationService.GetCommitteeRecommendationsAsync();

            // الفلتر الصارم والآمن: استخراج أي تقرير له توصية مسجلة مسبقاً (بغض النظر عن حالتها النصية)
            var reportsWithRecs = allRecs
                .Select(r => r.MonthlyReportId)
                .Distinct()
                .ToList();

            // استبعاد التقارير التي لها توصيات
            var inboxReports = allReports.Where(r => !reportsWithRecs.Contains(r.Id)).ToList();

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
        [Authorize(Roles = "COMMITTEE_CHAIR,LEADER")]
        public async Task<IActionResult> ChairBoard()
        {
            ViewData["EntityTitle"] = "منصة الاعتماد القيادي";
            ViewData["ThemeColor"] = "#0B4F61";

            var allRecs = await _recommendationService.GetCommitteeRecommendationsAsync();

            // الفلتر الصارم: اعرض التوصيات التي لا يزال تقريرها قيد التحليل (بمعنى أن الرئيس لم يُغلقه بعد)
            var pendingApprovals = allRecs
                .Where(r => r.SourceReport?.Status == CTP.Domain.Enums.ReportStatus.UnderAnalysis)
                .OrderByDescending(r => r.CreatedDate).ToList();

            return View(pendingApprovals);
        }
        [HttpGet]
        [Authorize(Roles = "COMMITTEE_CHAIR,LEADER")]
        public async Task<IActionResult> ChairReview(int reportId)
        {
            var allRecs = await _recommendationService.GetCommitteeRecommendationsAsync();

            // جلب التوصية بناءً على حالة التقرير بدلاً من مطابقة النص
            var draftRec = allRecs.FirstOrDefault(r => r.MonthlyReportId == reportId && r.SourceReport?.Status == CTP.Domain.Enums.ReportStatus.UnderAnalysis);

            if (draftRec == null) return RedirectToAction(nameof(ChairBoard));

            ViewData["EntityTitle"] = "مراجعة واعتماد التوصية";
            ViewData["ThemeColor"] = "#0B4F61";

            return View(draftRec);
        }
        #endregion
    }
}