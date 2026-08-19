using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Services
{
    public interface IMonthlyReportService
    {
        Task<MonthlyReport> CreateReportAsync(MonthlyReport report);
        Task<IEnumerable<MonthlyReport>> GetAmbassadorReportsAsync(int preparerId);
        Task<IEnumerable<MonthlyReport>> GetPendingReportsForLeaderAsync(int orgId);
        Task<bool> ReviewReportAsync(int reportId, bool isApproved, string? notes);
        Task<MonthlyReport?> GetReportByIdAsync(int id);
        Task<bool> UpdateReportAsync(MonthlyReport report);
        Task<IEnumerable<MonthlyReport>> GetUnitActiveChangesAsync(int orgId);
    }


}