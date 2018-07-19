using System;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IAnimalManagementService
    {
        Task HungryIncreaseAsync(Guid id);

        Task HapinessDecreaseAsync(Guid id);
    }
}