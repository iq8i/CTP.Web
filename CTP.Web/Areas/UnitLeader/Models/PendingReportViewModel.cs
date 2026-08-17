namespace CTP.Web.Areas.UnitLeader.Models
{
    public class PendingReportViewModel
    {
        public int Id { get; set; }
        public string ReportNumber { get; set; } = string.Empty;
        public string MonthYear { get; set; } = string.Empty;
        public string PreparerName { get; set; } = string.Empty;
        public string ChangeName { get; set; } = string.Empty;
        public int ReadinessScore { get; set; }
        public string SubmittedDate { get; set; } = string.Empty;
    }
}