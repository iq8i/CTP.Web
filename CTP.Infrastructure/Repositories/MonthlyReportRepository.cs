using CTP.Application.Interfaces.Repositories;
using CTP.Domain.Entities;
using CTP.Infrastructure.Data;

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
    }
}