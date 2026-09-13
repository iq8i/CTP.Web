using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CTP.Domain.Constants;

namespace CTP.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var fullName = User.FindFirst(ClaimTypes.GivenName)?.Value ?? "مستخدم";
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? AppRoles.Staff;
            var roleArabic = AppRolesHelper.GetArabicName(role);

            ViewData["Title"] = "لوحة القياس";
            ViewData["EntityTitle"] = "لوحة القياس";
            ViewData["EntityHeaderSubtitle"] = $"مرحباً {fullName} — {roleArabic}";
            ViewData["EntityHeaderIcon"] = "bi-speedometer2";
            ViewData["ThemeColor"] = "#106981";

            ViewBag.FullName = fullName;
            ViewBag.RoleCode = role;
            ViewBag.RoleArabic = roleArabic;

            return View();
        }

        [HttpGet]
        public IActionResult DashboardStats()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? AppRoles.Staff;

            // بيانات تجريبية حسب الدور (سنستبدلها بـ Service لاحقاً)
            return Json(new
            {
                totalReports = role == AppRoles.Ambassador ? 12 : 24,
                avgAdkar = 76,
                totalRecommendations = 18,
                totalChallenges = 5,
                pendingApprovals = role == AppRoles.UnitLeader ? 3 : 0
            });
        }

        [HttpGet]
        public IActionResult RecentActivities()
        {
            var activities = new[]
            {
                new { title = "تم رفع تقرير (هيئة العمليات)", time = "منذ 10 دقائق", icon = "bi-arrow-up-circle-fill", color = "#10b981" },
                new { title = "اعتماد توصية لدعم الاتصال", time = "منذ ساعة", icon = "bi-check-circle", color = "#10b981" },
                new { title = "تعليق من مدير الإدارة", time = "منذ 3 ساعات", icon = "bi-chat-dots", color = "#f59e0b" },
                new { title = "توصية جديدة بانتظار الاعتماد", time = "منذ 5 ساعات", icon = "bi-hourglass-split", color = "#f59e0b" }
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

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}