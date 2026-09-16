namespace CTP.Web.Areas.Leader.Models
{
    public class LeaderDashboardViewModel
    {
        // ═══ KPIs ═══
        public int TotalReports { get; set; }
        public int ApprovedReports { get; set; }
        public double AvgReadiness { get; set; }
        public double AvgAdoption { get; set; }
        public int TotalRecommendations { get; set; }
        public int ApprovedRecommendations { get; set; }

        // ═══ حالة البرنامج — خارطة الطريق ═══
        public string CurrentPhase { get; set; } = "مراجعة المادة العلمية واعتمادها";
        public int RoadmapProgressPercent { get; set; } = 38;
        public string RoadmapStatus { get; set; } = "البرنامج يسير وفق خارطة الطريق المعتمدة";

        // ═══ الملخص التنفيذي المعتمد ═══
        public string ExecutiveSummary { get; set; } = string.Empty;

        // ═══ قرارات الدعم عند الحاجة ═══
        public List<SupportDecisionItem> SupportDecisions { get; set; } = new();

        // ═══ المخاطر الحرجة عند الحاجة ═══
        public List<RiskItem> CriticalRisks { get; set; } = new();

        // ═══ الأثر ═══
        public int ImpactVerifiedCount { get; set; }
        public int ImpactPartialCount { get; set; }
        public double AvgImpactDelta { get; set; }
        public List<ImpactPreviewItem> TopImpacts { get; set; } = new();

        // ═══ آخر النشاطات (استراتيجية فقط) ═══
        public List<ActivityItem> RecentActivities { get; set; } = new();
    }

    public class SupportDecisionItem
    {
        public string Title { get; set; } = string.Empty;
        public string Entity { get; set; } = string.Empty;
        public string RequestedBy { get; set; } = string.Empty;
    }

    public class RiskItem
    {
        public string Title { get; set; } = string.Empty;
        public string Entity { get; set; } = string.Empty;
        public string Level { get; set; } = "متوسط";
        public string BadgeClass { get; set; } = "bg-warning text-dark";
    }

    public class ImpactPreviewItem
    {
        public string IndicatorName { get; set; } = string.Empty;
        public int Before { get; set; }
        public int After { get; set; }
        public int Delta { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class ActivityItem
    {
        public string Icon { get; set; } = "bi-circle";
        public string Color { get; set; } = "#106981";
        public string Title { get; set; } = string.Empty;
        public string TimeAgo { get; set; } = string.Empty;
    }
}