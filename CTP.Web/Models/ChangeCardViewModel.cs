namespace CTP.Web.Models
{
    public class ChangeCardViewModel
    {
        public int Id { get; set; }
        public string ChangeName { get; set; } = string.Empty;
        public string ChangeType { get; set; } = string.Empty;
        public string CurrentStage { get; set; } = string.Empty;
        public string ChangeSummary { get; set; } = string.Empty;
        public int ReadinessScore { get; set; }
        public string AmbassadorName { get; set; } = string.Empty;
        public string ApprovedDateHijri { get; set; } = string.Empty;
    }
}