using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Services
{
    public interface IAnimalService
    {
        Task<bool> CreateAnimalAsync(AnimalPost animal, string user);

        Task<bool> PatchAnimalAsync(List<AnimalPatch> animalPatch, string user, string animal);
    }
}