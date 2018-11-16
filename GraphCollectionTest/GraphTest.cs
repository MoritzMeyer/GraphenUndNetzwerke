using GraphCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollectionTest
{
    [TestClass]
    public class GraphTest
    {
        #region AddVertex_must_work
        [TestMethod]
        public void AddVertex_must_work()
        {
            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);

            Graph<int> graph = new Graph<int>(new List<Vertex<int>>() { v1, v2, v3, v4 });

            graph.AddEdge(v1, v2);
            graph.AddEdge(v2, v3);
            graph.AddEdge(v3, v4);
            graph.AddEdge(v4, v1);

            Assert.IsTrue(v1.HasEdgeTo(v2));
            Assert.IsTrue(v2.HasEdgeTo(v3));
            Assert.IsTrue(v3.HasEdgeTo(v4));
            Assert.IsTrue(v4.HasEdgeTo(v1));
        }
        #endregion

        #region RemoveVertex_must_work
        [TestMethod]
        public void RemoveVertex_must_work()
        {
            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);

            Graph<int> graph = new Graph<int>(new List<Vertex<int>>() { v1, v2, v3, v4 });

            Assert.IsTrue(graph.HasVertex(v1));
            Assert.IsTrue(graph.HasVertex(v2));
            Assert.IsTrue(graph.HasVertex(v3));
            Assert.IsTrue(graph.HasVertex(v4));

            Assert.IsTrue(graph.RemoveVertex(v1));
            Assert.IsTrue(graph.RemoveVertex(v2));

            Assert.IsFalse(graph.HasVertex(v1));
            Assert.IsFalse(graph.HasVertex(v2));
            Assert.IsTrue(graph.HasVertex(v3));
            Assert.IsTrue(graph.HasVertex(v4));
        }
        #endregion

        #region HasVertex_must_work
        [TestMethod]
        public void HasVertex_must_work()
        {
            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);

            Graph<int> graph = new Graph<int>(new List<Vertex<int>>() { v1, v2, v3, v4 });

            Assert.IsTrue(graph.HasVertex(v1));
            Assert.IsTrue(graph.HasVertex(v2));
            Assert.IsTrue(graph.HasVertex(v3));
            Assert.IsTrue(graph.HasVertex(v4));

            Assert.IsTrue(graph.HasVertexWithValue(v1.Value));
            Assert.IsTrue(graph.HasVertexWithValue(v2.Value));
            Assert.IsTrue(graph.HasVertexWithValue(v3.Value));
            Assert.IsTrue(graph.HasVertexWithValue(v4.Value));
        }
        #endregion

        #region AddEdge_must_work
        [TestMethod]
        public void AddEdge_must_work()
        {
            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);

            Graph<int> graph = new Graph<int>(new List<Vertex<int>>() { v1, v2, v3, v4 });

            Assert.IsTrue(graph.AddEdge(v1, v2));
            Assert.IsTrue(graph.AddEdge(v3.Value, v4.Value));

            Assert.IsTrue(v1.HasEdgeTo(v2));
            Assert.IsTrue(v3.HasEdgeTo(v4));
        }
        #endregion

        #region GetAdjacencyMatrix_must_work
        /// <summary>
        /// Testet die Methode GetAdjacencyMatrix.
        /// </summary>
        [TestMethod]
        public void GetAdjacencyMatrix_must_work()
        {
            // Die Knoten des Graphen erstellen.
            Vertex<string> v1 = new Vertex<string>("1");
            Vertex<string> v2 = new Vertex<string>("2");
            Vertex<string> v3 = new Vertex<string>("3");
            Vertex<string> v4 = new Vertex<string>("4");
            Vertex<string> v5 = new Vertex<string>("5");

            // Den Graphen erstellen
            Graph<string> graph = new Graph<string>(new List<Vertex<string>>() { v1, v2, v3, v4, v5 });

            // Die Nachbarn setzen.
            Assert.IsTrue(graph.AddEdge(v1, v2));
            Assert.IsTrue(graph.AddEdge(v1, v3));
            Assert.IsTrue(graph.AddEdge(v2, v3));
            Assert.IsTrue(graph.AddEdge(v3, v4));
            Assert.IsTrue(graph.AddEdge(v4, v5));
            Assert.IsTrue(graph.AddEdge(v5, v2));

            string adjacencyMatrix = graph.GetAdjacencyMatrix();
            Assert.AreEqual(" ||1|2|3|4|5\r\n==============\r\n1||1|1|1|0|0\r\n2||0|1|1|0|0\r\n3||0|0|1|1|0\r\n4||0|0|0|1|1\r\n5||0|1|0|0|1\r\n", adjacencyMatrix);
        }
        #endregion

        #region Load_must_work
        /// <summary>
        /// Testet die Load Methode des Graphen.
        /// </summary>
        [TestMethod]
        public void Load_must_work()
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "TestFiles", "TestGraph.txt");

            Graph<string> graph = Graph<string>.Load(path);
            string adjacencyMatrix = graph.GetAdjacencyMatrix();

            Assert.AreEqual(" ||1|2|3|4|5\r\n==============\r\n1||1|1|1|0|0\r\n2||0|1|1|0|0\r\n3||0|0|1|1|0\r\n4||0|0|0|1|1\r\n5||0|1|0|0|1\r\n", adjacencyMatrix);
        }
        #endregion
    }
}
