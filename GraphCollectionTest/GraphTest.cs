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

            Graph<int> graph = new Graph<int>();

            // Die Knoten dem Graphen hinzufügen.
            Assert.IsTrue(graph.AddVertex(v1));
            Assert.IsTrue(graph.AddVertex(v2));
            Assert.IsTrue(graph.AddVertex(v3));
            Assert.IsTrue(graph.AddVertex(v4));
        }
        #endregion

        #region RemoveVertex_must_work
        [TestMethod]
        public void RemoveVertex_must_work()
        {
            // Die Knoten erzeugen.
            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);

            // ungerichteten Graphen erzeugen.
            Graph<int> graph = new Graph<int>(new List<Vertex<int>>() { v1, v2, v3, v4 });

            // Mit HasVertex prüfen, ob die Knoten vorhanden sind.
            Assert.IsTrue(graph.HasVertex(v1));
            Assert.IsTrue(graph.HasVertex(v2));
            Assert.IsTrue(graph.HasVertex(v3));
            Assert.IsTrue(graph.HasVertex(v4));

            // Die ersten beiden Knoten wieder entfernen.
            Assert.IsTrue(graph.RemoveVertex(v1));
            Assert.IsTrue(graph.RemoveVertex(v2));

            // Das (nicht-) vorhanden sein der Knoten überprüfen.
            Assert.IsFalse(graph.HasVertex(v1));
            Assert.IsFalse(graph.HasVertex(v2));
            Assert.IsTrue(graph.HasVertex(v3));
            Assert.IsTrue(graph.HasVertex(v4));

            //-------------------- ungerichteter Graph mit Kanten v1-v2 und v3-v4 --------------------
            graph = new Graph<int>(new List<Vertex<int>>() { v1, v2, v3, v4 });
            Assert.IsTrue(graph.AddEdge(v1, v2));
            Assert.IsTrue(graph.AddEdge(v3.Value, v4.Value));

            // Überprüfe die vorhandene Kante v1-v2
            Assert.IsTrue(graph.HasEdge(v1, v2));
            Assert.IsTrue(graph.HasEdge(v2, v1));

            // Den ersten Knoten wieder entfernen
            Assert.IsTrue(graph.RemoveVertex(v1));

            // Es dürfen keine Kanten mehr zu dem Knoten existieren.
            Assert.IsFalse(graph.HasEdge(v1, v2));
            Assert.IsFalse(graph.HasEdge(v2, v1));
            Assert.IsFalse(graph.HasNeighbour(v1, v2));
            Assert.IsFalse(graph.HasNeighbour(v2, v1));

            //-------------------- gerichteter Graph mit Kante v1->v2 erzeugen v3->v4 --------------------
            graph = new Graph<int>(new List<Vertex<int>>() { v1, v2, v3, v4 });
            Assert.IsTrue(graph.AddEdge(v1, v2));
            Assert.IsTrue(graph.AddEdge(v3.Value, v4.Value));

            // Überprüfe die vorhandene Kante v1-v2
            Assert.IsTrue(graph.HasEdge(v1, v2));

            // Den ersten Knoten wieder entfernen
            Assert.IsTrue(graph.RemoveVertex(v1));

            // Es dürfen keine Kanten mehr zu dem Knoten existieren.
            Assert.IsFalse(graph.HasEdge(v1, v2));
            Assert.IsFalse(graph.HasNeighbour(v1, v2));
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

            Assert.IsTrue(graph.HasEdge(v1, v2));
            Assert.IsTrue(graph.HasEdge(v2, v1));
            Assert.IsTrue(graph.HasEdge(v3, v4));
            Assert.IsTrue(graph.HasEdge(v4, v3));

            Assert.IsTrue(graph.HasNeighbour(v1, v2));
            Assert.IsTrue(graph.HasNeighbour(v2, v1));
            Assert.IsTrue(graph.HasNeighbour(v3, v4));
            Assert.IsTrue(graph.HasNeighbour(v4, v3));

            graph = new Graph<int>(new List<Vertex<int>>() { v1, v2, v3, v4 }, true);
            Assert.IsTrue(graph.AddEdge(v1, v2));
            Assert.IsTrue(graph.AddEdge(v3.Value, v4.Value));

            Assert.IsTrue(graph.HasEdge(v1, v2));
            Assert.IsFalse(graph.HasEdge(v2, v1));
            Assert.IsTrue(graph.HasEdge(v3, v4));
            Assert.IsFalse(graph.HasEdge(v4, v3));

            Assert.IsTrue(graph.HasNeighbour(v1, v2));
            Assert.IsFalse(graph.HasNeighbour(v2, v1));
            Assert.IsTrue(graph.HasNeighbour(v3, v4));
            Assert.IsFalse(graph.HasNeighbour(v4, v3));

        }
        #endregion

        #region RemoveEdge_must_work
        [TestMethod]
        public void RemoveEdge_must_work()
        {
            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);

            // Einen ungerichteten Graphen mit den Kanten v1-v2, v3-v4 erzeugen
            Graph<int> graph = new Graph<int>(new List<Vertex<int>>() { v1, v2, v3, v4 });
            Assert.IsTrue(graph.AddEdge(v1, v2));
            Assert.IsTrue(graph.AddEdge(v3.Value, v4.Value));

            // Die erste Kante entfernen
            Edge<int> edge1 = new Edge<int>(v1, v2);
            Assert.IsTrue(graph.RemoveEdge(edge1));

            Assert.IsFalse(graph.HasEdge(v1, v2));
            Assert.IsFalse(graph.HasEdge(v2, v1));

            // Die zweite Kante entfernen
            Assert.IsTrue(graph.RemoveEdge(v3, v4));
            Assert.IsFalse(graph.HasEdge(v3, v4));
            Assert.IsFalse(graph.HasEdge(v4, v3));
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
            Assert.AreEqual(" ||1|2|3|4|5\r\n==============\r\n1||1|1|1|0|0\r\n2||1|1|1|0|1\r\n3||1|1|1|1|0\r\n4||0|0|1|1|1\r\n5||0|1|0|1|1\r\n", adjacencyMatrix);
        }
        #endregion

        #region Copy_must_work
        /// <summary>
        /// Testet die Methode Copy
        /// </summary>
        [TestMethod]
        public void Copy_must_work()
        {
            Graph<TwoBuckets> graph = GraphGenerator.BucketGraph(3, 5);
            Graph<TwoBuckets> graphCopy = graph.Copy();

            Assert.IsFalse(ReferenceEquals(graph.Vertices, graphCopy.Vertices));
            Assert.IsFalse(ReferenceEquals(graph.Edges, graphCopy.Edges));

            Assert.IsFalse(ReferenceEquals(graph.Vertices[0], graphCopy.Vertices[0]));
            Assert.IsFalse(ReferenceEquals(graph.Edges[0], graphCopy.Edges[0]));
        }
        #endregion

        #region GetNeighbours_must_work
        [TestMethod]
        public void GetNeighbours_must_work()
        {
            // Teste get Neighbours für ungerichtete Graphen
            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);

            Graph<int> graph = new Graph<int>(new List<Vertex<int>>() { v1, v2, v3, v4 });

            graph.AddEdge(v1, v2);
            graph.AddEdge(v3, v4);

            Assert.AreEqual(1, graph.GetNeighbours(v1).Count);
            Assert.AreEqual(v2, graph.GetNeighbours(v1)[0]);

            Assert.AreEqual(1, graph.GetNeighbours(v2).Count);
            Assert.AreEqual(v1, graph.GetNeighbours(v2)[0]);

            Assert.AreEqual(1, graph.GetNeighbours(v3).Count);
            Assert.AreEqual(v4, graph.GetNeighbours(v3)[0]);

            Assert.AreEqual(1, graph.GetNeighbours(v4).Count);
            Assert.AreEqual(v3, graph.GetNeighbours(v4)[0]);

            // Teste get Neighbours für gerichtete Graphen
            graph = new Graph<int>(new List<Vertex<int>>() { v1, v2, v3, v4 }, isDirected: true);

            graph.AddEdge(v1, v2);
            graph.AddEdge(v3, v4);

            Assert.AreEqual(1, graph.GetNeighbours(v1).Count);
            Assert.AreEqual(v2, graph.GetNeighbours(v1)[0]);

            Assert.AreEqual(0, graph.GetNeighbours(v2).Count);

            Assert.AreEqual(1, graph.GetNeighbours(v3).Count);
            Assert.AreEqual(v4, graph.GetNeighbours(v3)[0]);

            Assert.AreEqual(0, graph.GetNeighbours(v4).Count);

        }
        #endregion

        #region CountVerticesOfSubGraph_must_work
        [TestMethod]
        public void CountVerticesOfSubGraph_must_work()
        {
            // TODO: Test implementieren
            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);
            Vertex<int> v5 = new Vertex<int>(5);

            Graph<int> graph = new Graph<int>(new List<Vertex<int>>() { v1, v2, v3, v4, v5 });

            graph.AddEdge(v1, v2);
            graph.AddEdge(v1, v3);
            graph.AddEdge(v3, v4);
            graph.AddEdge(v3, v5);

            Assert.AreEqual(5, graph.CountVerticesOfSubgraph(v1));
            Assert.AreEqual(1, graph.CountVerticesOfSubgraph(v2));
            Assert.AreEqual(3, graph.CountVerticesOfSubgraph(v3));
            Assert.AreEqual(1, graph.CountVerticesOfSubgraph(v4));
            Assert.AreEqual(1, graph.CountVerticesOfSubgraph(v5));
        }
        #endregion
    }
}


// TODO: Die Tests für gewichtete und gerichtete Graphen hinzufügen