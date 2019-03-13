using GraphCollection;
using GraphCollection.SearchAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GraphCollectionTest
{
    [TestClass]
    public class DijkstraExtensionTest
    {
        #region Dijkstra_must_work
        /// <summary>
        /// Testet den Dijkstra-Algorithmus ohne Zielknoten
        /// </summary>
        [TestMethod]
        public void Dijkstra_must_work()
        {
            Vertex<string> FFM = new Vertex<string>("FFM");
            Vertex<string> MA = new Vertex<string>("MA");
            Vertex<string> KA = new Vertex<string>("KA");
            Vertex<string> A = new Vertex<string>("A");
            Vertex<string> M = new Vertex<string>("M");
            Vertex<string> EF = new Vertex<string>("EF");
            Vertex<string> WUE = new Vertex<string>("WUE");
            Vertex<string> N = new Vertex<string>("N");
            Vertex<string> S = new Vertex<string>("S");
            Vertex<string> KS = new Vertex<string>("KS");

            Graph<string> graph = new Graph<string>(new List<Vertex<string>>() { FFM, MA, KA, A, M, EF, WUE, N, S, KS });

            Assert.IsTrue(graph.AddEdge(FFM, MA, 85));
            Assert.IsTrue(graph.AddEdge(FFM, KS, 173));
            Assert.IsTrue(graph.AddEdge(FFM, WUE, 217));
            Assert.IsTrue(graph.AddEdge(MA, KA, 80));
            Assert.IsTrue(graph.AddEdge(KA, A, 250));
            Assert.IsTrue(graph.AddEdge(A, M, 84));
            Assert.IsTrue(graph.AddEdge(M, KS, 502));
            Assert.IsTrue(graph.AddEdge(M, N, 167));
            Assert.IsTrue(graph.AddEdge(WUE, EF, 186));
            Assert.IsTrue(graph.AddEdge(WUE, N, 103));
            Assert.IsTrue(graph.AddEdge(N, S, 183));

            graph.Dijkstra(FFM);

            // Die Distancen prüfen.
            Assert.AreEqual(0, FFM.DijkstraDistance);
            Assert.AreEqual(85, MA.DijkstraDistance);
            Assert.AreEqual(165, KA.DijkstraDistance);
            Assert.AreEqual(415, A.DijkstraDistance);
            Assert.AreEqual(487, M.DijkstraDistance);
            Assert.AreEqual(217, WUE.DijkstraDistance);
            Assert.AreEqual(320, N.DijkstraDistance);
            Assert.AreEqual(173, KS.DijkstraDistance);
            Assert.AreEqual(503, S.DijkstraDistance);
            Assert.AreEqual(403, EF.DijkstraDistance);

            // Die Vorgänger prüfen.
            Assert.AreEqual(FFM, FFM.DijkstraAncestor);
            Assert.AreEqual(FFM, MA.DijkstraAncestor);
            Assert.AreEqual(FFM, WUE.DijkstraAncestor);
            Assert.AreEqual(MA, KA.DijkstraAncestor);
            Assert.AreEqual(KA, A.DijkstraAncestor);
            Assert.AreEqual(N, M.DijkstraAncestor);
            Assert.AreEqual(FFM, KS.DijkstraAncestor);
            Assert.AreEqual(WUE, N.DijkstraAncestor);
            Assert.AreEqual(WUE, EF.DijkstraAncestor);
            Assert.AreEqual(N, S.DijkstraAncestor);
        }
        #endregion

        #region Dijkstra_Target_must_work
        /// <summary>
        /// Testet den Dijkstra-Algorithmus mit Zielknoten.
        /// </summary>
        [TestMethod]
        public void Dijkstra_Target_must_work()
        {
            Vertex<string> FFM = new Vertex<string>("FFM");
            Vertex<string> MA = new Vertex<string>("MA");
            Vertex<string> KA = new Vertex<string>("KA");
            Vertex<string> A = new Vertex<string>("A");
            Vertex<string> M = new Vertex<string>("M");
            Vertex<string> EF = new Vertex<string>("EF");
            Vertex<string> WUE = new Vertex<string>("WUE");
            Vertex<string> N = new Vertex<string>("N");
            Vertex<string> S = new Vertex<string>("S");
            Vertex<string> KS = new Vertex<string>("KS");

            Graph<string> graph = new Graph<string>(new List<Vertex<string>>() { FFM, MA, KA, A, M, EF, WUE, N, S, KS });

            Assert.IsTrue(graph.AddEdge(FFM, MA, 85));
            Assert.IsTrue(graph.AddEdge(FFM, KS, 173));
            Assert.IsTrue(graph.AddEdge(FFM, WUE, 217));
            Assert.IsTrue(graph.AddEdge(MA, KA, 80));
            Assert.IsTrue(graph.AddEdge(KA, A, 250));
            Assert.IsTrue(graph.AddEdge(A, M, 84));
            Assert.IsTrue(graph.AddEdge(M, KS, 502));
            Assert.IsTrue(graph.AddEdge(M, N, 167));
            Assert.IsTrue(graph.AddEdge(WUE, EF, 186));
            Assert.IsTrue(graph.AddEdge(WUE, N, 103));
            Assert.IsTrue(graph.AddEdge(N, S, 183));

            graph.Dijkstra(FFM, KA);

            // Die Distancen prüfen.
            Assert.AreEqual(0, FFM.DijkstraDistance);
            Assert.AreEqual(85, MA.DijkstraDistance);
            Assert.AreEqual(165, KA.DijkstraDistance);
            Assert.AreEqual(217, WUE.DijkstraDistance);
            Assert.AreEqual(173, KS.DijkstraDistance);
            Assert.AreEqual(int.MaxValue, A.DijkstraDistance);
            Assert.AreEqual(int.MaxValue, M.DijkstraDistance);
            Assert.AreEqual(int.MaxValue, N.DijkstraDistance);
            Assert.AreEqual(int.MaxValue, S.DijkstraDistance);
            Assert.AreEqual(int.MaxValue, EF.DijkstraDistance);

            // Die Vorgänger prüfen.
            Assert.AreEqual(FFM, FFM.DijkstraAncestor);
            Assert.AreEqual(FFM, MA.DijkstraAncestor);
            Assert.AreEqual(MA, KA.DijkstraAncestor);
            Assert.AreEqual(FFM, KS.DijkstraAncestor);
            Assert.AreEqual(FFM, WUE.DijkstraAncestor);
            Assert.AreEqual(null, A.DijkstraAncestor);
            Assert.AreEqual(null, M.DijkstraAncestor);
            Assert.AreEqual(null, N.DijkstraAncestor);
            Assert.AreEqual(null, EF.DijkstraAncestor);
            Assert.AreEqual(null, S.DijkstraAncestor);
        }
        #endregion
    }
}
