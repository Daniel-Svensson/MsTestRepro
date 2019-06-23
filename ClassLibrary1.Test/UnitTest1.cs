using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClassLibrary1.Test
{

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
            var expectedAssembly = typeof(UnitTest1).Assembly;
            var expectedType = typeof(UnitTest1);
            string name = expectedType.Name;

            var (assembly, types) = Class1.FindTypes(expectedAssembly.Location, name);


            Assert.AreEqual(expectedAssembly, assembly);
            Assert.AreEqual(1, types.Count, "Should find 1 matching type");
            Assert.AreEqual(expectedType, types[0]);
        }
    }
}
