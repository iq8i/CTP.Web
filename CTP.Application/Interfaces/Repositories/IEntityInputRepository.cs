using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Repositories
{
    public interface IEntityInputRepository
    {
        Task<EntityInput> AddAsync(EntityInput input);
        Task<int> SaveChangesAsync();
        Task<IEnumerable<EntityInput>> GetAllPendingAsync();

        Task<EntityInput?> GetByIdAsync(int id);
    }
}