using CTP.Application.Interfaces.Repositories;
using CTP.Domain.Entities;
using CTP.Infrastructure.Data;

namespace CTP.Infrastructure.Repositories
{
    public class EntityInputRepository : IEntityInputRepository
    {
        private readonly ApplicationDbContext _context;
        public EntityInputRepository(ApplicationDbContext context) => _context = context;

        public async Task<EntityInput> AddAsync(EntityInput input)
        {
            await _context.EntityInputs.AddAsync(input);
            return input;
        }
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}