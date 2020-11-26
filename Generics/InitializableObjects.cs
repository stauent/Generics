using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    public static class ObjectFactory 
    {
        public static T Create<T>(string Name) where T : class, ICommunicate, new()
        {
            T newObject = new T();
            newObject.Initialize(Name);
            return (newObject);
        }
    }
}
