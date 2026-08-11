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
                EvidenceLinks = model.EvidenceLinks,

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
    }
}