namespace CTP.Web.Areas.DeputyLeader.Models
{
    public class DeputyDashboardViewModel
    {
        // ═══ KPIs ═══
        public int TotalReports { get; set; }
        public int ApprovedReports { get; set; }
        public double AvgReadiness { get; set; }
        public double AvgAdoption { get; set; }
        public int ApprovedRecommendations { get; set; }
        public int PendingDecisions { get; set; }

        // ═══ الملخص التنفيذي الشهري ═══
        public string MonthlySummary { get; set; } = string.Empty;
        public string CurrentMonth { get; set; } = string.Empty;

        // ═══ مؤشرات الجاهزية والتبني ═══
        public double ReadinessCurrent { get; set; }
        public double AdoptionCurrent { get; set; }
        public double ReinforcementCurrent { get; set; }

        // ═══ التوصيات المعتمدة ═══
        public List<ApprovedRecommendationItem> ApprovedRecommendationsList { get; set; } = new();

        // ═══ قرارات الدعم ═══
        public List<SupportDecisionItem> SupportDecisions { get; set; } = new();

        // ═══ الأثر ═══
        public List<ImpactItem> TopImpacts { get; set; } = new();
        public int ImpactVerifiedCount { get; set; }
        public double AvgImpactDelta { get; set; }

        // ═══ خطة الشهر القادم ═══
        public List<string> NextMonthPlan { get; set; } = new();
    }

    public class ApprovedRecommendationItem
    {
        public string RecommendationNumber { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string SuggestedAction { get; set; } = string.Empty;
        public string ActionOwner { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
    }

    public class SupportDecisionItem
    {
        public string Title { get; set; } = string.Empty;
        public string Entity { get; set; } = string.Empty;
        public string RequestedBy { get; set; } = string.Empty;
        public string ExpectedImpact { get; set; } = string.Empty;
    }

    public class ImpactItem
    {
        public string IndicatorName { get; set; } = string.Empty;
        public int Before { get; set; }
        public int After { get; set; }
        public int Delta { get; set; }
    }
}