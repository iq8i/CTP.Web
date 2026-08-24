using CTP.Application.Interfaces.Repositories;
using CTP.Domain.Entities;
using CTP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<EntityInput>> GetAllPendingAsync()
        {
            return await _context.EntityInputs
                .Include(e => e.Organization)
                .Include(e => e.Preparer)
                .OrderByDescending(e => e.CreatedDate)
                .ToListAsync();
        }

        public async Task<EntityInput?> GetByIdAsync(int id)
        {
            return await _context.EntityInputs
                .Include(e => e.Organization)
                .Include(e => e.Preparer)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}