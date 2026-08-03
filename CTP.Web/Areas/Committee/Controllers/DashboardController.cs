using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CTP.Web.Areas.Committee.Controllers
{
    [Area("Committee")] // مهم جداً: هذا يربط الكنترولر بالـ Area
   // [Authorize(Roles = "COMMITTEE_CHAIR")] // تأمين: لن يدخل إلا رئيس اللجنة
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            // هنا ستجلب البيانات الحقيقية لاحقاً من قاعدة البيانات
            ViewData["EntityTitle"] = "إدارة اللجان";
            ViewData["EntityHeaderSubtitle"] = "مركز عمليات اللجان والاعتمادات";
            ViewData["EntityHeaderIcon"] = "bi-bullseye";

            return View();
        }
    }
}