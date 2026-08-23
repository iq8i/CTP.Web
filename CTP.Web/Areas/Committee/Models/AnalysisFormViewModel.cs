using System.ComponentModel.DataAnnotations;

namespace CTP.Web.Areas.Committee.Models
{
    public class AnalysisFormViewModel
    {
        [Required]
        public int MonthlyReportId { get; set; }

        [Required]
        public string WhatHappened { get; set; } = string.Empty; // ماذا حدث؟ (What)

        [Required]
        public string WhyItMatters { get; set; } = string.Empty; // لماذا يهم؟ (So What - الفجوة)

        [Required]
        public string WhatNow { get; set; } = string.Empty; // ماذا نفعل الآن؟ (Now What - التدخل)

        public string ExpectedImpact { get; set; } = string.Empty; // الأثر المتوقع
        public string Priority { get; set; } = "متوسطة"; // الأولوية
        public string Owner { get; set; } = string.Empty; // المالك
        public string SuggestedAction { get; set; } = string.Empty; // الإجراء المقترح
        public bool RequiresSupport { get; set; } // هل يحتاج قرار دعم؟
    }
}