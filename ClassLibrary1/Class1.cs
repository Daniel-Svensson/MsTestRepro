using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClassLibrary1
{
    public class Class1 : IComparable
    {
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int Method() => 42;


        public static (Assembly, List<Type>) FindTypes(string assemblyPath, string name)
        {
            // TODO: use mef to load assembly and look for type
            var assemblyName = AssemblyName.GetAssemblyName(assemblyPath);
            var assembly = Assembly.Load(assemblyName);
     
            var classes = assembly.GetExportedTypes()
                .Where(t => t.Name == name)
                .ToList();
            return (assembly, classes);
        }

    }
}
