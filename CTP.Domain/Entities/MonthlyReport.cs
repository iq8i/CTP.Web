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
        public string ReportNumber { get; set; } = string.Empty;

        [Required]
        public int Year { get; set; }

        [Required, StringLength(20)]
        public string Month { get; set; } = string.Empty;

        // الربط بالجهة والسفير
        [Required]
        public int OrganizationEntityId { get; set; }
        [ForeignKey("OrganizationEntityId")]
        public virtual OrganizationEntity? Organization { get; set; }

        [Required]
        public int PreparerId { get; set; }
        [ForeignKey("PreparerId")]
        public virtual User? Preparer { get; set; }

        // بيانات التغيير ونطاق الأثر
        [Required, StringLength(255)]
        public string ChangeName { get; set; } = string.Empty;

        [StringLength(50)]
        public string ChangeType { get; set; } = string.Empty; // تنظيمي، إجرائي، تقني، خدمي، ثقافي

        [StringLength(50)]
        public string CurrentStage { get; set; } = string.Empty; // تعريف، تهيئة، تطبيق، تثبيت

        [StringLength(255)]
        public string? AffectedGroups { get; set; } // الفئات المتأثرة

        [StringLength(100)]
        public string? ImpactScope { get; set; } // نطاق الأثر (الجهة، الإدارة، المنظومة)

        [StringLength(255)]
        public string? MostInNeedGroup { get; set; } // الفئة الأكثر احتياجاً للدعم

        [Range(0, 100)]
        public int ActivityCompletionRate { get; set; } // نسبة اكتمال الأنشطة (%)

        // تفاصيل التغيير التحليلية
        public string? ChangeSummary { get; set; }
        public string? WhyImportant { get; set; }
        public string? WhatWillChange { get; set; }
        public string? WhatWillNotChange { get; set; }
        public string? ExecutedActivities { get; set; }

        // مؤشرات ADKAR ومحرك الاحتساب
        [Range(0, 100)] public int AdkarAwareness { get; set; }
        [Range(0, 100)] public int AdkarDesire { get; set; }
        [Range(0, 100)] public int AdkarKnowledge { get; set; }
        [Range(0, 100)] public int AdkarAbility { get; set; }
        [Range(0, 100)] public int AdkarReinforcement { get; set; }

        [Range(0, 100)] public int ReadinessScore { get; set; }
        [Range(0, 100)] public int AdoptionScore { get; set; }

        // التحديات، المخاطر، والدعم
        public string? Obstacles { get; set; }
        public string? AdoptionBarriers { get; set; }
        public string? RequiredSupport { get; set; }
        public string? Risks { get; set; }
        public string? ImprovementOpportunities { get; set; }
        public string? SuccessStories { get; set; }
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