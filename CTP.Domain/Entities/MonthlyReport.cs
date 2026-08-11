using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CTP.Domain.Enums;

namespace CTP.Domain.Entities
{
    public class MonthlyReport
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string ReportNumber { get; set; } = string.Empty; // مثل: CTP-2026-07-001

        [Required]
        public int Year { get; set; }

        [Required, StringLength(20)]
        public string Month { get; set; } = string.Empty;

        // الربط مع الجهة (الوحدة/الهيئة/الإدارة)
        [Required]
        public int OrganizationEntityId { get; set; }
        [ForeignKey("OrganizationEntityId")]
        public virtual OrganizationEntity? Organization { get; set; }

        // الربط مع معد التقرير (سفير التغيير)
        [Required]
        public int PreparerId { get; set; }
        [ForeignKey("PreparerId")]
        public virtual User? Preparer { get; set; }

        // بيانات التغيير
        [Required, StringLength(255)]
        public string ChangeName { get; set; } = string.Empty;

        [StringLength(50)]
        public string ChangeType { get; set; } = string.Empty; // إجرائي، تقني، تنظيمي

        [StringLength(50)]
        public string CurrentStage { get; set; } = string.Empty; // تعريف، تهيئة، تطبيق

        public string? ChangeSummary { get; set; }

        // مؤشرات الأداء (من 0 إلى 100)
        [Range(0, 100)] public int ReadinessScore { get; set; }
        [Range(0, 100)] public int AdoptionScore { get; set; }

        // ADKAR Metrics
        [Range(0, 100)] public int AdkarAwareness { get; set; }
        [Range(0, 100)] public int AdkarDesire { get; set; }
        [Range(0, 100)] public int AdkarKnowledge { get; set; }
        [Range(0, 100)] public int AdkarAbility { get; set; }
        [Range(0, 100)] public int AdkarReinforcement { get; set; }

        // التحديات والتوصيات الأولية
        public string? Obstacles { get; set; }
        public string? InitialRecommendation { get; set; }

        // إدارة الحالة والاعتماد
        public ReportStatus Status { get; set; } = ReportStatus.Draft;

        public string? ApproverNotes { get; set; }
        public string? EvidenceLinks { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? SubmittedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}