namespace CTP.Web.Models.Navigation
{
    public class MenuSection
    {
        public string Title { get; set; } = string.Empty;
        public List<MenuItem> Items { get; set; } = new();
    }

    public class MenuItem
    {
        public string Title { get; set; } = string.Empty;
        public string IconClass { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}