using CTP.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Areas.ChiefOfStaff.Controllers
{
    [Area("ChiefOfStaff")]
    [Authorize(Roles = AppRoles.ChiefOfStaff)]
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["EntityTitle"] = "متابعة الرفع";
            ViewData["EntityHeaderSubtitle"] = "متابعة اكتمال رفع التقارير من الجهات";
            ViewData["EntityHeaderIcon"] = "bi-list-check";
            ViewData["ThemeColor"] = "#2E86A0";
            return View();
        }

        [HttpGet]
        public IActionResult ApprovedReports()
        {
            ViewData["EntityTitle"] = "التقارير المعتمدة";
            ViewData["EntityHeaderSubtitle"] = "التقارير المعتمدة الواردة من أصحاب السعادة";
            ViewData["EntityHeaderIcon"] = "bi-file-earmark-check";
            ViewData["ThemeColor"] = "#2E86A0";
            return View();
        }

        [HttpGet]
        public IActionResult Escalation()
        {
            ViewData["EntityTitle"] = "الرفع القيادي";
            ViewData["EntityHeaderSubtitle"] = "ما يُرفع لسعادة النائب ومعالي القائد";
            ViewData["EntityHeaderIcon"] = "bi-send-check";
            ViewData["ThemeColor"] = "#2E86A0";
            return View();
        }
    }
}