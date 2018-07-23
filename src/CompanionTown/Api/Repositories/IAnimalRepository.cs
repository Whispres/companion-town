using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Repositories
{
    public interface IAnimalRepository
    {
        Task<bool> InsertAsync(Animal animal);

        Task<bool> UpdateAsync(Animal animal);

        Task<Animal> GetAsync(Guid id);

        Task<Animal> GetAsync(string identifier, string user);

        Task<List<Animal>> GetAsync(string user);
    }
}