using CTP.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [ValidateNever] public MonthlyReport ReportDetails { get; set; } = new MonthlyReport();

        [Required] public int MonthlyReportId { get; set; }

        [Required(ErrorMessage = "مطلوب تحديد التشخيص الجذري")]
        public string WhatHappened { get; set; } = string.Empty;

        [Required(ErrorMessage = "مطلوب تحديد لماذا تشكل الفجوة خطراً")]
        public string WhyItMatters { get; set; } = string.Empty;

        [Required(ErrorMessage = "مطلوب تحديد التدخل أو الإجراء التصحيحي")]
        public string WhatNow { get; set; } = string.Empty;

        [Required(ErrorMessage = "مطلوب كتابة خلاصة التوصية")]
        public string SuggestedAction { get; set; } = string.Empty;

        [Required(ErrorMessage = "مطلوب تحديد الجهة المالكة للحل")]
        public string Owner { get; set; } = string.Empty;

        public string ExpectedImpact { get; set; } = string.Empty;
        public string Priority { get; set; } = "متوسطة";
        public bool RequiresSupport { get; set; }
    }

    // 3. نموذج التوصية اليدوية (خاص برئيس اللجنة)
    public class ManualRecommendationViewModel
    {
        [Required(ErrorMessage = "يرجى اختيار التقرير المصدر")] public int MonthlyReportId { get; set; }
        [Required(ErrorMessage = "يرجى تحديد الفجوة")] public string GapType { get; set; } = string.Empty;
        [Required(ErrorMessage = "الإجراء المقترح إلزامي")] public string SuggestedAction { get; set; } = string.Empty;
        [Required(ErrorMessage = "مالك الإجراء إلزامي")] public string ActionOwner { get; set; } = string.Empty;
        [Required(ErrorMessage = "المدة إلزامية")] public string Duration { get; set; } = string.Empty;
        [Required(ErrorMessage = "مؤشر النجاح إلزامي")] public string SuccessIndicator { get; set; } = string.Empty;
        public string ExpectedImpact { get; set; } = string.Empty;
    }

    // 4. نماذج التقرير المؤسسي (Dashboard)
    public class InstitutionalReportViewModel
    {
        public string ReportMonth { get; set; } = string.Empty;
        public int TotalReports { get; set; }
        public int TotalEntities { get; set; }
        public int AverageReadiness { get; set; }
        public int AverageAdoption { get; set; }
        public int AverageReinforcement { get; set; }
        public List<ParetoItem> TopBarriers { get; set; } = new();
        public List<Recommendation> ApprovedRecommendations { get; set; } = new();
    }

    public class ParetoItem
    {
        public string Barrier { get; set; } = string.Empty;
        public int Count { get; set; }
        public int Percentage { get; set; }
        public int CumulativePercentage { get; set; }
    }

}