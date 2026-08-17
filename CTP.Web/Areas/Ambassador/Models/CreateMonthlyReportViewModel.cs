using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CTP.Web.Areas.Ambassador.Models
{
    public class CreateMonthlyReportViewModel
    {
        // تعيين السنة الهجرية الحالية كافتراضي
        [Required(ErrorMessage = "سنة التقرير مطلوبة")]
        public int Year { get; set; } = new UmAlQuraCalendar().GetYear(DateTime.Now);

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

        public string ActionType { get; set; } = "Draft";
    }
}