using System;
using System.Collections.Generic;
using GraphCollection;
using GraphCollection.SearchAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphCollectionTest
{
    [TestClass]
    public class MaximumFlowExtensionsTest
    {
        #region FordFulkerson_must_work
        /// <summary>
        /// Testet den Ford-Fulkerson Algorithmus.
        /// </summary>
        [TestMethod]
        public void FordFulkerson_must_work()
        {
            // Quelle und Senke erstellen.
            Vertex<string> source = new Vertex<string>("s");
            Vertex<string> target = new Vertex<string>("t");

            // alle weiteren Knoten des Graphens erstellen.
            Vertex<string> vA = new Vertex<string>("a");
            Vertex<string> vB = new Vertex<string>("b");
            Vertex<string> vC = new Vertex<string>("c");
            Vertex<string> vD = new Vertex<string>("d");

            // Den Graphen erstellen.
            FlowGraph<string> flowGraph = new FlowGraph<string>(source, target, new List<Vertex<string>>() { source, vA, vB, vC, vD, target });

            // Die Kanten des Graphens erstellen.
            flowGraph.AddEdge("s", "a", capacity: 5, flow: 5);
            flowGraph.AddEdge("s", "b", capacity: 7, flow: 3);
            flowGraph.AddEdge("a", "b", capacity: 7, flow: 3);
            flowGraph.AddEdge("a", "c", capacity: 4, flow: 2);
            flowGraph.AddEdge("b", "c", capacity: 3, flow: 3);
            flowGraph.AddEdge("b", "d", capacity: 3, flow: 3);
            flowGraph.AddEdge("c", "d", capacity: 4, flow: 0);
            flowGraph.AddEdge("c", "t", capacity: 5, flow: 5);
            flowGraph.AddEdge("d", "t", capacity: 6, flow: 3);

            FlowGraph<string> maxFlowGraph = flowGraph.FordFulkerson();

            // Die Flüsse über die Kanten überprüfen.
            Assert.AreEqual(5, maxFlowGraph.Edges[0].Flow);
            Assert.AreEqual(5, maxFlowGraph.Edges[1].Flow);
            Assert.AreEqual(1, maxFlowGraph.Edges[2].Flow);
            Assert.AreEqual(4, maxFlowGraph.Edges[3].Flow);
            Assert.AreEqual(3, maxFlowGraph.Edges[4].Flow);
            Assert.AreEqual(3, maxFlowGraph.Edges[5].Flow);
            Assert.AreEqual(2, maxFlowGraph.Edges[6].Flow);
            Assert.AreEqual(5, maxFlowGraph.Edges[7].Flow);
            Assert.AreEqual(5, maxFlowGraph.Edges[8].Flow);

            // Die Kantenflüsse für den nächsten Test auf 0 setzen.
            flowGraph.Edges.ForEach(e =>
            {
                e.Flow = 0;
            });

            maxFlowGraph = flowGraph.FordFulkerson();

            // Die Flüsse über die Kanten überprüfen.
            Assert.AreEqual(4, maxFlowGraph.Edges[0].Flow);
            Assert.AreEqual(6, maxFlowGraph.Edges[1].Flow);
            Assert.AreEqual(0, maxFlowGraph.Edges[2].Flow);
            Assert.AreEqual(4, maxFlowGraph.Edges[3].Flow);
            Assert.AreEqual(3, maxFlowGraph.Edges[4].Flow);
            Assert.AreEqual(3, maxFlowGraph.Edges[5].Flow);
            Assert.AreEqual(2, maxFlowGraph.Edges[6].Flow);
            Assert.AreEqual(5, maxFlowGraph.Edges[7].Flow);
            Assert.AreEqual(5, maxFlowGraph.Edges[8].Flow);
        }
        #endregion

        #region GetResidualGraph_must_work
        /// <summary>
        /// Testet die Methode GetResidualGraph.
        /// </summary>
        [TestMethod]
        public void GetResidualGraph_must_work()
        {
            // Quelle und Senke erstellen.
            Vertex<string> source = new Vertex<string>("s");
            Vertex<string> target = new Vertex<string>("t");

            // alle weiteren Knoten des Graphens erstellen.
            Vertex<string> vA = new Vertex<string>("a");
            Vertex<string> vB = new Vertex<string>("b");
            Vertex<string> vC = new Vertex<string>("c");
            Vertex<string> vD = new Vertex<string>("d");

            // Den Graphen erstellen.
            FlowGraph<string> flowGraph = new FlowGraph<string>(source, target, new List<Vertex<string>>() { source, vA, vB, vC, vD, target });

            // Die Kanten des Graphens erstellen.
            flowGraph.AddEdge("s", "a", capacity: 5, flow: 5);
            flowGraph.AddEdge("s", "b", capacity: 7, flow: 3);
            flowGraph.AddEdge("a", "b", capacity: 7, flow: 3);
            flowGraph.AddEdge("a", "c", capacity: 4, flow: 2);
            flowGraph.AddEdge("b", "c", capacity: 3, flow: 3);
            flowGraph.AddEdge("b", "d", capacity: 3, flow: 3);
            flowGraph.AddEdge("c", "d", capacity: 4, flow: 0);
            flowGraph.AddEdge("c", "t", capacity: 5, flow: 5);
            flowGraph.AddEdge("d", "t", capacity: 6, flow: 3);

            FlowGraph<string> residualGraph = flowGraph.GetResidualGraph();

            // Die Kanten überprüfen
            Assert.AreEqual(13, residualGraph.Edges.Count);

            // a --> s
            Assert.AreEqual(vA, residualGraph.Edges[0].From);
            Assert.AreEqual(source, residualGraph.Edges[0].To);
            Assert.AreEqual(5, residualGraph.Edges[0].Capacity);
            Assert.AreEqual(-5, residualGraph.Edges[0].Flow);

            // s --> b
            Assert.AreEqual(source, residualGraph.Edges[1].From);
            Assert.AreEqual(vB, residualGraph.Edges[1].To);
            Assert.AreEqual(7, residualGraph.Edges[1].Capacity);
            Assert.AreEqual(4, residualGraph.Edges[1].Flow);

            // b --> s
            Assert.AreEqual(vB, residualGraph.Edges[2].From);
            Assert.AreEqual(source, residualGraph.Edges[2].To);
            Assert.AreEqual(7, residualGraph.Edges[2].Capacity);
            Assert.AreEqual(-3, residualGraph.Edges[2].Flow);

            // a --> b
            Assert.AreEqual(vA, residualGraph.Edges[3].From);
            Assert.AreEqual(vB, residualGraph.Edges[3].To);
            Assert.AreEqual(7, residualGraph.Edges[3].Capacity);
            Assert.AreEqual(4, residualGraph.Edges[3].Flow);

            // b --> a
            Assert.AreEqual(vB, residualGraph.Edges[4].From);
            Assert.AreEqual(vA, residualGraph.Edges[4].To);
            Assert.AreEqual(7, residualGraph.Edges[4].Capacity);
            Assert.AreEqual(-3, residualGraph.Edges[4].Flow);

            // a --> c
            Assert.AreEqual(vA, residualGraph.Edges[5].From);
            Assert.AreEqual(vC, residualGraph.Edges[5].To);
            Assert.AreEqual(4, residualGraph.Edges[5].Capacity);
            Assert.AreEqual(2, residualGraph.Edges[5].Flow);

            // c --> a
            Assert.AreEqual(vC, residualGraph.Edges[6].From);
            Assert.AreEqual(vA, residualGraph.Edges[6].To);
            Assert.AreEqual(4, residualGraph.Edges[6].Capacity);
            Assert.AreEqual(-2, residualGraph.Edges[6].Flow);

            // c --> b
            Assert.AreEqual(vC, residualGraph.Edges[7].From);
            Assert.AreEqual(vB, residualGraph.Edges[7].To);
            Assert.AreEqual(3, residualGraph.Edges[7].Capacity);
            Assert.AreEqual(-3, residualGraph.Edges[7].Flow);

            // d --> b
            Assert.AreEqual(vD, residualGraph.Edges[8].From);
            Assert.AreEqual(vB, residualGraph.Edges[8].To);
            Assert.AreEqual(3, residualGraph.Edges[8].Capacity);
            Assert.AreEqual(-3, residualGraph.Edges[8].Flow);

            // c --> d
            Assert.AreEqual(vC, residualGraph.Edges[9].From);
            Assert.AreEqual(vD, residualGraph.Edges[9].To);
            Assert.AreEqual(4, residualGraph.Edges[9].Capacity);
            Assert.AreEqual(4, residualGraph.Edges[9].Flow);

            // t --> c
            Assert.AreEqual(target, residualGraph.Edges[10].From);
            Assert.AreEqual(vC, residualGraph.Edges[10].To);
            Assert.AreEqual(5, residualGraph.Edges[10].Capacity);
            Assert.AreEqual(-5, residualGraph.Edges[10].Flow);

            // d --> t
            Assert.AreEqual(vD, residualGraph.Edges[11].From);
            Assert.AreEqual(target, residualGraph.Edges[11].To);
            Assert.AreEqual(6, residualGraph.Edges[11].Capacity);
            Assert.AreEqual(3, residualGraph.Edges[11].Flow);

            // t --> d
            Assert.AreEqual(target, residualGraph.Edges[12].From);
            Assert.AreEqual(vD, residualGraph.Edges[12].To);
            Assert.AreEqual(6, residualGraph.Edges[12].Capacity);
            Assert.AreEqual(-3, residualGraph.Edges[12].Flow);
        }
        #endregion
    }
}
