using CTP.Application.Interfaces.Services;

namespace CTP.Web.Areas.Committee.Models
{
    public class InstitutionalReportViewModel
    {
        // الترويسة
        public string ReportMonth { get; set; } = string.Empty;
        public int ReportYear { get; set; }
        public DateTime GeneratedDate { get; set; }

        // KPIs
        public int TotalReports { get; set; }
        public int ApprovedReports { get; set; }
        public int TotalRecommendations { get; set; }
        public int InstitutionalRecommendations { get; set; }

        // المتوسطات
        public double AvgReadiness { get; set; }
        public double AvgAdoption { get; set; }
        public double AvgAdkarAwareness { get; set; }
        public double AvgAdkarDesire { get; set; }
        public double AvgAdkarKnowledge { get; set; }
        public double AvgAdkarAbility { get; set; }
        public double AvgAdkarReinforcement { get; set; }

        // Pareto
        public List<ParetoBarrier> ParetoBarriers { get; set; } = new();

        // التوصيات المُدرجة
        public List<ReportRecommendationItem> Recommendations { get; set; } = new();
    }

    public class ReportRecommendationItem
    {
        public string RecommendationNumber { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string ReportNumber { get; set; } = string.Empty;
        public string GapType { get; set; } = string.Empty;
        public string SuggestedAction { get; set; } = string.Empty;
        public string ActionOwner { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string SuccessIndicator { get; set; } = string.Empty;
        public string ExpectedImpact { get; set; } = string.Empty;
    }
}