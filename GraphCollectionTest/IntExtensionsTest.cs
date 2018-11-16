using System;
using GraphCollection.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphCollectionTest
{
    [TestClass]
    public class IntExtensionsTest
    {
        [TestMethod]
        public void Faculty_must_work()
        {
            Assert.AreEqual(1, 1.Faculty());
            Assert.AreEqual(5040, 7.Faculty());
        }
    }
}
