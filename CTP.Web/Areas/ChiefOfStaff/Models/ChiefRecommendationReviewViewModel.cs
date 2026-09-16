using CTP.Domain.Entities;
using CTP.Domain.Enums;

namespace CTP.Web.Areas.ChiefOfStaff.Models
{
    public class ChiefRecommendationReviewViewModel
    {
        public Recommendation Recommendation { get; set; } = new();
        public MonthlyReport? SourceReport { get; set; }
        public List<PreviousDecisionItem> PreviousDecisions { get; set; } = new();
    }

    public class PreviousDecisionItem
    {
        public string Action { get; set; } = string.Empty;
        public string Actor { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string BadgeClass { get; set; } = "bg-secondary";
    }
}