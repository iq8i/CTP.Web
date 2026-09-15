using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Services
{
    public interface IImpactMeasurementService
    {
        Task<ImpactMeasurement?> GetByIdAsync(int id);
        Task<ImpactMeasurement?> GetByRecommendationIdAsync(int recommendationId);
        Task<ImpactMeasurement> SaveAsync(ImpactMeasurement measurement);
        Task<ImpactSummaryResult> GetSummaryAsync();
    }

    public class ImpactSummaryResult
    {
        public int TotalMeasurements { get; set; }
        public int VerifiedCount { get; set; }        // تحقق
        public int PartialCount { get; set; }         // جزئي
        public int NotVerifiedCount { get; set; }     // لم يتحقق بعد
        public double AvgDelta { get; set; }          // متوسط التحسن
        public int PendingCount { get; set; }         // بانتظار القياس (من التوصيات)
        public List<ImpactMeasurement> AllMeasurements { get; set; } = new();
    }
}