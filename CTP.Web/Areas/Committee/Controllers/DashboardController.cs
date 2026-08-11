using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CTP.Application.Interfaces.Services;
using CTP.Web.Areas.Committee.Models;

namespace CTP.Web.Areas.Committee.Controllers
{
    [Area("Committee")]
    [Authorize(Roles = "COMMITTEE_CHAIR,LEADER")] // تم إضافة LEADER لتوافق القائمة الجانبية
    public class DashboardController : Controller
    {
        private readonly ICommitteeService _committeeService;

        public DashboardController(ICommitteeService committeeService)
        {
            _committeeService = committeeService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["EntityTitle"] = "إدارة اللجان";
            ViewData["EntityHeaderSubtitle"] = "مركز عمليات اللجان والاعتمادات";
            ViewData["EntityHeaderIcon"] = "bi-bullseye";
            ViewData["ThemeColor"] = "#106981";

            var committees = await _committeeService.GetDashboardCommitteesAsync();

            var viewModel = committees.Select(c => new CommitteeViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description ?? "لا يوجد وصف",
                Status = c.IsActive ? "نشطة" : "مغلقة",
                CreatedDateFormatted = c.CreatedDate.ToString("yyyy/MM/dd")
            }).ToList();

            return View(viewModel);
        }
    }
}