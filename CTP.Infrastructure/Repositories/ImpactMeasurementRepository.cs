using CTP.Application.Interfaces.Repositories;
using CTP.Domain.Entities;
using CTP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CTP.Infrastructure.Repositories
{
    public class ImpactMeasurementRepository : IImpactMeasurementRepository
    {
        private readonly ApplicationDbContext _context;

        public ImpactMeasurementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ImpactMeasurement?> GetByIdAsync(int id)
            => await _context.ImpactMeasurements
                .Include(m => m.Recommendation)
                    .ThenInclude(r => r!.SourceReport)
                        .ThenInclude(sr => sr!.Organization)
                .FirstOrDefaultAsync(m => m.Id == id);

        public async Task<ImpactMeasurement?> GetByRecommendationIdAsync(int recommendationId)
            => await _context.ImpactMeasurements
                .Include(m => m.Recommendation)
                    .ThenInclude(r => r!.SourceReport)
                        .ThenInclude(sr => sr!.Organization)
                .FirstOrDefaultAsync(m => m.RecommendationId == recommendationId);

        public async Task<IEnumerable<ImpactMeasurement>> GetAllAsync()
            => await _context.ImpactMeasurements
                .Include(m => m.Recommendation)
                    .ThenInclude(r => r!.SourceReport)
                        .ThenInclude(sr => sr!.Organization)
                .OrderByDescending(m => m.CreatedDate)
                .ToListAsync();

        public async Task<ImpactMeasurement> AddAsync(ImpactMeasurement measurement)
        {
            await _context.ImpactMeasurements.AddAsync(measurement);
            return measurement;
        }

        public Task UpdateAsync(ImpactMeasurement measurement)
        {
            _context.ImpactMeasurements.Update(measurement);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}