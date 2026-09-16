using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Services
{
    public interface IReportQualityService
    {
        ReportQualityResult Check(MonthlyReport report);
        ReportQualityResult CheckForDraft(MonthlyReport report);
        ReportQualityResult CheckForSubmission(MonthlyReport report);
    }

    public class ReportQualityResult
    {
        public List<FieldIssue> Issues { get; set; } = new();
        public bool IsValid => Issues.Count == 0;
        public int TotalFields { get; set; }
        public int ValidFields { get; set; }
        public int QualityPercent => TotalFields == 0 ? 0 : (int)Math.Round((double)ValidFields / TotalFields * 100);
        public List<FieldIssue> CriticalIssues => Issues;
    }

    public class FieldIssue
    {
        public string FieldName { get; set; } = string.Empty;
        public string FieldLabel { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public IssueSeverity Severity { get; set; } = IssueSeverity.Critical;
    }

    public enum IssueSeverity { Warning = 1, Critical = 2 }
}