using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CTP.Application.Interfaces.Services;
using CTP.Application.Helpers;
using CTP.Web.Models;

namespace CTP.Web.Controllers
{
    [Authorize] // متاح لجميع الأدوار
    public class ChangeBoardController : Controller
    {
        private readonly IMonthlyReportService _reportService;

        public ChangeBoardController(IMonthlyReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["EntityTitle"] = "لوحة التغييرات الجارية";
            ViewData["EntityHeaderSubtitle"] = "استعراض كافة التغييرات والمبادرات المعتمدة داخل وحدتك";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["EntityHeaderIcon"] = "bi-kanban";

            var orgIdStr = User.FindFirst("OrganizationId")?.Value;
            if (!int.TryParse(orgIdStr, out int orgId))
            {
                TempData["Error"] = "لم يتم التعرف على الجهة الخاصة بك.";
                return RedirectToAction("Index", "Home");
            }

            var reports = await _reportService.GetUnitActiveChangesAsync(orgId);

            var viewModel = reports.Select(r => new ChangeCardViewModel
            {
                Id = r.Id,
                ChangeName = r.ChangeName,
                ChangeType = r.ChangeType,
                CurrentStage = r.CurrentStage,
                ChangeSummary = r.ChangeSummary ?? "لا يوجد ملخص متاح.",
                ReadinessScore = r.ReadinessScore,
                AmbassadorName = r.Preparer?.FullName ?? "غير محدد",
                ApprovedDateHijri = r.ApprovedDate.ToHijri()
            }).ToList();

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["EntityTitle"] = "التغييرات الجارية";
            ViewData["EntityHeaderSubtitle"] = "بطاقات واضحة تجيب عن الأسئلة الأساسية لكل تغيير جارٍ في المنظومة.";
            ViewData["ThemeColor"] = "#0B4F61";
            ViewData["EntityHeaderIcon"] = "bi-layout-text-window-reverse";

            var orgIdStr = User.FindFirst("OrganizationId")?.Value;
            if (!int.TryParse(orgIdStr, out int orgId)) return RedirectToAction("Index", "Home");

            var report = await _reportService.GetReportByIdAsync(id);

            // حماية: التأكد من أن التقرير معتمد ويتبع لجهة الموظف
            if (report == null || report.OrganizationEntityId != orgId || report.Status != CTP.Domain.Enums.ReportStatus.Approved)
            {
                TempData["Error"] = "البطاقة غير متاحة أو لا تملك صلاحية الوصول إليها.";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ChangeCardDetailsViewModel
            {
                Id = report.Id,
                ChangeName = report.ChangeName,
                ChangeType = report.ChangeType,
                ChangeSummary = string.IsNullOrWhiteSpace(report.ChangeSummary) ? "لا توجد تفاصيل إضافية." : report.ChangeSummary,
                WhyImportant = string.IsNullOrWhiteSpace(report.WhyImportant) ? "-" : report.WhyImportant,
                WhatWillChange = string.IsNullOrWhiteSpace(report.WhatWillChange) ? "-" : report.WhatWillChange,
                WhatWillNotChange = string.IsNullOrWhiteSpace(report.WhatWillNotChange) ? "-" : report.WhatWillNotChange,
                AffectedGroups = string.IsNullOrWhiteSpace(report.AffectedGroups) ? "كافة المنسوبين" : report.AffectedGroups,
                ExecutedActivities = string.IsNullOrWhiteSpace(report.ExecutedActivities) ? "الاطلاع على الأدلة الإرشادية" : report.ExecutedActivities,
                AmbassadorName = report.Preparer?.FullName ?? "إدارة التغيير"
            };

            return View(viewModel);
        }
    }
}