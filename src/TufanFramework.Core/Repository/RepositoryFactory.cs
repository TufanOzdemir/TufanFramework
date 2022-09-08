using System;
using System.Collections.Generic;
using System.Linq;

namespace TufanFramework.Core.Repository
{
    public class RepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepositoryClassProvider _repositoryClassProvider;

        public RepositoryFactory(IServiceProvider serviceProvider, IRepositoryClassProvider repositoryClassProvider)
        {
            _serviceProvider = serviceProvider;
            _repositoryClassProvider = repositoryClassProvider;
        }

        public T GetRepository<T>(bool isCacheEnabled = false) where T : IRepository
        {
            var mytype = typeof(T);
            if (_repositoryClassProvider.RegisteredClassDictionary.TryGetValue(mytype.FullName, out var mylist))
            {
                var result = GetTypeByTypeList(mylist, isCacheEnabled);
                if (result == null)
                {
                    return default;
                }
                object calcInstance = null;
                var firstConstructor = result.GetConstructors()?.FirstOrDefault();
                if (firstConstructor != null)
                {
                    var constructorParameters = firstConstructor.GetParameters();
                    if (constructorParameters != null && constructorParameters.Any())
                    {
                        var objectList = new List<object>();

                        foreach (var constructorParameter in constructorParameters)
                        {
                            objectList.Add(_serviceProvider.GetService(constructorParameter.ParameterType));
                        }

                        calcInstance = Activator.CreateInstance(result, objectList.ToArray());
                    }
                    else
                    {
                        calcInstance = Activator.CreateInstance(result);
                    }
                }
                return (T)calcInstance;
            }
            return default;
        }

        private Type GetTypeByTypeList(List<Type> typeList, bool isCacheEnabled)
        {
            return typeList
                .FirstOrDefault(c => 
                {
                    return !isCacheEnabled ?
                        c.GetInterfaces().All(c => c.Name != typeof(ICacheRepository).Name) :
                        c.GetInterfaces().Any(c => c.Name == typeof(ICacheRepository).Name);
                });
        }
    }
}
