using System;
using System.Collections;
using System.Collections.Generic;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            // Make a fixed size array of string data.
            string[] dumbArray = { "First", "Second", "Third" };
            DisplayData(dumbArray);

            try
            {
                // While you can modify an existing item in the array ...
                dumbArray[0] = "One";
                dumbArray[1] = "Two";
                dumbArray[2] = "Three";
                DisplayData(dumbArray);

                // The array is NOT flexible. It will not grow dynamically!
                dumbArray[3] = "Four";
                DisplayData(dumbArray);
            }
            catch (Exception Err)
            {
                Console.WriteLine(Err.Message);
            }


            // Because the non-generic collection ArrayList allows you to add
            // ANY type of object, it leads to confusion because you never
            // know what type of object you're dealing with as you iterate through 
            // the collection.
            ArrayList strArray = new ArrayList();
            strArray.AddRange(new string[] { "First", "Second", "Third" });
            strArray.Add(23);
            strArray.Add(8.97);
            strArray.Add(true);
            Car c = new Car { Manufacturer = "Ford", Model = "Focus" };
            strArray.Add(c);

            // Although value types (like int and bool) can be displayed properly
            // (because they have ToString() override implemented), reference types
            // (like our "Car" class) do not have this override by default. As a result
            // the "Car" class DOES NOT display its contents correctly. Instead the object
            // type "Generics.Car" is displayed.
            object[] fromList = strArray.ToArray();
            DisplayData(fromList);

            // Although the integer values 23 exists in the collection and it's value
            // can be displayed as a string, it will NOT compare as equal to the string "23".
            // More confusion!
            bool IsEqual = CheckForEquality(fromList, "23");
            Console.WriteLine($"Check for equality results are: {IsEqual}\r\n================================");

            // Create some Car objects that we'll use in both the non-generic as well as generic collections
            Car ford = new Car { Manufacturer = "Ford", Model = "Focus" };
            Car gmc = new Car { Manufacturer = "GMC", Model = "Journey" };
            Car honda = new Car { Manufacturer = "Honda", Model = "Civic" };

            // Let's use our hand coded "NonGenericCarCollection"
            NonGenericCarCollection nonGenericCarCollection = new NonGenericCarCollection();
            nonGenericCarCollection.Add(ford);
            nonGenericCarCollection.Add(gmc);
            nonGenericCarCollection.Add(honda);
            nonGenericCarCollection.Display();
            Console.WriteLine($"Removing {honda.Manufacturer}:{honda.Model}");
            nonGenericCarCollection.Remove(honda);
            nonGenericCarCollection.Display();


            Console.WriteLine("Demonstrate that using non-generic methods require a lot of hand coding and are not very flexible.");
            Console.WriteLine("========================================");

            // Here we demonstrate how to use a hand-coded class to swap various values.
            // Only the types the have been coded into the NonGenericSwap can make use of the "Swap" method.
            // We would not for example be able to swap two floating point numbers using this class!
            int a = 9, b = 6;
            Console.WriteLine($"a={a} b={b}");
            NonGenericSwap.Swap(ref a, ref b);
            Console.WriteLine($"a={a} b={b}");

            Console.WriteLine($"car a={ford.Manufacturer} car b={gmc.Manufacturer}");
            NonGenericSwap.Swap(ref ford, ref gmc);
            Console.WriteLine($"car a={ford.Manufacturer} car b={gmc.Manufacturer}");

            bool ok = true, notOk = false;
            Console.WriteLine($"ok={ok} notOk={notOk}");
            NonGenericSwap.Swap(ref ok, ref notOk);
            Console.WriteLine($"ok={ok} notOk={notOk}");

            // This example will not compile using the NonGenericSwap class  
            float f1 = 9.87f;
            float f2 = 2.34f;
            //NonGenericSwap.Swap(ref f1, ref f2);


            // Reset all variables to their original values so we can run the same tests using generics
            a = 9;
            b = 6;
            ok = true;
            notOk = false;
            NonGenericSwap.Swap(ref ford, ref gmc);

            //------------- Introducing Generics ----------------------------------
            Console.WriteLine("Now, WITHOUT CREATING ANY NEW CODE, we can use generics to create a type-safe collection of Car objects!");
            List<Car> genericCarCollection = new List<Car>();
            genericCarCollection.Add(ford);
            genericCarCollection.Add(gmc);
            genericCarCollection.Add(honda);
            genericCarCollection.Display();
            Console.WriteLine($"Removing {honda.Manufacturer}:{honda.Model}");
            genericCarCollection.Remove(honda);
            genericCarCollection.Display();

            Console.WriteLine("Demonstrate that writing a custom generic can help simplify your code.");

            // Notice that we can now swap not only all of the previous data types, but also ANY other type
            Console.WriteLine("========================================");
            Console.WriteLine($"a={a} b={b}");
            GenericSwap.Swap(ref a, ref b);
            Console.WriteLine($"a={a} b={b}");

            Console.WriteLine($"car a={ford.Manufacturer} car b={gmc.Manufacturer}");
            GenericSwap.Swap(ref ford, ref gmc);
            Console.WriteLine($"car a={ford.Manufacturer} car b={gmc.Manufacturer}");

            Console.WriteLine($"ok={ok} notOk={notOk}");
            GenericSwap.Swap(ref ok, ref notOk);
            Console.WriteLine($"ok={ok} notOk={notOk}");

            // This example would not compile using the NonGenericSwap class.
            Console.WriteLine($"f1={f1} f2={f2}");
            GenericSwap.Swap(ref f1, ref f2);
            Console.WriteLine($"f1={f1} f2={f2}");
            Console.WriteLine("========================================");

            // Demonstrates how we add constraints to a generic class in order
            // to tell the compiler more details about the type being used 
            ICommunicate fluffy = new Cat { Name = "Fluffy", Message = "Meaow" };
            ICommunicate rover = new Dog { Name = "Rover", Message = "Bark" };
            GenericSwap.SwapMessage(fluffy, rover);

            // NOTE: The previous method call and the one commented out below are functionally equivalent.
            //       When you invoke generic methods such as SwapMessage<T>, you can optionally omit the type parameter if (and only
            //       if) the generic method requires arguments because the compiler can infer the type parameter based on the
            //       member parameters. 
            //
            // GenericSwap.SwapMessage<ICommunicate>(fluffy, rover);




            DoubleLinkedList<string, int> intList = new DoubleLinkedList<string, int>();
            intList.Append("One", 1);
            intList.Append("Two", 2);
            intList.Append("Three", 3);
            intList.Append("Four", 4);
            intList.Append("Five", 5);
            intList.Append("Six", 6);
            intList.Remove("Four");
            intList.InsertAfter("Three", "Four", 4);
            intList.Append("Seven", 7);
            intList.Append("Eight", 8);
            intList.Remove("One");
            intList.Remove("Eight");
            intList.InsertAfter("Seven", "Last", 999);

            Console.WriteLine($"{intList}");
            Console.WriteLine($"Value at Six={intList["Six"].Value}");


            DoubleLinkedList<int, double> doubleList = new DoubleLinkedList<int, double>();
            doubleList.Append(1, 1.12);
            doubleList.Append(2, 87.4);
            doubleList.Append(3, 92.6);
            doubleList.Append(4, 15.4);
            doubleList.Append(5, 12.66);
            doubleList.Append(6, 9.3);
            doubleList.Append(7, 2.3);
            doubleList.Remove(4);
            doubleList.Remove(3);
            doubleList.InsertAfter(2, 3, 99.99);

            Console.WriteLine($"{doubleList}");
            Console.WriteLine($"Value at 6={doubleList[6].Value}");

            //Console.WriteLine("Enumerating entire list ==============================");
            //foreach (IDataNode<int,double> node in doubleList)
            //{
            //    Console.WriteLine($"{node.Key}={node.Value}");
            //}


            DoubleLinkedList<int, string> stringList = new DoubleLinkedList<int, string>();
            stringList.Append(1, "Hello");
            stringList.Append(2, "There");
            stringList.Append(3, "Everyone");
            stringList.Remove(3);
            stringList.InsertAfter(2, 3, "Bob");
            Console.WriteLine($"{stringList}");

            Console.WriteLine($"Last node in stringList {stringList.TerminalNode.Key}={stringList.TerminalNode.Value}");


            Car factoryCar = ObjectFactory.Create<Car>();
            Dog factoryDog = ObjectFactory.Create<Dog>();
            Cat factoryCat = ObjectFactory.Create<Cat>();



            Console.ReadKey();
        }


        public static void DisplayData(object[] someArray)
        {
            foreach(object s in someArray)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("==========================================");
        }

        public static bool CheckForEquality(object[] someArray, string value)
        {
            bool result = false;
            foreach (object s in someArray)
            {
                if(s == value)
                {
                    result = true;
                    break;
                }
            }

            return (result);
        }
    }

}
