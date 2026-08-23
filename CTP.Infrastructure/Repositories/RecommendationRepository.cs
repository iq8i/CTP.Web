using CTP.Application.Interfaces.Repositories;
using CTP.Domain.Entities;
using CTP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CTP.Infrastructure.Repositories
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly ApplicationDbContext _context;

        public RecommendationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Recommendation> AddAsync(Recommendation recommendation)
        {
            await _context.Recommendations.AddAsync(recommendation);
            return recommendation;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Recommendation>> GetAllWithDetailsAsync()
        {
            return await _context.Recommendations
                .Include(r => r.SourceReport)
                    .ThenInclude(sr => sr.Organization)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();
        }
    }
}