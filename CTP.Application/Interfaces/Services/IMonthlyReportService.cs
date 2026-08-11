using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Services
{
    public interface IMonthlyReportService
    {
        Task<MonthlyReport> CreateReportAsync(MonthlyReport report);
    }
}