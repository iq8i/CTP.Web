using System.ComponentModel.DataAnnotations;

namespace CTP.Web.Areas.Ambassador.Models
{
    public class EditMonthlyReportViewModel : CreateMonthlyReportViewModel
    {
        public int Id { get; set; }
        public string ReportNumber { get; set; } = string.Empty;
        public string? ApproverNotes { get; set; } // لعرض ملاحظات القائد للسفير
        public string? InitialRecommendation { get; set; } // حل خطأ CS1061
    }
}