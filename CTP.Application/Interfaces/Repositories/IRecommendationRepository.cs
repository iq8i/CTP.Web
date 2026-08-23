using CTP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTP.Application.Interfaces.Repositories
{
    public interface IRecommendationRepository
    {
        Task<Recommendation> AddAsync(Recommendation recommendation);
        Task<int> SaveChangesAsync();

        // السطر المفقود الذي يحل خطأ CS1061
        Task<IEnumerable<Recommendation>> GetAllWithDetailsAsync();
    }
}