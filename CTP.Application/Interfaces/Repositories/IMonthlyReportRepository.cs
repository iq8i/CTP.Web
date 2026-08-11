using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Repositories
{
    public interface IMonthlyReportRepository
    {
        Task<MonthlyReport> AddAsync(MonthlyReport report);
        Task<int> SaveChangesAsync();
    }
}