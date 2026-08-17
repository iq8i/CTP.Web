using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Repositories
{
    public interface IMonthlyReportRepository
    {
        Task<MonthlyReport> AddAsync(MonthlyReport report);
        Task<int> SaveChangesAsync();

        Task<IEnumerable<MonthlyReport>> GetReportsByPreparerAsync(int preparerId);

        Task<MonthlyReport?> GetByIdAsync(int id);
        Task<IEnumerable<MonthlyReport>> GetByOrganizationAndStatusAsync(int orgId, CTP.Domain.Enums.ReportStatus status);

        Task<IEnumerable<MonthlyReport>> GetSubmittedReportsAsync();

    }
}