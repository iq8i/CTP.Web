namespace CTP.Web.Areas.Staff.Models
{
    public class StaffDashboardViewModel
    {
        // ═══ KPIs ═══
        public int ActiveChangesCount { get; set; }
        public int EducationalMaterialsCount { get; set; }
        public int InquiryChannelsCount { get; set; }

        // ═══ التغييرات الجارية ═══
        public List<ChangeCard> ActiveChanges { get; set; } = new();

        // ═══ المواد التعليمية ═══
        public List<MaterialCard> Materials { get; set; } = new();

        // ═══ قنوات الاستفسار ═══
        public List<ChannelCard> Channels { get; set; } = new();
    }

    public class ChangeCard
    {
        public int Id { get; set; }
        public string ChangeName { get; set; } = string.Empty;
        public string ChangeType { get; set; } = string.Empty;
        public string CurrentStage { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string WhatWillChange { get; set; } = string.Empty;
        public string WhatWillNotChange { get; set; } = string.Empty;
        public string AmbassadorName { get; set; } = string.Empty;
    }

    public class MaterialCard
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = "bi-book";
        public string Color { get; set; } = "primary";
        public string Url { get; set; } = "#";
        public string StatusBadge { get; set; } = "متاح";
        public string StatusClass { get; set; } = "bg-success";
    }

    public class ChannelCard
    {
        public string Icon { get; set; } = "bi-chat-dots";
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
    }
}