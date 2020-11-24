using Microsoft.VisualBasic;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    /// <summary>
    /// This class will help demonstrate how you can implement generics in your own code.
    /// </summary>
    public static class GenericSwap
    {
        // This method will swap any two items.
        // as specified by the type parameter <T>.
        public static void Swap<T>(ref T a, ref T b)
        {
            Console.WriteLine("You sent the Swap() method a {0}", typeof(T));
            T temp = a;
            a = b;
            b = temp;

            // WILL NOT COMPILE!!!! Because the compiler is not told that type T must support interface "Communicate"
            // a.Speak();
            // b.Speak();
        }

        /// <summary>
        /// This method will make use of the fact that all parameters are of type <T>
        /// which is the interface ICommunicate. We will make use of the methods exposed
        /// by this interface to exchange message data between the two objects being used
        /// in the call.
        /// </summary>
        /// <typeparam name="T">Specifies the generic type of the method parameters</typeparam>
        /// <param name="a">First parameter that implements interface ICommunicate</param>
        /// <param name="b">Second parameter that implements interface ICommunicate</param>
        public static void SwapMessage<T>(T a, T b) where T: ICommunicate
        {
            Console.WriteLine("Before swap");

            //-------- Without the "where" type constraint, the following code would not compile!
            a.Speak();
            b.Speak();

            string temp = a.Message;
            a.Message = b.Message;
            b.Message = temp;

            Console.WriteLine("After swap");
            a.Speak();
            b.Speak();
        }

    }

    public static class CollectionHelper<T> where T: class
    {
        public static List<string> ConvertToString(List<T> ObjectToConvert)
        {
            List<string> Converted = new List<string>();
            foreach(T EnumerableItem in ObjectToConvert)
            {
                Converted.Add(EnumerableItem.ToString());
            }

            return (Converted);
        }
    }
}
