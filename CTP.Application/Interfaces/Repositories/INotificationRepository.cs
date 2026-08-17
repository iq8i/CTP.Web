using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task<List<Notification>> GetUnreadByUserIdAsync(int userId);
        Task MarkAllAsReadAsync(int userId);
        Task<int> SaveChangesAsync();
    }
}