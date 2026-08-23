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
        public async Task<IEnumerable<MonthlyReport>> GetAmbassadorReportsAsync(int preparerId)
        {
            return await _repository.GetReportsByPreparerAsync(preparerId);
        }
        public async Task<IEnumerable<MonthlyReport>> GetPendingReportsForLeaderAsync(int orgId)
        {
            return await _repository.GetByOrganizationAndStatusAsync(orgId, CTP.Domain.Enums.ReportStatus.Submitted);
        }

        public async Task<bool> ReviewReportAsync(int reportId, bool isApproved, string? notes)
        {
            var report = await _repository.GetByIdAsync(reportId);
            if (report == null || report.Status != CTP.Domain.Enums.ReportStatus.Submitted)
                return false;

            // التعديل الجوهري: القائد يحيلها للجنة (تحت التحليل) بدلاً من الاعتماد النهائي
            report.Status = isApproved ? CTP.Domain.Enums.ReportStatus.UnderAnalysis : CTP.Domain.Enums.ReportStatus.Returned;
            report.ApproverNotes = notes;

            await _repository.SaveChangesAsync();
            return true;
        }

        // تعديل هام جداً: اللجنة تقرأ فقط التقارير المعتمدة من القائد
        public async Task<IEnumerable<MonthlyReport>> GetCommitteeInboxReportsAsync()
        {
            var allReports = await _repository.GetSubmittedReportsAsync();
            // التعديل الجوهري: صندوق وارد اللجنة يقرأ فقط ما هو "تحت التحليل"
            return allReports.Where(r => r.Status == CTP.Domain.Enums.ReportStatus.UnderAnalysis);
        }

        public async Task<MonthlyReport?> GetReportByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<bool> UpdateReportAsync(MonthlyReport report)
        {
            // عند التحديث والإرسال، نعيد الحالة إلى "مرفوع" لتعود لقائد الوحدة
            if (report.Status == CTP.Domain.Enums.ReportStatus.Returned)
            {
                report.Status = CTP.Domain.Enums.ReportStatus.Submitted;
                report.SubmittedDate = DateTime.Now;
            }

            await _repository.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<MonthlyReport>> GetUnitActiveChangesAsync(int orgId)
        {
            return await _repository.GetApprovedByOrganizationAsync(orgId);
        }
        public async Task<bool> ApproveByCommitteeAsync(int reportId, string recommendation)
        {
            var report = await _repository.GetByIdAsync(reportId);
            if (report == null || report.Status != CTP.Domain.Enums.ReportStatus.UnderAnalysis) return false;

            report.Status = CTP.Domain.Enums.ReportStatus.Approved;
            report.CommitteeRecommendation = recommendation;
            report.ApprovedDate = DateTime.Now;

            await _repository.SaveChangesAsync();
            return true;
        }
    }
}