using CTP.Application.Interfaces.Repositories;
using CTP.Domain.Entities;
using CTP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CTP.Infrastructure.Repositories
{
    public class MonthlyReportRepository : IMonthlyReportRepository
    {
        private readonly ApplicationDbContext _context;

        public MonthlyReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MonthlyReport> AddAsync(MonthlyReport report)
        {
            await _context.MonthlyReports.AddAsync(report);
            return report;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MonthlyReport>> GetReportsByPreparerAsync(int preparerId)
        {
            return await _context.MonthlyReports
                .Where(m => m.PreparerId == preparerId)
                .OrderByDescending(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task<MonthlyReport?> GetByIdAsync(int id)
        {
            return await _context.MonthlyReports
                .Include(m => m.Organization)
                .Include(m => m.Preparer)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MonthlyReport>> GetByOrganizationAndStatusAsync(int orgId, CTP.Domain.Enums.ReportStatus status)
        {
            return await _context.MonthlyReports
                .Include(m => m.Preparer)
                .Where(m => m.OrganizationEntityId == orgId && m.Status == status)
                .OrderByDescending(m => m.SubmittedDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<MonthlyReport>> GetSubmittedReportsAsync()
        {
            return await _context.MonthlyReports
                .Include(m => m.Organization)
                .Where(m => m.Status != CTP.Domain.Enums.ReportStatus.Draft)
                .OrderByDescending(m => m.SubmittedDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<MonthlyReport>> GetApprovedByOrganizationAsync(int orgId)
        {
            return await _context.MonthlyReports
                .Include(m => m.Preparer)
                .Where(m => m.OrganizationEntityId == orgId && m.Status == CTP.Domain.Enums.ReportStatus.Approved)
                .OrderByDescending(m => m.ApprovedDate)
                .ToListAsync();
        }
    }
}