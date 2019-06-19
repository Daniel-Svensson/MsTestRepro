using ClassLibrary2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClassLibrary2.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var c = new Class2();
            Assert.AreEqual(-42, c.Method2());
        }
    }
}
