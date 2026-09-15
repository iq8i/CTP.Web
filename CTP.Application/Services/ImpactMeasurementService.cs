using CTP.Application.Interfaces.Repositories;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;

namespace CTP.Application.Services
{
    public class ImpactMeasurementService : IImpactMeasurementService
    {
        private readonly IImpactMeasurementRepository _repository;

        public ImpactMeasurementService(IImpactMeasurementRepository repository)
        {
            _repository = repository;
        }

        public async Task<ImpactMeasurement?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<ImpactMeasurement?> GetByRecommendationIdAsync(int recommendationId)
            => await _repository.GetByRecommendationIdAsync(recommendationId);

        public async Task<ImpactMeasurement> SaveAsync(ImpactMeasurement measurement)
        {
            // حساب الحالة تلقائياً بناءً على الفرق
            var delta = measurement.AfterValue - measurement.BeforeValue;
            measurement.Status = delta switch
            {
                >= 10 => "تحقق",
                >= 1 => "جزئي",
                _ => "لم يتحقق بعد"
            };

            // إذا كانت القيمة بعد > 0 نضع تاريخ القياس
            if (measurement.AfterValue > 0 && !measurement.MeasuredDate.HasValue)
                measurement.MeasuredDate = DateTime.Now;

            var existing = await _repository.GetByRecommendationIdAsync(measurement.RecommendationId);

            if (existing == null)
            {
                await _repository.AddAsync(measurement);
            }
            else
            {
                existing.IndicatorName = measurement.IndicatorName;
                existing.BeforeValue = measurement.BeforeValue;
                existing.AfterValue = measurement.AfterValue;
                existing.Status = measurement.Status;
                existing.ImprovementReason = measurement.ImprovementReason;
                existing.NextStep = measurement.NextStep;
                existing.Lesson = measurement.Lesson;
                existing.MeasuredDate = measurement.MeasuredDate;
                await _repository.UpdateAsync(existing);
                measurement = existing;
            }

            await _repository.SaveChangesAsync();
            return measurement;
        }

        public async Task<ImpactSummaryResult> GetSummaryAsync()
        {
            var all = (await _repository.GetAllAsync()).ToList();

            return new ImpactSummaryResult
            {
                TotalMeasurements = all.Count,
                VerifiedCount = all.Count(m => m.Status == "تحقق"),
                PartialCount = all.Count(m => m.Status == "جزئي"),
                NotVerifiedCount = all.Count(m => m.Status == "لم يتحقق بعد"),
                AvgDelta = all.Any() ? Math.Round(all.Average(m => (double)m.Delta), 1) : 0,
                AllMeasurements = all
            };
        }
    }
}