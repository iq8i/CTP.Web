using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Repositories
{
    public interface IEntityInputRepository
    {
        Task<EntityInput> AddAsync(EntityInput input);
        Task<int> SaveChangesAsync();
    }
}