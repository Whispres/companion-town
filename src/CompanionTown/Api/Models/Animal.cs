using System;

namespace Api.Models
{
    // hack : if I know the types of animals I can transform this into a abstract class
    public class Animal
    {
        public Animal(int defaultHappy = 1, int defaultHungry = 1)
        {
            this.DefaultHappy = defaultHappy;
            this.DefaultHungry = defaultHungry;
            this.CreateDate = DateTime.Now;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Hapiness { get; set; }

        public int Hungry { get; set; }

        public int DefaultHappy { get; }

        public int DefaultHungry { get; } //  todo : check if is start or the increment

        public int PersonId { get; set; }

        public DateTime CreateDate { get; }

        public bool Alive { get; set; } = true;
    }
}