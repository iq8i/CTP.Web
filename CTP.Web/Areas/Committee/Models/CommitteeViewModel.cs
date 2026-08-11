namespace CTP.Web.Areas.Committee.Models
{
    public class CommitteeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CreatedDateFormatted { get; set; } = string.Empty;
    }
}