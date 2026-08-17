using System.Security.Claims;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;
using CTP.Web.Areas.Ambassador.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Areas.Ambassador.Controllers
{
    [Area("Ambassador")]
    [Authorize(Roles = "AMBASSADOR")]
    public class InputController : Controller
    {
        private readonly IEntityInputService _inputService;
        public InputController(IEntityInputService inputService) => _inputService = inputService;

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["EntityTitle"] = "رفع مشاركة أو احتياج";
            ViewData["EntityHeaderSubtitle"] = "قصص نجاح، احتياج دعم، فرص تحسين، أو عوائق تبني";
            ViewData["ThemeColor"] = "#C9A227";
            ViewData["EntityHeaderIcon"] = "bi-chat-left-dots";
            return View(new CreateEntityInputViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEntityInputViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orgIdStr = User.FindFirst("OrganizationId")?.Value;

            if (!int.TryParse(userIdStr, out int preparerId) || !int.TryParse(orgIdStr, out int orgId))
            {
                TempData["Error"] = "خطأ في الجلسة. يرجى إعادة الدخول.";
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var input = new EntityInput
            {
                OrganizationEntityId = orgId,
                PreparerId = preparerId,
                Type = model.Type,
                Content = model.Content
            };

            await _inputService.SubmitInputAsync(input);
            TempData["Success"] = "تم إرسال المدخل بنجاح وسوف تتم مراجعته من قبل اللجنة.";
            return RedirectToAction(nameof(Create)); // لتفريغ النموذج
        }
    }
}