using CTP.Application.Interfaces.Repositories;
using CTP.Domain.Entities;
using CTP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CTP.Infrastructure.Repositories
{
    public class CommitteeRepository : ICommitteeRepository
    {
        private readonly ApplicationDbContext _context;

        public CommitteeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Committee>> GetAllActiveAsync()
        {
            return await _context.Committees
                .Where(c => c.IsActive)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }
    }
}