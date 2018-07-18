using System;
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

        public Task<Animal> Get(string name)
        {
            try
            {
                return Task.Run(() => this.Database.Find(_ => _.Name == name).FirstOrDefault());
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"On {nameof(Get)}");

                return null;
            }
        }

        public Task<PagedResult<Animal>> GetPaged(int skip, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(Animal animal)
        {
            try
            {
                this.Database.Insert(animal);

                return Task.Run(() => true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"On {nameof(Insert)}");

                return Task.Run(() => false);
            }
        }

        public Task<bool> Update(Animal animal)
        {
            try
            {
                this.Database.Update(animal);

                return Task.Run(() => true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"On {nameof(Insert)}");

                return Task.Run(() => false);
            }
        }
    }
}