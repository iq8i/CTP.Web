using CTP.Application.Interfaces.Repositories;
using CTP.Application.Interfaces.Services;
using CTP.Domain.Entities;

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
    }
}