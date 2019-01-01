using GraphCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GraphCollectionTest
{
    [TestClass]
    public class VertexTest
    {
        #region Equals_must_work
        [TestMethod]
        public void Equals_musst_work()
        {
            // Test mit Strings
            Vertex<string> v1 = new Vertex<string>("Vertex1");
            Vertex<string> v2 = new Vertex<string>("Vertex1");
            Vertex<string> v3 = new Vertex<string>("Vertex2");

            Assert.IsFalse(v1.Equals(null));

            Assert.IsTrue(v1.Equals(v2));
            Assert.IsTrue(v2.Equals(v1));
            Assert.IsFalse(v1.Equals(v3));
            Assert.IsFalse(v2.Equals(v3));

            // Test mit ints.
            Vertex<int> v4 = new Vertex<int>(1);
            Vertex<int> v5 = new Vertex<int>(1);
            Vertex<int> v6 = new Vertex<int>(2);

            Assert.IsTrue(v4.Equals(v5));
            Assert.IsFalse(v4.Equals(v6));

            // Test mit TwoBuckets
            Vertex<TwoBuckets> v7 = new Vertex<TwoBuckets>(new TwoBuckets(3, 5));
            Vertex<TwoBuckets> v8 = new Vertex<TwoBuckets>(new TwoBuckets(3, 5));
            Vertex<TwoBuckets> v9 = new Vertex<TwoBuckets>(new TwoBuckets(5, 3));

            Assert.IsTrue(v7.Equals(v8));
            Assert.IsFalse(v7.Equals(v9));

            v7.Value = v7.Value.FillB1();
            Assert.IsFalse(v7.Equals(v8));

            v8.Value = v8.Value.FillB1();
            Assert.IsTrue(v7.Equals(v8));
        }
        #endregion
    }
}