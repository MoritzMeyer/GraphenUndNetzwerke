using System;
using System.Collections.Generic;
using GraphCollection;
using GraphCollection.SearchAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphCollectionTest
{
    [TestClass]
    public class AllPairsShortestPathTest
    {
        #region AllPairsShortestPath_must_work
        /// <summary>
        /// Testet den AllPairsShortestPath Algorithmus
        /// </summary>
        [TestMethod]
        public void AllPairsShortestPath_must_work()
        {
            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);

            Graph<int> graph = new Graph<int>(new List<Vertex<int>>() { v1, v2, v3, v4 }, true);

            graph.AddEdge(v1, v3, -2);
            graph.AddEdge(v3, v4, 2);
            graph.AddEdge(v4, v2, -1);
            graph.AddEdge(v2, v1, 4);
            graph.AddEdge(v2, v3, 3);

            string allPairsShortestPathString = graph.AllPairsShortestPath();

            string expected = "  ||1 |2 |3 |4 " + Environment.NewLine +
                              "=================" + Environment.NewLine +
                              "1 ||0 |-1|-2|0 " + Environment.NewLine +
                              "2 ||4 |0 |2 |4 " + Environment.NewLine +
                              "3 ||5 |1 |0 |2 " + Environment.NewLine +
                              "4 ||3 |-1|1 |0 " + Environment.NewLine;


            Assert.AreEqual(expected, allPairsShortestPathString);
        }
        #endregion
    }
}
