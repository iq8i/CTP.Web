using CTP.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Areas.DeputyLeader.Controllers
{
    [Area("DeputyLeader")]
    [Authorize(Roles = AppRoles.DeputyLeader)]
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["EntityTitle"] = "الملخص التنفيذي";
            ViewData["EntityHeaderSubtitle"] = "ملخص سعادة النائب الشهري";
            ViewData["EntityHeaderIcon"] = "bi-file-earmark-bar-graph";
            ViewData["ThemeColor"] = "#0B4F61";
            return View();
        }

        [HttpGet]
        public IActionResult Decisions()
        {
            ViewData["EntityTitle"] = "قرارات الدعم";
            ViewData["EntityHeaderSubtitle"] = "قرارات الدعم المطلوبة من سعادة النائب";
            ViewData["EntityHeaderIcon"] = "bi-check2-square";
            ViewData["ThemeColor"] = "#0B4F61";
            return View();
        }
    }
}