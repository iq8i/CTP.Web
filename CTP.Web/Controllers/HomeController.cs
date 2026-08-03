//using CTP.Web.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;

//namespace CTP.Web.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
// using MediatR; // سنستخدمها لاحقاً للاتصال بطبقة الـ Application

namespace CTP.Web.Controllers
{
    // [Authorize] // سنقوم بتفعيلها بعد الانتهاء من نظام تسجيل الدخول
    public class HomeController : Controller
    {
        // private readonly ISender _mediator;

        public HomeController()
        {
            // _mediator = mediator; // حقن مكتبة MediatR
        }

        // 1. عرض الشاشة الرئيسية
        public IActionResult Index()
        {
            // مستقبلاً: يمكننا جلب اسم المستخدم وصلاحياته وتمريرها للـ View
            return View();
        }

        // ==========================================
        // Endpoints (APIs) لتغذية الشاشة بالبيانات
        // ==========================================

        [HttpGet]
        public IActionResult DashboardStats()
        {
            // مستقبلاً: var stats = await _mediator.Send(new GetDashboardStatsQuery());

            // بيانات تجريبية مؤقتة
            return Json(new
            {
                totalReports = 24,
                avgAdkar = 76,
                totalRecommendations = 18,
                totalChallenges = 5
            });
        }

        [HttpGet]
        public IActionResult RecentActivities()
        {
            var activities = new[]
            {
                new { title = "تم رفع تقرير (هيئة العمليات)", time = "منذ 10 دقائق", icon = "bi-arrow-up-circle-fill", color = "#10b981" },
                new { title = "اعتماد توصية لدعم الاتصال", time = "منذ ساعة", icon = "bi-check-circle", color = "#10b981" },
                new { title = "تعليق من مدير الإدارة", time = "منذ 3 ساعات", icon = "bi-chat-dots", color = "#f59e0b" }
            };

            return Json(activities);
        }

        [HttpGet]
        public IActionResult ChartData()
        {
            var data = new[]
            {
                new { status = "مسودة", count = 5 },
                new { status = "بانتظار الاعتماد", count = 12 },
                new { status = "معتمد", count = 7 }
            };

            return Json(data);
        }
    }
}
