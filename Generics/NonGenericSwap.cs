using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    /// <summary>
    /// This class demonstrates the problem we have if we don't 
    /// use generics. We have to duplicate the same lines of code
    /// every time we want to support a new data type.
    /// </summary>
    public static class NonGenericSwap
    {
        // Swap two integers.
        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        // Swap two Car objects.
        public static void Swap(ref Car a, ref Car b)
        {
            Car temp = a;
            a = b;
            b = temp;
        }

        // Swap two bool objects.
        public static void Swap(ref bool a, ref bool b)
        {
            bool temp = a;
            a = b;
            b = temp;
        }
    }
}
