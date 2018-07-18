using System.Threading.Tasks;
using Api.Models;

namespace Api.Repositories
{
    public interface IAnimalRepository
    {
        Task<bool> Insert(Animal animal);

        Task<bool> Update(Animal animal);

        Task<Animal> Get(string name);

        Task<PagedResult<Animal>> GetPaged(int skip, int limit);
    }
}