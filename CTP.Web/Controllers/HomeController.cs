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
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? AppRoles.Staff;

            return role switch
            {
                AppRoles.Leader => RedirectToAction("Index", "Dashboard", new { area = "Leader" }),
                AppRoles.DeputyLeader => RedirectToAction("Index", "Dashboard", new { area = "DeputyLeader" }),
                AppRoles.ChiefOfStaff => RedirectToAction("Index", "Dashboard", new { area = "ChiefOfStaff" }),
                AppRoles.CommitteeChair => RedirectToAction("Index", "ChairWorkCenter", new { area = "Committee" }),
                AppRoles.CommitteeMember => RedirectToAction("Index", "ChairWorkCenter", new { area = "Committee" }),
                AppRoles.UnitLeader => RedirectToAction("Index", "Dashboard", new { area = "UnitLeader" }),
                AppRoles.Ambassador => RedirectToAction("Index", "Report", new { area = "Ambassador" }),
                AppRoles.CorporateComms => RedirectToAction("Index", "Dashboard", new { area = "CorporateComms" }),
                AppRoles.Manager => RedirectToAction("Index", "Dashboard", new { area = "Manager" }),
                _ => RedirectToAction("Index", "Dashboard", new { area = "Staff" })  // Staff
            };
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}