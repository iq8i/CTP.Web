using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CTP.Web.Areas.Ambassador.Models
{
    public class CreateMonthlyReportViewModel
    {
        // 1. بيانات التقرير
        [Required(ErrorMessage = "سنة التقرير مطلوبة")]
        public int Year { get; set; } = new UmAlQuraCalendar().GetYear(DateTime.Now);

        [Required(ErrorMessage = "شهر التقرير مطلوب")]
        public string Month { get; set; } = string.Empty;

        // 2. بيانات التغيير ونطاق الأثر
        [Required(ErrorMessage = "اسم التغيير مطلوب")]
        [StringLength(255)]
        public string ChangeName { get; set; } = string.Empty;

        public string ChangeType { get; set; } = "تنظيمي";
        public string CurrentStage { get; set; } = "تعريف";
        public string? AffectedGroups { get; set; }
        public string ImpactScope { get; set; } = "الجهة";
        public string? MostInNeedGroup { get; set; }

        [Range(0, 100, ErrorMessage = "النسبة يجب أن تكون بين 0 و 100")]
        public int ActivityCompletionRate { get; set; } = 0;

        public string? ChangeSummary { get; set; }
        public string? WhyImportant { get; set; }
        public string? WhatWillChange { get; set; }
        public string? WhatWillNotChange { get; set; }
        public string? ExecutedActivities { get; set; }

        // 3. مؤشرات ADKAR
        [Range(0, 100)] public int AdkarAwareness { get; set; } = 0;
        [Range(0, 100)] public int AdkarDesire { get; set; } = 0;
        [Range(0, 100)] public int AdkarKnowledge { get; set; } = 0;
        [Range(0, 100)] public int AdkarAbility { get; set; } = 0;
        [Range(0, 100)] public int AdkarReinforcement { get; set; } = 0;

        // 4. التحديات والدعم
        public string? Obstacles { get; set; }
        public string? AdoptionBarriers { get; set; }
        public string? RequiredSupport { get; set; }
        public string? Risks { get; set; }
        public string? ImprovementOpportunities { get; set; }
        public string? SuccessStories { get; set; }

        public string ActionType { get; set; } = "Draft";
    }
}