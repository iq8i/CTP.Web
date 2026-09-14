using CTP.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTP.Domain.Entities
{
    public class Recommendation
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string RecommendationNumber { get; set; } = string.Empty; // مثل: R-031

        [Required]
        public int MonthlyReportId { get; set; }
        [ForeignKey("MonthlyReportId")]
        public virtual MonthlyReport? SourceReport { get; set; }

        [Required, StringLength(100)]
        public string GapType { get; set; } = string.Empty; // فجوة وعي، فجوة رغبة، الخ

        [Required, StringLength(500)]
        public string SuggestedAction { get; set; } = string.Empty; // الإجراء المقترح

        [StringLength(200)]
        public string ActionOwner { get; set; } = string.Empty; // مالك الإجراء

        [StringLength(100)]
        public string Duration { get; set; } = string.Empty; // المدة (مثل: 4 أسابيع)

        [StringLength(255)]
        public string SuccessIndicator { get; set; } = string.Empty; // مؤشر النجاح

        [StringLength(500)]
        public string ExpectedImpact { get; set; } = string.Empty; // الأثر المتوقع

        public RecommendationStatus Status { get; set; } = RecommendationStatus.RequiresCompletion;
        [StringLength(1000)]
        public string? ChairReviewNotes { get; set; }
        [StringLength(500)]
        public string? Evidence { get; set; }           // الدليل أو المؤشر

        [StringLength(500)]
        public string? Cause { get; set; }               // السبب المحتمل

        [StringLength(500)]
        public string? GapEffect { get; set; }           // أثر الفجوة على التبني

        [StringLength(500)]
        public string? ImpactMeasure { get; set; }       // طريقة قياس الأثر

        [StringLength(500)]
        public string? SupportDecision { get; set; }     // قرار الدعم المطلوب

        [StringLength(200)]
        public string? Escalation { get; set; }          // مستوى الرفع
        public bool IncludeInInstitutionalReport { get; set; }
        public bool IncludeInImpactDashboard { get; set; }
        public bool IncludeInExecutiveSummary { get; set; }

        public DateTime? ReviewedDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}