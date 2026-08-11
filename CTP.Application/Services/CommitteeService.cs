using CTP.Application.Interfaces.Repositories;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;

namespace CTP.Application.Services
{
    public class CommitteeService : ICommitteeService
    {
        private readonly ICommitteeRepository _repository;

        public CommitteeService(ICommitteeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Committee>> GetDashboardCommitteesAsync()
        {
            // يمكنك إضافة قواعد أعمال (Business Logic) هنا مستقبلاً
            return await _repository.GetAllActiveAsync();
        }
    }
}