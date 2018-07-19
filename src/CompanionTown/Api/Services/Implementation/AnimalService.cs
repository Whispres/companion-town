using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Exceptions;
using Api.Models;
using Api.Options;
using Api.Repositories;
using Hangfire;
using Microsoft.Extensions.Options;

namespace Api.Services.Implementation
{
    public class AnimalService : IAnimalService
    {
        private readonly AnimalJobOptions _animalJobOptions;
        private readonly IAnimalRepository _animalRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAnimalManagementService _animalManagementService;

        public AnimalService(
            IOptions<AnimalJobOptions> animalJobOptions,
            IAnimalRepository animalRepository,
            IUserRepository userRepository,
            IAnimalManagementService animalManagementService)
        {
            this._animalJobOptions = animalJobOptions.Value;
            this._animalRepository = animalRepository;
            this._userRepository = userRepository;
            this._animalManagementService = animalManagementService;
        }

        public async Task<bool> CreateAnimalAsync(AnimalViewModel animalModel, string userName)
        {
            try
            {
                var user = await this._userRepository.GetAsync(userName);

                if (user == null)
                {
                    throw new NotFoundException("User");
                }

                var animal = this.GenerateAnimal(animalModel, userName);

                if (animal.Type == AnimalType.Undefined)
                {
                    throw new BadRequestException("Invalid type");
                }

                var animalsOfTheUser = await this._animalRepository.GetAsync(animal.User);

                if (animalsOfTheUser.Any(_ => _.Name == animal.Name))
                {
                    throw new NotModifiedException("Already exists");
                }

                var isSaved = await this._animalRepository.InsertAsync(animal);

                if (isSaved)
                {
                    RecurringJob.AddOrUpdate<IAnimalManagementService>(
                        $"HAPINESS-{animal.Id.ToString()}",
                        _ => _.HapinessDecreaseAsync(animal.Id),
                        Cron.MinuteInterval(_animalJobOptions.MinutesForHapiness));

                    RecurringJob.AddOrUpdate<IAnimalManagementService>(
                        $"HUNGRY-{animal.Id.ToString()}",
                        _ => _.HungryIncreaseAsync(animal.Id),
                        Cron.MinuteInterval(_animalJobOptions.MinutesForHungry));
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Animal GenerateAnimal(AnimalViewModel animalViewModel, string userName)
        {
            switch (animalViewModel.Type)
            {
                case AnimalType.Undefined:
                    return new Animal();

                case AnimalType.Dog:
                    return new Animal(animalViewModel.Name, userName, animalViewModel.Type, 5, 2);

                case AnimalType.Cat:
                    return new Animal(animalViewModel.Name, userName, animalViewModel.Type, 3, 3);

                case AnimalType.Parrot:
                    return new Animal(animalViewModel.Name, userName, animalViewModel.Type, 4, 1);

                default:
                    return new Animal();
            }
        }
    }
}