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

        #region AddNeighbor_must_work
        [TestMethod]
        public void AddNeighbor_must_work()
        {
            Vertex<TwoBuckets> v1 = new Vertex<TwoBuckets>(new TwoBuckets(3, 5, 3, 0));
            Vertex<TwoBuckets> v2 = new Vertex<TwoBuckets>(v1.Value.FillB2());

            Assert.IsTrue(v1.AddNeighbor(v2));
            Assert.IsFalse(v1.AddNeighbor(v2));
        }
        #endregion

        #region HasNeighbor_must_work
        [TestMethod]
        public void HasNeighbor_must_work()
        {
            Vertex<TwoBuckets> v1 = new Vertex<TwoBuckets>(new TwoBuckets(3, 5, 3, 0));
            Vertex<TwoBuckets> v2 = new Vertex<TwoBuckets>(v1.Value.FillB2());

            Assert.IsTrue(v1.AddNeighbor(v2));
            Assert.IsTrue(v1.HasNeighbor(v2));
        }
        #endregion

        #region AddNeighbors_must_work
        [TestMethod]
        public void AddNeighbors_must_work()
        {
            Vertex<TwoBuckets> v1 = new Vertex<TwoBuckets>(new TwoBuckets(3, 5, 3, 0));
            Vertex<TwoBuckets> v2 = new Vertex<TwoBuckets>(v1.Value.FillB2());
            Vertex<TwoBuckets> v3 = new Vertex<TwoBuckets>(v1.Value.EmptyB1());

            Assert.IsTrue(v1.AddNeighbors(new HashSet<Vertex<TwoBuckets>>() { v2, v3 }));
            Assert.IsTrue(v1.HasNeighbor(v2));
            Assert.IsTrue(v1.HasNeighbor(v3));
            Assert.IsTrue(v1.RemoveNeighbor(v2));
            Assert.IsFalse(v1.AddNeighbors(new HashSet<Vertex<TwoBuckets>>() { v2, v3 }));
        }
        #endregion

        #region RemoveEdge_must_work
        [TestMethod]
        public void RemoveEdge_must_work()
        {
            Vertex<TwoBuckets> v1 = new Vertex<TwoBuckets>(new TwoBuckets(3, 5, 3, 0));
            Vertex<TwoBuckets> v2 = new Vertex<TwoBuckets>(v1.Value.FillB2());

            Assert.IsTrue(v1.AddEdge(v2));
            Assert.IsTrue(v1.HasEdgeTo(v2));
            Assert.IsTrue(v1.RemoveNeighbor(v2));
            Assert.IsFalse(v1.HasEdgeTo(v2));
        }
        #endregion

        #region CountVerticesOfSubGraph_must_work
        [TestMethod]
        public void CountVerticesOfSubGraph_must_work()
        {
            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);
            Vertex<int> v5 = new Vertex<int>(5);

            v1.AddEdge(v2);
            v1.AddEdge(v3);
            v3.AddEdge(v4);
            v3.AddEdge(v5);


            Assert.AreEqual(5, v1.CountVerticesOfSubGraph());
            Assert.AreEqual(1, v2.CountVerticesOfSubGraph());
            Assert.AreEqual(3, v3.CountVerticesOfSubGraph());
            Assert.AreEqual(1, v4.CountVerticesOfSubGraph());
            Assert.AreEqual(1, v5.CountVerticesOfSubGraph());
        }
        #endregion
    }
}