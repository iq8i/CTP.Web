using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Services
{
    /// <summary>
    /// خدمة أدوات التحليل المنهجي (ADKAR, PCT, Pareto, ...)
    /// </summary>
    public interface IAnalysisToolService
    {
        /// <summary>
        /// فحص اكتمال التقرير — يعيد العناصر الناقصة
        /// </summary>
        CompletenessResult CheckCompleteness(MonthlyReport report);

        /// <summary>
        /// خلاصة What / So What / Now What
        /// </summary>
        WswnwResult BuildWswnwSummary(MonthlyReport report);

        /// <summary>
        /// تحليل ADKAR — نقطة الحاجز
        /// </summary>
        AdkarResult AnalyzeAdkar(MonthlyReport report);

        /// <summary>
        /// تحليل PCT — مثلث نجاح التغيير
        /// </summary>
        PctResult AnalyzePct(MonthlyReport report);

        /// <summary>
        /// تحليل Pareto لعوائق التبني المتكررة على مستوى المنظومة
        /// </summary>
        List<ParetoBarrier> AnalyzePareto(List<MonthlyReport> reports, int thresholdPercent = 80);

        /// <summary>
        /// تحليل مخاطر التبني لتقرير واحد
        /// </summary>
        AdoptionRiskResult AnalyzeAdoptionRisk(MonthlyReport report);
    }
}

    public class CompletenessResult
    {
        public int TotalFields { get; set; }
        public int CompletedFields { get; set; }
        public List<string> MissingFields { get; set; } = new();
        public int CompletenessPercent => TotalFields == 0 ? 0 : (int)Math.Round((double)CompletedFields / TotalFields * 100);
        public bool IsComplete => MissingFields.Count == 0;
    }

    public class WswnwResult
    {
        public string What { get; set; } = string.Empty;
        public string SoWhat { get; set; } = string.Empty;
        public string NowWhat { get; set; } = string.Empty;
        public string ExpectedImpact { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string SupportDecision { get; set; } = string.Empty;
    }

    public class AdkarResult
    {
        public int Awareness { get; set; }
        public int Desire { get; set; }
        public int Knowledge { get; set; }
        public int Ability { get; set; }
        public int Reinforcement { get; set; }
        public string BarrierName { get; set; } = string.Empty;
        public int BarrierValue { get; set; }
    }

    public class PctResult
    {
        public int Leadership { get; set; }       // القيادة والرعاية
        public int ProjectManagement { get; set; } // إدارة المشروع
        public int PeopleManagement { get; set; }  // إدارة الأفراد
        public string Weakest { get; set; } = string.Empty;
        public string Recommendation { get; set; } = string.Empty;
    }
public class ParetoBarrier
{
    public string Barrier { get; set; } = string.Empty;
    public int Count { get; set; }
    public int Percentage { get; set; }
    public int CumulativePercentage { get; set; }
    public bool IsFocusArea { get; set; }   // ضمن الـ 80%
}

public class AdoptionRiskResult
{
    public string RiskLevel { get; set; } = string.Empty;  // مرتفع / متوسط / منخفض
    public string BadgeClass { get; set; } = string.Empty;  // CSS class
    public int AdoptionScore { get; set; }
    public string TopBarrier { get; set; } = string.Empty;
    public string PotentialCause { get; set; } = string.Empty;
    public string ImpactOnResults { get; set; } = string.Empty;
}
