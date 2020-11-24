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
        string Message { get; set; }
        void Speak();
    }


    /// <summary>
    /// All Animal objects implement ICommunicate
    /// </summary>
    public class Animal : ICommunicate
    {
        public string Name { get; set; }
        public string Message { get; set; }

        public void Speak()
        {
            Console.WriteLine($"{Name} says {Message}");
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



}
