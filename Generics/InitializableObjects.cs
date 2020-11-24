using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    public static class ObjectFactory 
    {
        public static T Create<T>() where T : class, new()
        {
            T newObject = new T();
            return (newObject);
        }
    }
}
