using System;
using System.Collections.Generic;

namespace gnCommon
{
    public class ServiceLocator
    {
        static Dictionary<string, object> servicesDictionary = new Dictionary<string, object>();

        public static void Register<T>(T service)
        {
            servicesDictionary[typeof(T).Name] = service;
        }

        public static void Unregister<T>(T service)
        {
            if (servicesDictionary.ContainsKey(typeof(T).Name))
            {
                servicesDictionary.Remove(typeof(T).Name);
            }
        }

        public static T GetService<T>()
        {
            T instance = default(T);
            if (servicesDictionary.ContainsKey(typeof(T).Name) == true)
            {
                instance = (T)servicesDictionary[typeof(T).Name];
            }
            return instance;
        }
    }
}
