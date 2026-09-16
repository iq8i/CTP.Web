using CTP.Application.Interfaces.Services;
using CTP.Domain.Constants;
using CTP.Domain.Enums;
using CTP.Web.Areas.Staff.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = AppRoles.Staff)]
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
            ViewData["EntityTitle"] = "مساحة المنسوب";
            ViewData["EntityHeaderSubtitle"] = "هنا تفهم ما يتغير، وماذا يعني لك، وما الدعم المتاح";
            ViewData["EntityHeaderIcon"] = "bi-person-badge";
            ViewData["ThemeColor"] = "#106981";
            ViewData["Title"] = "مساحة المنسوب";

            var vm = await BuildViewModelAsync();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Changes()
        {
            ViewData["EntityTitle"] = "التغييرات الجارية";
            ViewData["EntityHeaderSubtitle"] = "كل التغييرات المعتمدة في المنظومة";
            ViewData["EntityHeaderIcon"] = "bi-kanban";
            ViewData["ThemeColor"] = "#106981";
            ViewData["Title"] = "التغييرات الجارية";

            var vm = await BuildViewModelAsync();
            return View(vm);
        }

        private async Task<StaffDashboardViewModel> BuildViewModelAsync()
        {
            var allReports = (await _reportService.GetCommitteeInboxReportsAsync()).ToList();
            var approvedReports = allReports.Where(r => r.Status == ReportStatus.Approved).ToList();

            var vm = new StaffDashboardViewModel
            {
                ActiveChangesCount = approvedReports.Count,
                EducationalMaterialsCount = 3,
                InquiryChannelsCount = 2,

                ActiveChanges = approvedReports
                    .OrderByDescending(r => r.ApprovedDate)
                    .Take(6)
                    .Select(r => new ChangeCard
                    {
                        Id = r.Id,
                        ChangeName = r.ChangeName,
                        ChangeType = r.ChangeType,
                        CurrentStage = r.CurrentStage,
                        EntityName = r.Organization?.EntityName ?? "—",
                        Summary = string.IsNullOrWhiteSpace(r.ChangeSummary) ? "لا يوجد ملخص" : r.ChangeSummary,
                        WhatWillChange = string.IsNullOrWhiteSpace(r.WhatWillChange) ? "—" : r.WhatWillChange,
                        WhatWillNotChange = string.IsNullOrWhiteSpace(r.WhatWillNotChange) ? "—" : r.WhatWillNotChange,
                        AmbassadorName = r.Preparer?.FullName ?? "إدارة التغيير"
                    })
                    .ToList(),

                Materials = new List<MaterialCard>
                {
                    new()
                    {
                        Title = "مادة إدارة التغيير",
                        Description = "المرجع المعرفي المؤسسي لبناء ثقافة التغيير — فصلان.",
                        Icon = "bi-book-fill",
                        Color = "warning",
                        Url = "/Materials/View/ChangeManagement",
                        StatusBadge = "المنتج المؤسسي الأول",
                        StatusClass = "bg-warning text-dark"
                    },
                    new()
                    {
                        Title = "دورة سفراء التغيير",
                        Description = "برنامج تأهيل سفراء التغيير — 6 دروس مبسطة.",
                        Icon = "bi-mortarboard-fill",
                        Color = "primary",
                        Url = "/Materials/AmbassadorCourse",
                        StatusBadge = "بعد اعتماد المادة",
                        StatusClass = "bg-secondary"
                    },
                    new()
                    {
                        Title = "رسائل التوعية",
                        Description = "رسائل دورية موحدة لدعم الفهم والتبني.",
                        Icon = "bi-chat-quote-fill",
                        Color = "info",
                        Url = "#",
                        StatusBadge = "قريباً",
                        StatusClass = "bg-secondary"
                    }
                },

                Channels = new List<ChannelCard>
                {
                    new()
                    {
                        Icon = "bi-person-badge",
                        Title = "سفير التغيير في جهتك",
                        Description = "اسأله عن أي تغيير في نطاق جهتك",
                        Contact = "عبر النظام"
                    },
                    new()
                    {
                        Icon = "bi-envelope",
                        Title = "البريد الداخلي للجنة",
                        Description = "للاستفسارات المتخصصة والأمور غير الاعتيادية",
                        Contact = "ctp@mod.gov.sa"
                    }
                }
            };

            return vm;
        }
    }
}