namespace CTP.Web.Areas.Ambassador.Models
{
    public class AmbassadorReportListViewModel
    {
        public int Id { get; set; }
        public string ReportNumber { get; set; } = string.Empty;
        public string MonthYear { get; set; } = string.Empty;
        public string ChangeName { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public string StatusBadgeClass { get; set; } = string.Empty;
        public string CreatedDateFormatted { get; set; } = string.Empty;
        public bool IsDraft { get; set; }
    }
}