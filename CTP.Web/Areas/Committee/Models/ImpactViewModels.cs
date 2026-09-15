using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;

namespace CTP.Web.Areas.Committee.Models
{
    public class ImpactDashboardViewModel
    {
        public ImpactSummaryResult Summary { get; set; } = new();
        public List<ImpactRow> Rows { get; set; } = new();
    }

    public class ImpactRow
    {
        public int RecommendationId { get; set; }
        public string RecommendationNumber { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string ReportNumber { get; set; } = string.Empty;
        public string SuggestedAction { get; set; } = string.Empty;

        public bool HasMeasurement { get; set; }
        public string IndicatorName { get; set; } = string.Empty;
        public int BeforeValue { get; set; }
        public int AfterValue { get; set; }
        public int Delta { get; set; }
        public string Status { get; set; } = "بانتظار القياس";
        public string StatusBadgeClass { get; set; } = "bg-secondary";
        public string? ImprovementReason { get; set; }
        public string? NextStep { get; set; }
        public string? Lesson { get; set; }
        public string? MeasuredDateFormatted { get; set; }
        public string SuccessIndicator { get; set; } = string.Empty;
    }
}