using System;
using System.Collections.Generic;
using GraphCollection;
using GraphCollection.SearchAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphCollectionTest
{
    [TestClass]
    public class MinimalSpanningTreeTest
    {
        #region Kurskal_must_work
        /// <summary>
        /// Testet den Kruskal Algorithmus
        /// </summary>
        [TestMethod]
        public void Kurskal_must_work()
        {
            Vertex<string> vA = new Vertex<string>("A");
            Vertex<string> vB = new Vertex<string>("B");
            Vertex<string> vC = new Vertex<string>("C");
            Vertex<string> vD = new Vertex<string>("D");
            Vertex<string> vE = new Vertex<string>("E");
            Vertex<string> vF = new Vertex<string>("F");
            Vertex<string> vG = new Vertex<string>("G");

            Graph<string> graph = new Graph<string>(new List<Vertex<string>>() { vA, vB, vC, vD, vE, vF, vG });

            graph.AddEdge(vA, vD, 5);
            graph.AddEdge(vA, vB, 7);
            graph.AddEdge(vB, vC, 8);
            graph.AddEdge(vB, vE, 7);
            graph.AddEdge(vB, vD, 9);
            graph.AddEdge(vC, vE, 5);
            graph.AddEdge(vD, vE, 15);
            graph.AddEdge(vE, vF, 8);
            graph.AddEdge(vE, vG, 9);
            graph.AddEdge(vD, vF, 6);
            graph.AddEdge(vF, vG, 11);

            Graph<string> minimalSpanningTree = graph.Kruskal();
            Assert.IsTrue(minimalSpanningTree.HasEdge(vA, vD));
            Assert.IsTrue(minimalSpanningTree.HasEdge(vA, vB));
            Assert.IsTrue(minimalSpanningTree.HasEdge(vC, vE));
            Assert.IsTrue(minimalSpanningTree.HasEdge(vB, vE));
            Assert.IsTrue(minimalSpanningTree.HasEdge(vD, vF));
            Assert.IsTrue(minimalSpanningTree.HasEdge(vE, vG));            
        }
        #endregion

        /// <summary>
        /// Implementiert den Prim Algorithmus für minimale Spannbäume
        /// </summary>
        [TestMethod]
        public void Prim_must_work()
        {
            Vertex<string> vA = new Vertex<string>("A");
            Vertex<string> vB = new Vertex<string>("B");
            Vertex<string> vC = new Vertex<string>("C");
            Vertex<string> vD = new Vertex<string>("D");
            Vertex<string> vE = new Vertex<string>("E");
            Vertex<string> vF = new Vertex<string>("F");
            Vertex<string> vG = new Vertex<string>("G");

            Graph<string> graph = new Graph<string>(new List<Vertex<string>>() { vA, vB, vC, vD, vE, vF, vG });

            graph.AddEdge(vA, vD, 5);
            graph.AddEdge(vA, vB, 7);
            graph.AddEdge(vB, vC, 8);
            graph.AddEdge(vB, vE, 7);
            graph.AddEdge(vB, vD, 9);
            graph.AddEdge(vC, vE, 5);
            graph.AddEdge(vD, vE, 15);
            graph.AddEdge(vE, vF, 8);
            graph.AddEdge(vE, vG, 9);
            graph.AddEdge(vD, vF, 6);
            graph.AddEdge(vF, vG, 11);

            Graph<string> minimalSpanningTree = graph.Prim();
            Assert.IsTrue(minimalSpanningTree.HasEdge(vA, vD));
            Assert.IsTrue(minimalSpanningTree.HasEdge(vA, vB));
            Assert.IsTrue(minimalSpanningTree.HasEdge(vC, vE));
            Assert.IsTrue(minimalSpanningTree.HasEdge(vB, vE));
            Assert.IsTrue(minimalSpanningTree.HasEdge(vD, vF));
            Assert.IsTrue(minimalSpanningTree.HasEdge(vE, vG));

        }
    }
}
