using CTP.Application.Helpers;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Constants;
using CTP.Domain.Enums;
using CTP.Web.Areas.CorporateComms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Areas.CorporateComms.Controllers
{
    [Area("CorporateComms")]
    [Authorize(Roles = AppRoles.CorporateComms)]
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
            ViewData["EntityTitle"] = "مساحة الاتصال المؤسسي";
            ViewData["EntityHeaderSubtitle"] = "توحيد الرسائل ورفع الوعي والفهم بلغة داعمة";
            ViewData["EntityHeaderIcon"] = "bi-megaphone";
            ViewData["ThemeColor"] = "#C9A227";
            ViewData["Title"] = "الاتصال المؤسسي";

            var vm = await BuildViewModelAsync();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Templates()
        {
            ViewData["EntityTitle"] = "قوالب الرسائل";
            ViewData["EntityHeaderSubtitle"] = "قوالب الاتصال المؤسسي الجاهزة";
            ViewData["EntityHeaderIcon"] = "bi-chat-quote";
            ViewData["ThemeColor"] = "#C9A227";
            ViewData["Title"] = "قوالب الرسائل";

            var vm = await BuildViewModelAsync();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Announcements()
        {
            ViewData["EntityTitle"] = "إعلانات التغيير";
            ViewData["EntityHeaderSubtitle"] = "إعلانات التغييرات الجارية على مستوى المنظومة";
            ViewData["EntityHeaderIcon"] = "bi-megaphone";
            ViewData["ThemeColor"] = "#C9A227";
            ViewData["Title"] = "إعلانات التغيير";

            var vm = await BuildViewModelAsync();
            return View(vm);
        }

        private async Task<CommsDashboardViewModel> BuildViewModelAsync()
        {
            var allReports = (await _reportService.GetCommitteeInboxReportsAsync()).ToList();
            var approvedReports = allReports.Where(r => r.Status == ReportStatus.Approved).ToList();

            // ═══ التغييرات الجارية (المعتمدة) ═══
            var activeChanges = approvedReports
                .OrderByDescending(r => r.ApprovedDate)
                .Take(12)
                .Select(r => new ActiveChangeItem
                {
                    ChangeName = r.ChangeName,
                    ChangeType = r.ChangeType,
                    CurrentStage = r.CurrentStage,
                    EntityName = r.Organization?.EntityName ?? "—",
                    ApprovedDate = r.ApprovedDate.ToHijri()
                })
                .ToList();

            var vm = new CommsDashboardViewModel
            {
                TotalTemplates = 10,
                ActiveChanges = activeChanges.Count,
                PublishedAnnouncements = 4,
                DraftMessages = 2,

                ActiveChangesList = activeChanges,

                // ═══ حزمة القوالب الجاهزة ═══
                Templates = new List<TemplateItem>
                {
                    new() { Icon = "bi-rocket-takeoff", Title = "رسالة التدشين", Description = "رسالة رسمية للإعلان عن تدشين المنصة", Color = "primary", FileName = "01_رسالة_التدشين.docx" },
                    new() { Icon = "bi-bell", Title = "إعلان المستخدمين", Description = "إعلان موجّه للمنسوبين عن الخدمات الجديدة", Color = "info", FileName = "02_إعلان_المستخدمين.docx" },
                    new() { Icon = "bi-person-badge", Title = "رسالة القيادة", Description = "رسالة موجّهة من القيادة لدعم التغيير", Color = "dark", FileName = "03_رسالة_القيادة.docx" },
                    new() { Icon = "bi-question-circle", Title = "الأسئلة الشائعة", Description = "إجابات جاهزة لأكثر الاستفسارات تكراراً", Color = "warning", FileName = "04_الأسئلة_الشائعة.docx" },
                    new() { Icon = "bi-megaphone", Title = "إعلان التغيير", Description = "قالب موحد للإعلان عن تغيير جديد", Color = "success", FileName = "05_إعلان_التغيير.docx" },
                    new() { Icon = "bi-kanban", Title = "دليل محتوى التغييرات الجارية", Description = "إرشادات إعداد محتوى بطاقات التغيير", Color = "secondary", FileName = "06_دليل_المحتوى.docx" },
                    new() { Icon = "bi-chat-dots", Title = "رسائل التوعية", Description = "رسائل دورية لرفع الوعي والفهم", Color = "info", FileName = "07_رسائل_التوعية.docx" },
                    new() { Icon = "bi-people", Title = "دليل تواصل سفراء التغيير", Description = "إرشادات التواصل بين السفراء والمنسوبين", Color = "primary", FileName = "08_دليل_التواصل.docx" },
                    new() { Icon = "bi-diagram-3", Title = "أدوار الاتصال", Description = "توزيع أدوار الاتصال المؤسسي داخل المنظومة", Color = "warning", FileName = "09_أدوار_الاتصال.docx" },
                    new() { Icon = "bi-bookmark-check", Title = "موجز الاتصال", Description = "ملخص استراتيجية الاتصال المؤسسي", Color = "success", FileName = "10_موجز_الاتصال.docx" }
                },

                // ═══ الإعلانات المنشورة ═══
                PublishedAnnouncementsList = new List<AnnouncementItem>
                {
                    new() { Title = "تدشين منصة التغيير والتحول", Type = "تدشين", Audience = "جميع المنسوبين", Date = DateTime.Now.AddDays(-5).ToHijri(), StatusBadge = "bg-success" },
                    new() { Title = "إعلان عن توحيد التقارير الشهرية", Type = "إعلان تغيير", Audience = "سفراء التغيير", Date = DateTime.Now.AddDays(-3).ToHijri(), StatusBadge = "bg-success" },
                    new() { Title = "رسالة القيادة لدعم التغيير", Type = "رسالة قيادة", Audience = "جميع المنسوبين", Date = DateTime.Now.AddDays(-2).ToHijri(), StatusBadge = "bg-success" },
                    new() { Title = "الأسئلة الشائعة حول المنصة", Type = "FAQ", Audience = "جميع المنسوبين", Date = DateTime.Now.AddDays(-1).ToHijri(), StatusBadge = "bg-info" }
                },

                // ═══ إرشادات النشر ═══
                PublishingGuidelines = new List<string>
                {
                    "استخدم دائماً القوالب المعتمدة من الاتصال المؤسسي.",
                    "أوضح في كل رسالة: لماذا التغيير؟ ماذا سيتغير؟ ماذا يعني للمنسوب؟",
                    "استخدم لغة داعمة وإيجابية — تجنب اللغة الرقابية.",
                    "ذكر قناة الاستفسار الرسمية (سفير التغيير / البريد الداخلي).",
                    "أرفق روابط المواد التعليمية عند الاقتضاء.",
                    "احصل على اعتماد الجهة المخولة قبل النشر الرسمي.",
                    "وثّق كل رسالة منشورة في سجل الإعلانات."
                }
            };

            return vm;
        }
    }
}