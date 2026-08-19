namespace CTP.Web.Models
{
    public class ChangeCardDetailsViewModel
    {
        public int Id { get; set; }
        public string ChangeName { get; set; } = string.Empty;
        public string ChangeType { get; set; } = string.Empty;

        // الأسئلة الأساسية المستمدة من التقرير
        public string? ChangeSummary { get; set; }     // ما التغيير؟
        public string? WhyImportant { get; set; }      // لماذا هذا التغيير؟
        public string? WhatWillChange { get; set; }    // ماذا سيتغير؟
        public string? WhatWillNotChange { get; set; } // ماذا لن يتغير؟
        public string? AffectedGroups { get; set; }    // من الفئات المتأثرة؟
        public string? ExecutedActivities { get; set; } // الأنشطة / كيف نستعد؟

        // الدعم وقنوات التواصل
        public string AmbassadorName { get; set; } = string.Empty; // القناة الرسمية
    }
}