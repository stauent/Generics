using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
public class Car
{
    public string Manufacturer { get; set; }
    public string Model { get; set; }

    public Guid UniqueId { get; }

    public Car()
    {
        UniqueId = new Guid();
    }
}

public static class CarCollectionHelper
{
    public static void Display(this List<Car> allCars)
    {
        foreach (Car nextCar in allCars)
        {
            Console.WriteLine($"Manufacturer:{nextCar.Manufacturer}\tModel:{nextCar.Model}");
        }
        Console.WriteLine("==========================================");
    }
}
}
