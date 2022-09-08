using System;
using System.Collections.Generic;
using System.Linq;

namespace TufanFramework.Core.Repository
{
    public class RepositoryClassProvider : IRepositoryClassProvider
    {
        private Lazy<Dictionary<string, List<Type>>> MainDictionary = new Lazy<Dictionary<string, List<Type>>>(() =>
        {
            var classDictionary = new Dictionary<string, List<Type>>();
            return classDictionary;
        });

        public Dictionary<string, List<Type>> RegisteredClassDictionary => MainDictionary.Value;

        public void Register(Type master, Type slave)
        {
            var masterTypes = master.Assembly.GetTypes();
            var typeName = typeof(IRepository).Name;
            foreach (var item in masterTypes)
            {
                List<Type> typeList = new List<Type>();
                var myInterface = item.GetInterfaces().FirstOrDefault(c => c.Name == typeName);
                if (myInterface != null)
                {
                    var slaveTypes = slave.Assembly.GetTypes();
                    foreach (var item2 in slaveTypes)
                    {
                        var teta = item2.GetInterfaces();
                        var myInterface2 = teta.FirstOrDefault(c => c.Name == item.Name);
                        if (myInterface2 != null)
                        {
                            typeList.Add(item2);
                        }
                    }

                    if (typeList.Any())
                    {
                        RegisteredClassDictionary.Add(item.FullName, typeList);
                    }
                }
            }
        }
    }
}