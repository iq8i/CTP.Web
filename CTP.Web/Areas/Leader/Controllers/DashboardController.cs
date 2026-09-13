using CTP.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Areas.Leader.Controllers
{
    [Area("Leader")]
    [Authorize(Roles = AppRoles.Leader)]
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["EntityTitle"] = "الموجز الاستراتيجي";
            ViewData["EntityHeaderSubtitle"] = "نظرة معالي القائد على برنامج التغيير والتحول";
            ViewData["EntityHeaderIcon"] = "bi-award";
            ViewData["ThemeColor"] = "#073B49";
            return View();
        }

        [HttpGet]
        public IActionResult Impact()
        {
            ViewData["EntityTitle"] = "الأثر المتحقق";
            ViewData["EntityHeaderSubtitle"] = "قياس الأثر على مستوى المنظومة";
            ViewData["EntityHeaderIcon"] = "bi-graph-up-arrow";
            ViewData["ThemeColor"] = "#073B49";
            return View();
        }
    }
}