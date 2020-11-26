using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    /// <summary>
    /// Interface that all Animals must have
    /// </summary>
    public interface ICommunicate
    {
        string Name { get; set; }
        int MaxAge { get; set; }
        string Message { get; set; }
        void Speak();
        void Initialize(string Name);
    }


    /// <summary>
    /// All Animal objects implement ICommunicate
    /// </summary>
    public class Animal : ICommunicate
    {
        public string Name { get; set; }
        public int MaxAge { get; set; }
        public string Message { get; set; }

        public void Speak()
        {
            Console.WriteLine($"{Name} says {Message}, I can live to be {MaxAge}");
        }
        public void Initialize(string Name)
        {
            this.Name = Name;

            // Simulate going to database to retreive more complex initialization info
            AnimalInfo animal = AnimalInfoDatabase.Find(this.GetType().Name);
            MaxAge = animal.MaxAge;
            Message = animal.Message;
        }
    }

    /// <summary>
    /// Derive any specific animal from base class
    /// </summary>
    public class Cat : Animal
    {
    }

    /// <summary>
    /// Derive any specific animal from base class
    /// </summary>
    public class Dog : Animal
    {
    }

    public class AnimalInfo
    {
        public string AnimalType { get; set; }
        public string Message { get; set; }
        public int MaxAge { get; set; }
    }

    public static class AnimalInfoDatabase
    {
        static List<AnimalInfo> AnimalRecords = new List<AnimalInfo>();
        static AnimalInfoDatabase()
        {
            AnimalRecords.Add(new AnimalInfo { AnimalType = "Cat", Message = "Meaow", MaxAge = 20 });
            AnimalRecords.Add(new AnimalInfo { AnimalType = "Dog", Message = "Woof", MaxAge = 25 });
        }

        public static AnimalInfo Find(string AnimalType)
        {
            AnimalInfo found = null;
            foreach(AnimalInfo animal in AnimalRecords)
            {
                if(animal.AnimalType == AnimalType)
                {
                    found = animal;
                    break;
                }
            }
            return (found);
        }
    }

}
