using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Options;
using Microsoft.Extensions.Options;
using Serilog;

namespace Api.Repositories.Implementation
{
    public class AnimalRepository : BaseRepository<Animal>, IAnimalRepository
    {
        public AnimalRepository(IOptions<DatabaseOptions> databaseOptions) :
            base(databaseOptions.Value.CompanionTownConnectionString, databaseOptions.Value.AnimalsCollection)
        {
        }

        public Task<List<Animal>> GetAsync(string user)
        {
            var result = new List<Animal>();

            try
            {
                result = this.Database.Find(_ => _.User == user).ToList();

                return Task.Run(() => result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"On {nameof(GetAsync)} list");

                return null;
            }
        }

        public Task<Animal> GetAsync(Guid id)
        {
            try
            {
                var result = this.Database.Find(_ => _.Id == id).FirstOrDefault();

                return Task.Run(() => result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"On {nameof(InsertAsync)}");

                return null;
            }
        }

        public Task<Animal> GetAsync(string identifier, string user)
        {
            try
            {
                var result = this.Database.Find(_ => _.Identifier == identifier && _.User == user).FirstOrDefault();

                return Task.Run(() => result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"On {nameof(InsertAsync)}");

                return null;
            }
        }

        public Task<bool> InsertAsync(Animal animal)
        {
            try
            {
                this.Database.Insert(animal);

                return Task.Run(() => true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"On {nameof(InsertAsync)}");

                return Task.Run(() => false);
            }
        }

        public Task<bool> UpdateAsync(Animal animal)
        {
            try
            {
                animal.LastUpdate = DateTime.Now;

                this.Database.Update(animal);

                return Task.Run(() => true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"On {nameof(InsertAsync)}");

                return Task.Run(() => false);
            }
        }
    }
}