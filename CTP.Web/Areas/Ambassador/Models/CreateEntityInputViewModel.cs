using System.ComponentModel.DataAnnotations;
using CTP.Domain.Enums;

namespace CTP.Web.Areas.Ambassador.Models
{
    public class CreateEntityInputViewModel
    {
        [Required(ErrorMessage = "يرجى تحديد نوع المدخل")]
        public InputType Type { get; set; }

        [Required(ErrorMessage = "يرجى كتابة التفاصيل")]
        public string Content { get; set; } = string.Empty;
    }
}