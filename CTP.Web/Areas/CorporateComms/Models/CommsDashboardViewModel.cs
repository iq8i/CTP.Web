namespace CTP.Web.Areas.CorporateComms.Models
{
    public class CommsDashboardViewModel
    {
        // ═══ KPIs ═══
        public int TotalTemplates { get; set; } = 10;
        public int ActiveChanges { get; set; }          // ← KPI (int)
        public int PublishedAnnouncements { get; set; }
        public int DraftMessages { get; set; }

        // ═══ حزمة القوالب ═══
        public List<TemplateItem> Templates { get; set; } = new();

        // ═══ التغييرات الجارية (تمّ تغيير الاسم) ═══
        public List<ActiveChangeItem> ActiveChangesList { get; set; } = new();  // ← جديد

        // ═══ الإعلانات المنشورة ═══
        public List<AnnouncementItem> PublishedAnnouncementsList { get; set; } = new();  // ← جديد

        // ═══ إرشادات النشر ═══
        public List<string> PublishingGuidelines { get; set; } = new();
    }

    public class TemplateItem
    {
        public string Icon { get; set; } = "bi-file-text";
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = "primary";
        public string FileName { get; set; } = string.Empty;
    }

    public class ActiveChangeItem
    {
        public string ChangeName { get; set; } = string.Empty;
        public string ChangeType { get; set; } = string.Empty;
        public string CurrentStage { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string ApprovedDate { get; set; } = string.Empty;
    }

    public class AnnouncementItem
    {
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string StatusBadge { get; set; } = "bg-success";
    }
}