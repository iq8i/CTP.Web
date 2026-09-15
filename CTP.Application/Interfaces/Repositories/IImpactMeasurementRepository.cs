using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Repositories
{
    public interface IImpactMeasurementRepository
    {
        Task<ImpactMeasurement?> GetByIdAsync(int id);
        Task<ImpactMeasurement?> GetByRecommendationIdAsync(int recommendationId);
        Task<IEnumerable<ImpactMeasurement>> GetAllAsync();
        Task<ImpactMeasurement> AddAsync(ImpactMeasurement measurement);
        Task UpdateAsync(ImpactMeasurement measurement);
        Task<int> SaveChangesAsync();
    }
}