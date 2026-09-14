using CTP.Domain.Entities;
using CTP.Domain.Enums;

namespace CTP.Application.Interfaces.Services
{
    public interface IEntityInputService
    {
        Task<EntityInput> SubmitInputAsync(EntityInput input);
        Task<IEnumerable<EntityInput>> GetCommitteeInputsAsync();
        Task<EntityInput?> GetInputByIdAsync(int id);
        Task<bool> UpdateInputStatusAsync(int inputId, InputStatus newStatus);
    }
}