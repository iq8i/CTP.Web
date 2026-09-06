using CTP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTP.Application.Interfaces.Repositories
{
    public interface IRecommendationRepository
    {
        Task<Recommendation> AddAsync(Recommendation recommendation);
        Task<int> SaveChangesAsync();

        Task<IEnumerable<Recommendation>> GetAllWithDetailsAsync();
        Task<IEnumerable<Recommendation>> GetPendingChairApprovalsAsync();
        Task<Recommendation?> GetByMonthlyReportIdAsync(int reportId);
        Task<IEnumerable<Recommendation>> GetApprovedRecommendationsAsync();
        Task<IEnumerable<Recommendation>> GetApprovedForInstitutionalReportAsync();
        Task<IEnumerable<Recommendation>> GetApprovedForImpactDashboardAsync();
        Task<IEnumerable<Recommendation>> GetApprovedForExecutiveSummaryAsync();
        Task<bool> UpdatePlacementAsync(int reportId, string placement, bool enabled);
    }
}