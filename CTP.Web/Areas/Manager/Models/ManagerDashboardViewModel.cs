namespace CTP.Web.Areas.Manager.Models
{
    public class ManagerDashboardViewModel
    {
        public int TeamSize { get; set; } = 12;
        public int ActiveChangesCount { get; set; }
        public int MaterialsCount { get; set; } = 3;

        public List<CTP.Web.Areas.Staff.Models.ChangeCard> TeamChanges { get; set; } = new();

        public List<SupportToolItem> SupportTools { get; set; } = new();
    }

    public class SupportToolItem
    {
        public string Icon { get; set; } = "bi-tools";
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = "#";
        public string Color { get; set; } = "primary";
    }
}