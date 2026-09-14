using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;

namespace CTP.Web.Areas.Committee.Models
{
    public class ChairWorkCenterViewModel
    {
        // ─── KPIs (Tab 1) ───
        public int TotalReports { get; set; }
        public int CompleteReports { get; set; }
        public int IncompleteReports { get; set; }
        public int LateReports { get; set; }

        // ─── Tab 1: قائمة التقارير ───
        public List<ChairReportListItem> Reports { get; set; } = new();

        // ─── Tab 2: مدخلات الجهات ───
        public List<EntityInput> EntityInputs { get; set; } = new();
        public int TotalInputs { get; set; }
        public int PendingInputs { get; set; }
        public Dictionary<string, int> InputsByType { get; set; } = new();

        // ─── التبويب النشط ───
        public string ActiveTab { get; set; } = "t1";

        // ─── التقرير المختار (Session) ───
        public MonthlyReport? SelectedReport { get; set; }

        // ─── Tab 3: نتائج الأدوات ───
        public CompletenessResult? Completeness { get; set; }
        public WswnwResult? Wswnw { get; set; }
        public AdkarResult? Adkar { get; set; }
        public PctResult? Pct { get; set; }
        // ─── Tab 4: التوصيات ───
        public List<ChairRecommendationItem> Recommendations { get; set; } = new();
        public Recommendation? SelectedRecommendation { get; set; }
        public QualityGateResult? QualityGate { get; set; }

        // ─── KPIs للتوصيات ───
        public int TotalRecommendations { get; set; }
        public int PendingRecommendations { get; set; }
        public int ApprovedRecommendations { get; set; }
        public int RejectedRecommendations { get; set; }
        // ─── B.3b: الأدوات الإضافية ───
        public List<ParetoBarrier> ParetoBarriers { get; set; } = new();
        public AdoptionRiskResult? AdoptionRisk { get; set; }
        // ─── Tab 5: الداشبورد المؤسسي ───
        public int TotalAnalyzedReports { get; set; }
        public double AvgReadiness { get; set; }
        public double AvgAdoption { get; set; }
        public double AvgReinforcement { get; set; }

        // ADKAR على مستوى المنظومة
        public double AvgAdkarAwareness { get; set; }
        public double AvgAdkarDesire { get; set; }
        public double AvgAdkarKnowledge { get; set; }
        public double AvgAdkarAbility { get; set; }
        public double AvgAdkarReinforcement { get; set; }

        // توزيع حسب نوع الجهة
        public List<EntityTypeStat> StatsByEntityType { get; set; } = new();

        // توصيات جاهزة للإدراج
        public List<ChairRecommendationItem> ApprovedRecommendationsList { get; set; } = new();

        // عدد الأثر الحالي
        public int TotalPlacedInInstitutional { get; set; }
        public int TotalPlacedInImpact { get; set; }
        public int TotalPlacedInExecutive { get; set; }
    }

    public class ChairRecommendationItem
    {
        public int Id { get; set; }
        public string RecommendationNumber { get; set; } = string.Empty;
        public int ReportId { get; set; }
        public string ReportNumber { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string GapType { get; set; } = string.Empty;
        public string SuggestedAction { get; set; } = string.Empty;
        public string StatusArabic { get; set; } = string.Empty;
        public string StatusBadgeClass { get; set; } = string.Empty;
        public bool IsPending { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public bool IncludeInInstitutionalReport { get; set; }
        public bool IncludeInImpactDashboard { get; set; }
        public bool IncludeInExecutiveSummary { get; set; }
        public string CreatedDateFormatted { get; set; } = "-";
        public string? ReviewedDateFormatted { get; set; }
    }
    public class EntityTypeStat
    {
        public string EntityType { get; set; } = string.Empty;
        public int ReportCount { get; set; }
        public double AvgReadiness { get; set; }
        public double AvgAdoption { get; set; }
        public int ReadinessPercent { get; set; }
        public int AdoptionPercent { get; set; }
    }
    public class ChairReportListItem
    {
        public int Id { get; set; }
        public string ReportNumber { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string ChangeName { get; set; } = string.Empty;
        public string Month { get; set; } = string.Empty;
        public int Year { get; set; }
        public int ReadinessScore { get; set; }
        public int AdoptionScore { get; set; }
        public int ActivityCompletionRate { get; set; }
        public string SubmittedDateFormatted { get; set; } = "-";
        public bool IsComplete { get; set; }
        public bool IsLate { get; set; }
        public string StatusArabic { get; set; } = string.Empty;
        public bool HasActiveRecommendation { get; set; }
        
    }
}