namespace CTP.Web.Areas.Committee.Models
{
    public class CommitteeReportListViewModel
    {
        public int Id { get; set; }
        public string ReportNumber { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public string AmbassadorName { get; set; } = string.Empty;
        public string ChangeName { get; set; } = string.Empty;
        public int ReadinessScore { get; set; }
        public string ApprovedDate { get; set; } = string.Empty;
    }
}