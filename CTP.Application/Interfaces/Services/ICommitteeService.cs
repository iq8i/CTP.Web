using CTP.Domain.Entities;

namespace CTP.Application.Interfaces.Services
{
    public interface ICommitteeService
    {
        Task<IEnumerable<Committee>> GetDashboardCommitteesAsync();
    }
}