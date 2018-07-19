using System;

namespace Api.Models
{
    public class Animal
    {
        public Animal()
        {
        }

        public Animal(string name, string user, AnimalType animalType, int defaultHappy = 1, int defaultHungry = 1)
        {
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

        public int DefaultHappy { get; }

        public int DefaultHungry { get; }

        public string User { get; set; }

        public DateTime CreateDate { get; }

        public DateTime LastUpdate { get; set; }

        public AnimalType Type { get; set; }

        public bool Alive { get; set; } = true;
    }
}