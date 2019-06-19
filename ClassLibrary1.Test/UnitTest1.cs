using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;

namespace ClassLibrary1.Test
{
    public class Class1 : IComparable
    {
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int Method() => 42;
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var c = new Class1();
            Assert.AreEqual(42, c.Method());
        }

        [TestMethod]
        [DeploymentItem("TextFile1.txt")]
        public void Failing1()
        {
            TestMefLogic();
        }

        [TestMethod]
        public void WorkingIfFiailingDoesNotRun()
        {
            TestMefLogic();
        }

        private static void TestMefLogic()
        {
            var assemblyPath = typeof(UnitTest1).Assembly.Location;

            // TODO: use mef to load assembly and look for type
            var assembly = LoadAssembly(assemblyPath);
            Assert.AreEqual(typeof(UnitTest1).Assembly, assembly);

            var class1 = assembly.GetExportedTypes()
                .Where(t => typeof(ICloneable).IsInstanceOfType(t))
                .ToList();

            Assert.AreEqual(1, class1.Count, "Should find 1 matching type");
            Assert.AreEqual(typeof(Class1), class1[0]);
        }

        internal static Assembly LoadAssembly(string assemblyFileName)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(assemblyFileName), "assemblyFileName is required");

            Assembly assembly = null;

            // TODO: [roncain] Dev10 temp workaround.
            // Reference assemblies will fail by file path, so try to load via name
            // and silently accept that result.  If fail, let normal file load try.
            try
            {
                AssemblyName asmName = AssemblyName.GetAssemblyName(assemblyFileName);
                assembly = LoadAssembly(asmName, null);
                if (assembly != null)
                {
                    return assembly;
                }

                // Otherwise attempt to load from file
                assembly = Assembly.LoadFrom(assemblyFileName);
            }
            catch (Exception ex)
            {
                // Some common exceptions log a warning and keep running
                if (ex is System.IO.FileNotFoundException ||
                    ex is System.IO.FileLoadException ||
                    ex is System.IO.PathTooLongException ||
                    ex is BadImageFormatException ||
                    ex is System.Security.SecurityException)
                {
                    
                }
                else
                {
                    throw;
                }
            }
            return assembly;
        }

        /// <summary>
        /// Loads the specified assembly by name.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly to load.  It cannot be null.</param>
        /// <param name="logger">The optional logger to use to report known load failures.</param>
        /// <returns>The loaded <see cref="Assembly"/> if successful, null if it could not be loaded for a known reason
        /// (and an error message will have been logged).
        /// </returns>
        internal static Assembly LoadAssembly(AssemblyName assemblyName, ILogger logger)
        {
            System.Diagnostics.Debug.Assert(assemblyName != null, "assemblyName is required");

            Assembly assembly = null;

            try
            {
                assembly = Assembly.Load(assemblyName);
            }
            catch (Exception ex)
            {
                // Some common exceptions log a warning and keep running
                if (ex is System.IO.FileNotFoundException ||
                    ex is System.IO.FileLoadException ||
                    ex is System.IO.PathTooLongException ||
                    ex is BadImageFormatException ||
                    ex is System.Security.SecurityException)
                {
                }
                else
                {
                    throw;
                }
            }
            return assembly;
        }
    }
}
