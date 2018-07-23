using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Services
{
    public interface IAnimalService
    {
        Task<List<Animal>> GetAsync(string user);

        Task<Animal> GetAsync(string name, string user);

        Task<bool> CreateAnimalAsync(AnimalPost animal, string user);

        Task<bool> PatchAnimalAsync(List<AnimalPatch> animalPatch, string user, string animal);
    }
}