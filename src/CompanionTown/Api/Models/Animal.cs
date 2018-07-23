using System;
using Api.Extensions;

namespace Api.Models
{
    public class Animal
    {
        public Animal()
        {
        }

        public Animal(string name, string user, AnimalType animalType, int defaultHappy = 1, int defaultHungry = 1)
        {
            this.Name = name;
            this.User = user;
            this.DefaultHappy = defaultHappy;
            this.DefaultHungry = defaultHungry;
            this.Type = animalType;

            this.CreateDate = DateTime.Now;
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Hapiness { get; set; } = 100;

        public int Hungry { get; set; }

        public int DefaultHappy { get; private set; }

        public int DefaultHungry { get; private set; }

        public string User { get; set; }

        public DateTime CreateDate { get; private set; }

        public DateTime LastUpdate { get; set; }

        public AnimalType Type { get; set; }

        public bool Alive { get; set; } = true;

        public string Identifier
        {
            get
            {
                return this.Name.RemoveSpecialCharacters();
            }
        }
    }
}