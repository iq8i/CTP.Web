using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Repositories
{
    public interface ICommitteeRepository
    {
        Task<IEnumerable<Committee>> GetAllActiveAsync();
    }
}