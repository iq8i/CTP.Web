using System.ComponentModel.DataAnnotations;

namespace CTP.Web.Areas.Ambassador.Models
{
    public class CreateMonthlyReportViewModel
    {
        [Required(ErrorMessage = "سنة التقرير مطلوبة")]
        public int Year { get; set; } = DateTime.Now.Year;

        [Required(ErrorMessage = "شهر التقرير مطلوب")]
        public string Month { get; set; } = string.Empty;

        [Required(ErrorMessage = "اسم التغيير مطلوب")]
        public string ChangeName { get; set; } = string.Empty;

        public string ChangeType { get; set; } = string.Empty;
        public string CurrentStage { get; set; } = string.Empty;
        public string? ChangeSummary { get; set; }

        [Range(0, 100)] public int AdkarAwareness { get; set; }
        [Range(0, 100)] public int AdkarDesire { get; set; }
        [Range(0, 100)] public int AdkarKnowledge { get; set; }
        [Range(0, 100)] public int AdkarAbility { get; set; }
        [Range(0, 100)] public int AdkarReinforcement { get; set; }

        public string? Obstacles { get; set; }
        public string? InitialRecommendation { get; set; }
        public string? EvidenceLinks { get; set; }

        // أكشن الحفظ (Draft أو Submit)
        public string ActionType { get; set; } = "Draft";
    }
}