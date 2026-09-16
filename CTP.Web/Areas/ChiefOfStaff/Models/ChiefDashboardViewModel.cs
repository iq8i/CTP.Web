using CTP.Domain.Enums;

namespace CTP.Web.Areas.ChiefOfStaff.Models
{
    public class ChiefDashboardViewModel
    {
        // ═══ KPIs ═══
        public int TotalEntities { get; set; }
        public int EntitiesSubmitted { get; set; }
        public int EntitiesPending { get; set; }
        public int EntitiesLate { get; set; }
        public int TotalReports { get; set; }
        public int ApprovedReports { get; set; }
        public int PendingApprovalReports { get; set; }

        // ═══ نسبة الرفع الكلية ═══
        public int CompletionPercent { get; set; }

        // ═══ جدول الجهات ═══
        public List<EntityStatusItem> EntitiesStatus { get; set; } = new();

        // ═══ الجهات المتأخرة ═══
        public List<EntityStatusItem> LateEntities { get; set; } = new();

        // ═══ التقارير المعتمدة الحديثة ═══
        public List<ApprovedReportItem> RecentApprovedReports { get; set; } = new();

        // ═══ ملخصات رئيس اللجنة ═══
        public List<string> ChairSummaries { get; set; } = new();

        // ═══ التوصيات الجاهزة للرفع ═══
        public List<ReadyRecommendationItem> ReadyRecommendations { get; set; } = new();

        // ═══ سجل المتابعة ═══
        public List<string> FollowUpLog { get; set; } = new();
    }

    public class EntityStatusItem
    {
        public int EntityId { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public int TotalReports { get; set; }
        public int ApprovedReports { get; set; }
        public int CompletionPercent { get; set; }
        public bool IsComplete { get; set; }
        public bool IsLate { get; set; }
    }

    public class ApprovedReportItem
    {
        public string ReportNumber { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string ChangeName { get; set; } = string.Empty;
        public string ApprovedDate { get; set; } = string.Empty;
    }

    public class ReadyRecommendationItem
    {
        public int Id { get; set; }                          // ← جديد
        public string RecommendationNumber { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string SuggestedAction { get; set; } = string.Empty;
        public string ActionOwner { get; set; } = string.Empty;
        public string Escalation { get; set; } = string.Empty;
        public RecommendationStatus Status { get; set; }     // ← جديد
    }
}