using CTP.Application.Interfaces.Repositories;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;
using CTP.Domain.Enums;

namespace CTP.Application.Services
{
    public class EntityInputService : IEntityInputService
    {
        private readonly IEntityInputRepository _repository;
        public EntityInputService(IEntityInputRepository repository) => _repository = repository;

        public async Task<EntityInput> SubmitInputAsync(EntityInput input)
        {
            input.CreatedDate = DateTime.Now;
            input.Status = CTP.Domain.Enums.InputStatus.Received;
            await _repository.AddAsync(input);
            await _repository.SaveChangesAsync();
            return input;
        }
        public async Task<IEnumerable<EntityInput>> GetCommitteeInputsAsync()
        {
            return await _repository.GetAllPendingAsync();
        }
        public async Task<EntityInput?> GetInputByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<bool> UpdateInputStatusAsync(int inputId, InputStatus newStatus)
        {
            var input = await _repository.GetByIdAsync(inputId);
            if (input == null) return false;

            input.Status = newStatus;
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}