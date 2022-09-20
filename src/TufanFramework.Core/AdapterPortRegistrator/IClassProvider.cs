using System;
using System.Collections.Generic;

namespace TufanFramework.Core.AdapterPortRegistrator
{
    public interface IClassProvider
    {
        Dictionary<Type, Type> RegisteredClassDictionary { get; }
        void Register(Type portAssembly, Type adapterAssembly, Type targetInterface);
    }
}