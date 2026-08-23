using CTP.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace CTP.Web.Areas.Committee.Models
{
    public class CommitteeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CreatedDateFormatted { get; set; } = string.Empty;
    }
    // نموذج شاشة التحليل
    public class AnalyzeReportViewModel
    {
        public MonthlyReport ReportDetails { get; set; } = new MonthlyReport();

        [Required(ErrorMessage = "هذا الحقل إلزامي")]
        public int MonthlyReportId { get; set; }

        [Required(ErrorMessage = "مطلوب تحديد ماذا حدث")]
        public string WhatHappened { get; set; } = string.Empty;

        [Required(ErrorMessage = "مطلوب تحديد لماذا يهم (الفجوة)")]
        public string WhyItMatters { get; set; } = string.Empty;

        [Required(ErrorMessage = "مطلوب تحديد التدخل (ماذا نفعل الآن)")]
        public string WhatNow { get; set; } = string.Empty;

        public string ExpectedImpact { get; set; } = string.Empty;
        public string Priority { get; set; } = "متوسطة";
        public string Owner { get; set; } = string.Empty;
        public string SuggestedAction { get; set; } = string.Empty;
        public bool RequiresSupport { get; set; }
    }
}