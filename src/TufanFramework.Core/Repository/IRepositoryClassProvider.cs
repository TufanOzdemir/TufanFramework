using System;
using System.Collections.Generic;

namespace TufanFramework.Core.Repository
{
    public interface IRepositoryClassProvider
    {
        Dictionary<string, List<Type>> RegisteredClassDictionary { get; }
        void Register(Type master, Type slave);
    }
}