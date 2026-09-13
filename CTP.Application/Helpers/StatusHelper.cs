using CTP.Domain.Enums;

namespace CTP.Application.Helpers
{
    public static class StatusHelper
    {
        public static string ToArabic(this RecommendationStatus status) => status switch
        {
            RecommendationStatus.RequiresCompletion => "تحتاج استكمال",
            RecommendationStatus.AwaitingSupport => "بانتظار قرار الدعم",
            RecommendationStatus.ReadyForChair => "جاهزة لاعتماد الرئيس",
            RecommendationStatus.Approved => "معتمدة",
            RecommendationStatus.Rejected => "مرفوضة",
            RecommendationStatus.SupersededBySovereign => "مستبدلة بتوصية سيادية",
            _ => "غير معروفة"
        };

        public static string ToBadgeClass(this RecommendationStatus status) => status switch
        {
            RecommendationStatus.Approved => "bg-success",
            RecommendationStatus.Rejected => "bg-danger",
            RecommendationStatus.ReadyForChair => "bg-warning text-dark",
            RecommendationStatus.AwaitingSupport => "bg-info text-dark",
            RecommendationStatus.RequiresCompletion => "bg-secondary",
            RecommendationStatus.SupersededBySovereign => "bg-dark",
            _ => "bg-secondary"
        };
    }
}