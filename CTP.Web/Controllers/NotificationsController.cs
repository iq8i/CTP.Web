using System.Security.Claims;
using CTP.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTP.Web.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyNotifications()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();

            var notifications = await _notificationService.GetUnreadNotificationsAsync(userId);
            int count = notifications.Count;
            string html = "";

            if (count > 0)
            {
                foreach (var notif in notifications)
                {
                    html += $@"<li><a class='dropdown-item border-bottom py-3 text-wrap' href='{notif.ActionUrl}'>
                                <div class='d-flex align-items-start gap-2 {notif.TextColorClass}'>
                                    <i class='bi {notif.IconClass} fs-5 mt-1'></i>
                                    <div>
                                        <strong class='d-block'>{notif.Title}</strong>
                                        <span class='text-dark small'>{notif.Message}</span>
                                        <div class='text-muted mt-1' style='font-size: 10px;'>{notif.CreatedDate:yyyy/MM/dd HH:mm}</div>
                                    </div>
                                </div></a></li>";
                }
            }
            else
            {
                html = "<li><div class='dropdown-item text-center text-muted small py-4'>لا توجد إشعارات جديدة</div></li>";
            }

            return Json(new { count, html });
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdStr, out int userId))
            {
                await _notificationService.MarkAllAsReadAsync(userId);
            }
            return Ok();
        }
    }
}