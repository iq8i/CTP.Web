using CTP.Application.Interfaces.Repositories;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;

namespace CTP.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateNotificationAsync(int userId, string title, string message, string url, string iconClass, string colorClass)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                ActionUrl = url,
                IconClass = iconClass,
                TextColorClass = colorClass,
                CreatedDate = DateTime.Now,
                IsRead = false
            };

            await _repository.AddAsync(notification);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetUnreadNotificationsAsync(int userId)
        {
            return await _repository.GetUnreadByUserIdAsync(userId);
        }

        public async Task MarkAllAsReadAsync(int userId)
        {
            await _repository.MarkAllAsReadAsync(userId);
        }
    }
}