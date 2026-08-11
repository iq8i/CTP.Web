namespace CTP.Domain.Enums
{
    public enum ReportStatus
    {
        Draft = 1,          // مسودة
        Submitted = 2,      // مرفوع (بانتظار اعتماد صاحب السعادة)
        UnderAnalysis = 3,  // تحت التحليل (لدى لجنة شركاء التغيير)
        Returned = 4,       // معاد للاستكمال
        Approved = 5        // معتمد نهائياً
    }
}