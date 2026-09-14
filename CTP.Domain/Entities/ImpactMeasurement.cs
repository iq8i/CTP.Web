using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTP.Domain.Entities
{
    /// <summary>
    /// قياس الأثر قبل/بعد لكل توصية معتمدة
    /// </summary>
    public class ImpactMeasurement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RecommendationId { get; set; }
        [ForeignKey("RecommendationId")]
        public virtual Recommendation? Recommendation { get; set; }

        [Required, StringLength(255)]
        public string IndicatorName { get; set; } = string.Empty;   // اسم المؤشر

        [Range(0, 100)]
        public int BeforeValue { get; set; }                         // القيمة قبل

        [Range(0, 100)]
        public int AfterValue { get; set; }                          // القيمة بعد

        public int Delta => AfterValue - BeforeValue;                // الفرق

        [StringLength(20)]
        public string Status { get; set; } = "لم يتحقق بعد";          // تحقق / جزئي / لم يتحقق بعد

        [StringLength(500)]
        public string? ImprovementReason { get; set; }               // سبب عدم التحسن

        [StringLength(500)]
        public string? NextStep { get; set; }                        // الخطوة التالية

        [StringLength(500)]
        public string? Lesson { get; set; }                          // الدرس المستفاد

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? MeasuredDate { get; set; }                  // تاريخ القياس
    }
}