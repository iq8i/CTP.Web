using CTP.Application.Interfaces.Repositories;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;

namespace CTP.Application.Services
{
    public class MonthlyReportService : IMonthlyReportService
    {
        private readonly IMonthlyReportRepository _repository;

        public MonthlyReportService(IMonthlyReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<MonthlyReport> CreateReportAsync(MonthlyReport report)
        {
            // إنشاء رقم مرجعي فريد للتقرير
            report.ReportNumber = $"CTP-{report.Year}-{report.Month}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";
            report.CreatedDate = DateTime.Now;

            await _repository.AddAsync(report);
            await _repository.SaveChangesAsync();
            return report;
        }
    }
}