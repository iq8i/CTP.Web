namespace CTP.Web.Areas.UnitLeader.Models
{
    public class ReportDetailsViewModel
    {
        public int Id { get; set; }
        public string ReportNumber { get; set; } = string.Empty;
        public string PreparerName { get; set; } = string.Empty;
        public string MonthYear { get; set; } = string.Empty;
        public string ChangeName { get; set; } = string.Empty;
        public string ChangeType { get; set; } = string.Empty;
        public string CurrentStage { get; set; } = string.Empty;
        public string? ChangeSummary { get; set; }
        public int ReadinessScore { get; set; }
        public int AdoptionScore { get; set; }
        public int AdkarAwareness { get; set; }
        public int AdkarDesire { get; set; }
        public int AdkarKnowledge { get; set; }
        public int AdkarAbility { get; set; }
        public int AdkarReinforcement { get; set; }
        public string? Obstacles { get; set; }
        public string? InitialRecommendation { get; set; }
        public string? CommitteeRecommendation { get; set; }
        public string? EvidenceLinks { get; set; }
    }
}