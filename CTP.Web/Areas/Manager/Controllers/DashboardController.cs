using CTP.Application.Interfaces.Services;
using CTP.Domain.Constants;
using CTP.Domain.Enums;
using CTP.Web.Areas.Manager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = AppRoles.Manager)]
    public class DashboardController : Controller
    {
        private readonly IMonthlyReportService _reportService;

        public DashboardController(IMonthlyReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["EntityTitle"] = "مساحة القادة والمدراء المباشرين";
            ViewData["EntityHeaderSubtitle"] = "دورك الأقرب للمنسوبين — الدعم اليومي خلال التغيير";
            ViewData["EntityHeaderIcon"] = "bi-people";
            ViewData["ThemeColor"] = "#2E86A0";
            ViewData["Title"] = "المدراء المباشرون";

            var allReports = (await _reportService.GetCommitteeInboxReportsAsync()).ToList();
            var approved = allReports.Where(r => r.Status == ReportStatus.Approved).ToList();

            var vm = new ManagerDashboardViewModel
            {
                ActiveChangesCount = approved.Count,
                TeamChanges = approved
                    .OrderByDescending(r => r.ApprovedDate)
                    .Take(6)
                    .Select(r => new CTP.Web.Areas.Staff.Models.ChangeCard
                    {
                        Id = r.Id,
                        ChangeName = r.ChangeName,
                        ChangeType = r.ChangeType,
                        CurrentStage = r.CurrentStage,
                        EntityName = r.Organization?.EntityName ?? "—",
                        Summary = r.ChangeSummary ?? "—",
                        WhatWillChange = r.WhatWillChange ?? "—",
                        WhatWillNotChange = r.WhatWillNotChange ?? "—",
                        AmbassadorName = r.Preparer?.FullName ?? "—"
                    })
                    .ToList(),

                SupportTools = new List<SupportToolItem>
                {
                    new() { Icon = "bi-book-fill", Title = "مادة إدارة التغيير", Description = "المرجع المؤسسي لبناء ثقافة التغيير — فصلان", Url = "/Materials/View/ChangeManagement", Color = "warning" },
                    new() { Icon = "bi-kanban", Title = "التغييرات الجارية", Description = "بطاقات تغيير في نطاق فريقك", Url = "/Staff/Dashboard/Changes", Color = "primary" },
                    new() { Icon = "bi-chat-left-dots", Title = "تواصل مع سفير التغيير", Description = "شارك تحديات فريقك", Url = "#", Color = "info" }
                }
            };

            return View(vm);
        }
    }
}