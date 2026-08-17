using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task CreateNotificationAsync(int userId, string title, string message, string url, string iconClass, string colorClass);
        Task<List<Notification>> GetUnreadNotificationsAsync(int userId);
        Task MarkAllAsReadAsync(int userId);
    }
}