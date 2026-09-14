using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Services
{
    public interface IReportQualityService
    {
        /// <summary>
        /// فحص شامل لجودة التقرير — يكشف الحقول الناقصة والمُزيفة
        /// </summary>
        ReportQualityResult Check(MonthlyReport report);

        /// <summary>
        /// فحص مخصص عند الحفظ (قبل الرفع)
        /// </summary>
        ReportQualityResult CheckForDraft(MonthlyReport report);

        /// <summary>
        /// فحص صارم عند الرفع للاعتماد
        /// </summary>
        ReportQualityResult CheckForSubmission(MonthlyReport report);
    }

    public class ReportQualityResult
    {
        public List<FieldIssue> Issues { get; set; } = new();
        public bool IsValid => Issues.Count == 0;
        public int TotalFields { get; set; }
        public int ValidFields { get; set; }
        public int QualityPercent => TotalFields == 0 ? 0 : (int)Math.Round((double)ValidFields / TotalFields * 100);

        public List<FieldIssue> CriticalIssues => Issues.Where(i => i.Severity == IssueSeverity.Critical).ToList();
        public List<FieldIssue> Warnings => Issues.Where(i => i.Severity == IssueSeverity.Warning).ToList();
    }

    public class FieldIssue
    {
        public string FieldName { get; set; } = string.Empty;
        public string FieldLabel { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public IssueSeverity Severity { get; set; }
    }

    public enum IssueSeverity
    {
        Warning = 1,    // حقل ثانوي — يسمح بالمرور
        Critical = 2    // حقل حرج — يمنع المرور
    }
}