using CTP.Domain.Entities;
namespace CTP.Application.Interfaces.Services
{
    public interface IEntityInputService { Task<EntityInput> SubmitInputAsync(EntityInput input); }
}