namespace CTP.Web.Areas.Committee.Models
{
    public class CommitteeReportDetailsViewModel
    {
        public int Id { get; set; }
        public string ReportNumber { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public string PreparerName { get; set; } = string.Empty;
        public string MonthYear { get; set; } = string.Empty;
        public string ChangeName { get; set; } = string.Empty;
        public string ChangeType { get; set; } = string.Empty;
        public string CurrentStage { get; set; } = string.Empty;
        public string? AffectedGroups { get; set; }

        public string? ChangeSummary { get; set; }
        public string? WhyImportant { get; set; }
        public string? WhatWillChange { get; set; }
        public string? WhatWillNotChange { get; set; }
        public string? ExecutedActivities { get; set; }

        public int ReadinessScore { get; set; }
        public int AdoptionScore { get; set; }
        public int AdkarAwareness { get; set; }
        public int AdkarDesire { get; set; }
        public int AdkarKnowledge { get; set; }
        public int AdkarAbility { get; set; }
        public int AdkarReinforcement { get; set; }

        public string? Obstacles { get; set; }
        public string? AdoptionBarriers { get; set; }
        public string? RequiredSupport { get; set; }
        public string? Risks { get; set; }
        public string? ImprovementOpportunities { get; set; }
        public string? SuccessStories { get; set; }
        public string? InitialRecommendation { get; set; }
        public string? ApproverNotes { get; set; }
    }
}