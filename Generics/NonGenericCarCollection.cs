using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
/// <summary>
/// This class demonstrates how programmers historically had to hand-code 
/// collections, prior to generics, in order to make them type-safe.
/// </summary>
public class NonGenericCarCollection : IEnumerable
{
    // The non-generic collection is used to house ONLY  "Car" objects.
    protected ArrayList allCars = new ArrayList();

    /// <summary>
    /// Type-safe method to add a "Car" to the list of cars
    /// </summary>
    /// <param name="newCar">Object of type Car. Ensures that ONLY Car objects are added to the collection</param>
    public void Add(Car newCar)
    {
        allCars.Add(newCar);
    }

    /// <summary>
    /// Type-safe method to remove a "Car" from the list of cars.
    /// </summary>
    /// <param name="removeCar">Object of type Car. Ensures that ONLY Car objects are added to the collection</param>
    /// <returns>True is returned if the car was successfully found and removed from the collection</returns>
    public bool Remove(Car removeCar)
    {
        bool success = false;

        try
        {
            Car found = Find(removeCar.UniqueId);
            if (found != null)
            {
                allCars.Remove(removeCar);
                success = true;
            }
        }
        catch (Exception Err)
        {
            Console.WriteLine(Err.Message);
        }

        return (success);
    }

    /// <summary>
    /// Finds a car in the collection
    /// </summary>
    /// <param name="findCar">Specific car to find in the collection</param>
    /// <returns>True is returned if the specified car is found in the collection</returns>
    public bool Find(Car findCar)
    {
        return (Find(findCar.UniqueId) != null);
    }


    /// <summary>
    /// Finds a car in the collection
    /// </summary>
    /// <param name="uniqueId">Specific car to find in the collection</param>
    /// <returns>The Car matching the uniqueID is returned. Null if no match.</returns>
    public Car Find(Guid uniqueId)
    {
        Car found = null;
        foreach (Car c in allCars)
        {
            if (c.UniqueId == uniqueId)
            {
                found = c;
                break;
            }
        }

        return (found);
    }

    public int Count => allCars.Count;

    // Foreach enumeration support.
    IEnumerator IEnumerable.GetEnumerator() => allCars.GetEnumerator();

    public void Display()
    {
        foreach (Car nextCar in allCars)
        {
            Console.WriteLine($"Manufacturer:{nextCar.Manufacturer}\tModel:{nextCar.Model}");
        }
        Console.WriteLine("==========================================");
    }
}

}
