using System;
using GraphCollection;
using GraphCollection.SearchAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphCollectionTest
{
    [TestClass]
    public class DepthFirstSearchTest
    {
        [TestMethod]
        public void DepthFirstSearch_must_work()
        {
            Graph<TwoBuckets> graph = GraphGenerator.BucketGraph(3, 5);

            Assert.IsTrue(graph.DepthFirstSearch(new Vertex<TwoBuckets>(new TwoBuckets(3, 5, 0, 0)), new Vertex<TwoBuckets>(new TwoBuckets(3, 5, 0, 4))));
        }
    }
}
