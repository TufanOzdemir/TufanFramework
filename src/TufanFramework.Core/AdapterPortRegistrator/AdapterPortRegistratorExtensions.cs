using Microsoft.Extensions.DependencyInjection;
using System;

namespace TufanFramework.Core.AdapterPortRegistrator
{
    public static class AdapterPortRegistratorExtensions
    {
        public static void AdapterPortRegister(this IServiceCollection services, Type master, Type slave, Type interfaceType)
        {
            var classProvider = new ClassProvider();
            classProvider.Register(master, slave, interfaceType);
            foreach (var item in classProvider.RegisteredClassDictionary)
            {
                services.AddSingleton(item.Key, item.Value);
            }
        }
    }
}