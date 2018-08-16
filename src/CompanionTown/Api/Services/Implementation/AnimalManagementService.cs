using System;
using System.Threading.Tasks;
using Api.Repositories;
using Hangfire;
using Serilog;

namespace Api.Services.Implementation
{
    public class AnimalManagementService : IAnimalManagementService
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalManagementService(IAnimalRepository animalRepository)
        {
            this._animalRepository = animalRepository;
        }

        public async Task HapinessDecreaseAsync(Guid id)
        {
            try
            {
                var animal = await this._animalRepository.GetAsync(id);

                animal.Hapiness -= animal.DefaultHappy;

                if (animal.Hapiness < 0)
                {
                    animal.Hapiness = 0;

                    RecurringJob.RemoveIfExists($"HAPINESS-{id.ToString()}");
                }

                await this._animalRepository.UpdateAsync(animal);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Error processing {nameof(HapinessDecreaseAsync)}");
            }
        }

        public async Task HungryIncreaseAsync(Guid id)
        {
            try
            {
                var animal = await this._animalRepository.GetAsync(id);

                animal.Hungry += animal.DefaultHungry;

                if (animal.Hungry > 100)
                {
                    animal.Hungry = 101;
                    animal.Alive = false;

                    RecurringJob.RemoveIfExists($"HUNGRY-{id.ToString()}");
                }

                await this._animalRepository.UpdateAsync(animal);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Error processing {nameof(HungryIncreaseAsync)}");
            }
        }
    }
}