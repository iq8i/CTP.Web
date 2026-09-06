using CTP.Domain.Entities;

namespace CTP.Web.Areas.Committee.Models
{
    public class ChairBoardViewModel
    {
        public List<Recommendation> PendingRecommendations { get; set; } = new();
        public List<Recommendation> ApprovedRecommendations { get; set; } = new();
    }
}
