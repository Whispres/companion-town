using System.Threading.Tasks;
using Api.Models;

namespace Api.Services
{
    public interface IAnimalService
    {
        Task<bool> CreateAnimalAsync(AnimalViewModel animal, string user);
    }
}