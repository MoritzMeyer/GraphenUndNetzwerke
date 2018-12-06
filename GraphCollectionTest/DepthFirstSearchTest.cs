using System;
using System.Collections.Generic;
using GraphCollection;
using GraphCollection.SearchAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphCollectionTest
{
    [TestClass]
    public class DepthFirstSearchTest
    {
        #region DepthFirstSearch_must_work
        [TestMethod]
        public void DepthFirstSearch_must_work()
        {
            Graph<TwoBuckets> graph = GraphGenerator.BucketGraph(3, 5);

            Assert.IsTrue(graph.DepthFirstSearch(new Vertex<TwoBuckets>(new TwoBuckets(3, 5, 0, 0)), new Vertex<TwoBuckets>(new TwoBuckets(3, 5, 0, 4))));
        }
        #endregion

        #region HasCycle_must_work
        [TestMethod]
        public void HasCycle_must_work()
        {
            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);
            Vertex<int> v5 = new Vertex<int>(5);
            Vertex<int> v6 = new Vertex<int>(6);

            Graph<int> graph = new Graph<int>(new List<Vertex<int>>() { v1, v2, v3, v4, v5, v6 });

            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 6);

            Assert.IsFalse(graph.HasCycle());

            graph.AddEdge(6, 5);

            Assert.IsTrue(graph.HasCycle());

        }
        #endregion
    }
}
