using CTP.Application.Interfaces.Repositories;
using CTP.Domain.Entities;
using CTP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CTP.Infrastructure.Repositories
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly ApplicationDbContext _context;

        public RecommendationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Recommendation> AddAsync(Recommendation recommendation)
        {
            await _context.Recommendations.AddAsync(recommendation);
            return recommendation;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Recommendation>> GetAllWithDetailsAsync()
        {
            return await _context.Recommendations
                .Include(r => r.SourceReport!)
                    .ThenInclude(sr => sr.Organization)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recommendation>> GetPendingChairApprovalsAsync()
        {
            return await _context.Recommendations
                .Include(r => r.SourceReport!)
                    .ThenInclude(sr => sr.Organization)
                .Where(r =>
                    (r.Status == "جاهزة لاعتماد رئيس اللجنة" || r.Status == "بانتظار قرار الدعم") &&
                    r.SourceReport != null &&
                    r.SourceReport.Status == CTP.Domain.Enums.ReportStatus.UnderAnalysis)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();
        }

        public async Task<Recommendation?> GetByMonthlyReportIdAsync(int reportId)
        {
            return await _context.Recommendations
                .Include(r => r.SourceReport!)
                    .ThenInclude(sr => sr.Organization)
                .FirstOrDefaultAsync(r => r.MonthlyReportId == reportId);
        }

        public async Task<IEnumerable<Recommendation>> GetApprovedRecommendationsAsync()
        {
            return await _context.Recommendations
                .Include(r => r.SourceReport!)
                    .ThenInclude(sr => sr.Organization)
                .Where(r => r.Status == "معتمدة")
                .OrderByDescending(r => r.ReviewedDate ?? r.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recommendation>> GetApprovedForInstitutionalReportAsync()
        {
            return await _context.Recommendations
                .Include(r => r.SourceReport!)
                    .ThenInclude(sr => sr.Organization)
                .Where(r => r.Status == "معتمدة" && r.IncludeInInstitutionalReport)
                .OrderByDescending(r => r.ReviewedDate ?? r.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recommendation>> GetApprovedForImpactDashboardAsync()
        {
            return await _context.Recommendations
                .Include(r => r.SourceReport!)
                    .ThenInclude(sr => sr.Organization)
                .Where(r => r.Status == "معتمدة" && r.IncludeInImpactDashboard)
                .OrderByDescending(r => r.ReviewedDate ?? r.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recommendation>> GetApprovedForExecutiveSummaryAsync()
        {
            return await _context.Recommendations
                .Include(r => r.SourceReport!)
                    .ThenInclude(sr => sr.Organization)
                .Where(r => r.Status == "معتمدة" && r.IncludeInExecutiveSummary)
                .OrderByDescending(r => r.ReviewedDate ?? r.CreatedDate)
                .ToListAsync();
        }

        public async Task<bool> UpdatePlacementAsync(int reportId, string placement, bool enabled)
        {
            var recommendation = await _context.Recommendations.FirstOrDefaultAsync(r => r.MonthlyReportId == reportId);
            if (recommendation == null || recommendation.Status != "معتمدة")
            {
                return false;
            }

            switch (placement)
            {
                case "institutional":
                    recommendation.IncludeInInstitutionalReport = enabled;
                    break;
                case "impact":
                    recommendation.IncludeInImpactDashboard = enabled;
                    break;
                case "executive":
                    recommendation.IncludeInExecutiveSummary = enabled;
                    break;
                default:
                    return false;
            }

            recommendation.ReviewedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}