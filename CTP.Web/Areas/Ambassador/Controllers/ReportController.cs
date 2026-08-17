using System.Security.Claims;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;
using CTP.Domain.Enums;
using CTP.Web.Areas.Ambassador.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Areas.Ambassador.Controllers
{
    [Area("Ambassador")]
    [Authorize(Roles = "AMBASSADOR")]
    public class ReportController : Controller
    {
        private readonly IMonthlyReportService _reportService;

        public ReportController(IMonthlyReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["EntityTitle"] = "التقرير الشهري";
            ViewData["EntityHeaderSubtitle"] = "نموذج التقرير الشهري الموحد للتغيير والتحول";
            ViewData["ThemeColor"] = "#C9A227"; // اللون الذهبي للسفراء
            ViewData["EntityHeaderIcon"] = "bi-file-earmark-text";

            return View(new CreateMonthlyReportViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMonthlyReportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "يرجى التأكد من تعبئة الحقول الإلزامية.";
                return View(model);
            }

            // استخراج هوية المستخدم وجهته من الجلسة الموثوقة (Claims)
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orgIdStr = User.FindFirst("OrganizationId")?.Value;

            if (!int.TryParse(userIdStr, out int preparerId) || !int.TryParse(orgIdStr, out int orgId))
            {
                TempData["Error"] = "حدث خطأ في قراءة بيانات المستخدم والجهة. يرجى إعادة تسجيل الدخول.";
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            // الحساب التلقائي لمعادلات التبني والجاهزية
            int readiness = (model.AdkarAwareness + model.AdkarDesire + model.AdkarKnowledge) / 3;
            int adoption = (model.AdkarAbility + model.AdkarReinforcement) / 2;

            var report = new MonthlyReport
            {
                Year = model.Year,
                Month = model.Month,
                PreparerId = preparerId,
                OrganizationEntityId = orgId,
                ChangeName = model.ChangeName,
                ChangeType = model.ChangeType,
                CurrentStage = model.CurrentStage,
                ChangeSummary = model.ChangeSummary,
                AdkarAwareness = model.AdkarAwareness,
                AdkarDesire = model.AdkarDesire,
                AdkarKnowledge = model.AdkarKnowledge,
                AdkarAbility = model.AdkarAbility,
                AdkarReinforcement = model.AdkarReinforcement,
                ReadinessScore = readiness,
                AdoptionScore = adoption,
                Obstacles = model.Obstacles,
                InitialRecommendation = model.InitialRecommendation,

                // تحديد الحالة بناءً على الزر الذي تم الضغط عليه
                Status = model.ActionType == "Submit" ? ReportStatus.Submitted : ReportStatus.Draft,
                SubmittedDate = model.ActionType == "Submit" ? DateTime.Now : null
            };

            await _reportService.CreateReportAsync(report);

            TempData["Success"] = model.ActionType == "Submit"
                ? $"تم رفع التقرير بنجاح برقم: {report.ReportNumber}"
                : $"تم حفظ المسودة بنجاح برقم: {report.ReportNumber}";

            // توجيه مبدئي لنفس الصفحة (لحين بناء شاشة عرض تقارير السفير)
            return RedirectToAction(nameof(Create));
        }
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["EntityTitle"] = "تقاريري السابقة";
            ViewData["EntityHeaderSubtitle"] = "سجل التقارير الشهرية ومتابعة الاعتمادات";
            ViewData["ThemeColor"] = "#C9A227";
            ViewData["EntityHeaderIcon"] = "bi-clock-history";

            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out int preparerId)) return RedirectToAction("Login", "Account", new { area = "" });

            var reports = await _reportService.GetAmbassadorReportsAsync(preparerId);

            // نظام الإشعارات الذكي (مبني على حالة التقارير)
            ViewBag.ReturnedReportsCount = reports.Count(r => r.Status == ReportStatus.Returned);
            ViewBag.ApprovedReportsCount = reports.Count(r => r.Status == ReportStatus.Approved);

            var viewModel = reports.Select(r => new AmbassadorReportListViewModel
            {
                Id = r.Id,
                ReportNumber = r.ReportNumber,
                MonthYear = $"{r.Month} {r.Year}",
                ChangeName = r.ChangeName,
                CreatedDateFormatted = r.CreatedDate.ToString("yyyy/MM/dd"),
                IsDraft = r.Status == ReportStatus.Draft || r.Status == ReportStatus.Returned, // المعاد يعامل معاملة المسودة ليتمكن من تعديله
                StatusName = GetStatusName(r.Status),
                StatusBadgeClass = GetStatusBadgeClass(r.Status)
            }).ToList();

            return View(viewModel);
        }
        // دوال مساعدة لترجمة الـ Enum إلى نصوص وألوان الواجهة (توضع داخل الكنترولر)
        private string GetStatusName(ReportStatus status) => status switch
        {
            ReportStatus.Draft => "مسودة",
            ReportStatus.Submitted => "مرفوع",
            ReportStatus.UnderAnalysis => "تحت التحليل",
            ReportStatus.Returned => "معاد للاستكمال",
            ReportStatus.Approved => "معتمد",
            _ => "غير معروف"
        };

        private string GetStatusBadgeClass(ReportStatus status) => status switch
        {
            ReportStatus.Draft => "moda-badge-secondary",
            ReportStatus.Submitted => "moda-badge-info",
            ReportStatus.UnderAnalysis => "moda-badge-gold",
            ReportStatus.Returned => "moda-badge-danger",
            ReportStatus.Approved => "moda-badge-success",
            _ => "moda-badge-secondary"
        };

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // التحقق من الملكية
            if (report == null || report.PreparerId.ToString() != userIdStr) return NotFound();

            ViewData["EntityTitle"] = "تفاصيل التقرير";
            ViewData["EntityHeaderSubtitle"] = $"تقرير رقم {report.ReportNumber}";
            ViewData["ThemeColor"] = "#C9A227";

            return View(report); // سنمرر الـ Entity مباشرة هنا للسرعة، أو استخدم ViewModel
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (report == null || report.PreparerId.ToString() != userIdStr ||
               (report.Status != ReportStatus.Draft && report.Status != ReportStatus.Returned))
            {
                TempData["Error"] = "لا يمكن تعديل هذا التقرير لأنه قيد الإجراء أو معتمد.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["EntityTitle"] = "تعديل واستكمال التقرير";
            ViewData["EntityHeaderSubtitle"] = $"تحديث التقرير رقم {report.ReportNumber}";
            ViewData["ThemeColor"] = "#C9A227";

            var vm = new EditMonthlyReportViewModel
            {
                Id = report.Id,
                ReportNumber = report.ReportNumber,
                Year = report.Year,
                Month = report.Month,
                ChangeName = report.ChangeName,
                ChangeType = report.ChangeType,
                CurrentStage = report.CurrentStage,
                ChangeSummary = report.ChangeSummary,
                AdkarAwareness = report.AdkarAwareness,
                AdkarDesire = report.AdkarDesire,
                AdkarKnowledge = report.AdkarKnowledge,
                AdkarAbility = report.AdkarAbility,
                AdkarReinforcement = report.AdkarReinforcement,
                Obstacles = report.Obstacles,
                InitialRecommendation = report.InitialRecommendation,
                ApproverNotes = report.ApproverNotes
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditMonthlyReportViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var report = await _reportService.GetReportByIdAsync(model.Id);
            if (report == null) return NotFound();

            // تحديث البيانات
            report.Year = model.Year;
            report.Month = model.Month;
            report.ChangeName = model.ChangeName;
            report.ChangeType = model.ChangeType;
            report.CurrentStage = model.CurrentStage;
            report.ChangeSummary = model.ChangeSummary;
            report.AdkarAwareness = model.AdkarAwareness;
            report.AdkarDesire = model.AdkarDesire;
            report.AdkarKnowledge = model.AdkarKnowledge;
            report.AdkarAbility = model.AdkarAbility;
            report.AdkarReinforcement = model.AdkarReinforcement;
            report.Obstacles = model.Obstacles;
            report.InitialRecommendation = model.InitialRecommendation;

            int readiness = (model.AdkarAwareness + model.AdkarDesire + model.AdkarKnowledge) / 3;
            int adoption = (model.AdkarAbility + model.AdkarReinforcement) / 2;
            report.ReadinessScore = readiness;
            report.AdoptionScore = adoption;

            report.Status = model.ActionType == "Submit" ? ReportStatus.Submitted : report.Status;

            await _reportService.UpdateReportAsync(report);

            TempData["Success"] = "تم تحديث التقرير وإرساله بنجاح.";
            return RedirectToAction(nameof(Index));
        }
    }
}