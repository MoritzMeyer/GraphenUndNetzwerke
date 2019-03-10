using GraphCollection;
using GraphCollection.SearchAlgorithms;
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
    public class GraphGeneratorTest
    {
        #region LoadFromFile_must_work
        /// <summary>
        /// Testet die Load Methode des Graphen.
        /// </summary>
        [TestMethod]
        public void LoadFromFile_must_work()
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "TestFiles", "TestGraph.txt");

            Graph<string> graph = GraphGenerator.LoadFromFile(path);
            string adjacencyMatrix = graph.GetAdjacencyMatrix();

            Assert.AreEqual(" ||1|2|3|4|5\r\n==============\r\n1||1|1|1|0|0\r\n2||1|1|1|0|1\r\n3||1|1|1|1|0\r\n4||0|0|1|1|1\r\n5||0|1|0|1|1\r\n", adjacencyMatrix);
        }
        #endregion

        #region LoadFlowGraph_must_work
        /// <summary>
        /// Testet das Laden eines Flowgraphens.
        /// </summary>
        [TestMethod]
        public void LoadFlowGraph_must_work()
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "TestFiles", "FlowGraph.txt");

            FlowGraph<string> graph = (FlowGraph<string>)GraphGenerator.LoadFromFile(path);

            // Die Vertices prüfen
            Assert.IsTrue(graph.HasEdge(new Vertex<string>("s"), new Vertex<string>("a")));
            Assert.IsTrue(graph.HasEdge(new Vertex<string>("s"), new Vertex<string>("b")));
            Assert.IsTrue(graph.HasEdge(new Vertex<string>("a"), new Vertex<string>("b")));
            Assert.IsTrue(graph.HasEdge(new Vertex<string>("a"), new Vertex<string>("c")));
            Assert.IsTrue(graph.HasEdge(new Vertex<string>("b"), new Vertex<string>("c")));
            Assert.IsTrue(graph.HasEdge(new Vertex<string>("b"), new Vertex<string>("d")));
            Assert.IsTrue(graph.HasEdge(new Vertex<string>("c"), new Vertex<string>("d")));
            Assert.IsTrue(graph.HasEdge(new Vertex<string>("c"), new Vertex<string>("t")));
            Assert.IsTrue(graph.HasEdge(new Vertex<string>("d"), new Vertex<string>("t")));

            // Die Kapzitäten und Flüsse prüfen.
            Assert.AreEqual(5, graph.GetEdge(new Vertex<string>("s"), new Vertex<string>("a")).Capacity);
            Assert.AreEqual(5, graph.GetEdge(new Vertex<string>("s"), new Vertex<string>("a")).Flow);

            Assert.AreEqual(7, graph.GetEdge(new Vertex<string>("s"), new Vertex<string>("b")).Capacity);
            Assert.AreEqual(3, graph.GetEdge(new Vertex<string>("s"), new Vertex<string>("b")).Flow);

            Assert.AreEqual(7, graph.GetEdge(new Vertex<string>("a"), new Vertex<string>("b")).Capacity);
            Assert.AreEqual(3, graph.GetEdge(new Vertex<string>("a"), new Vertex<string>("b")).Flow);

            Assert.AreEqual(4, graph.GetEdge(new Vertex<string>("a"), new Vertex<string>("c")).Capacity);
            Assert.AreEqual(2, graph.GetEdge(new Vertex<string>("a"), new Vertex<string>("c")).Flow);

            Assert.AreEqual(3, graph.GetEdge(new Vertex<string>("b"), new Vertex<string>("c")).Capacity);
            Assert.AreEqual(3, graph.GetEdge(new Vertex<string>("b"), new Vertex<string>("c")).Flow);

            Assert.AreEqual(3, graph.GetEdge(new Vertex<string>("b"), new Vertex<string>("d")).Capacity);
            Assert.AreEqual(3, graph.GetEdge(new Vertex<string>("b"), new Vertex<string>("d")).Flow);

            Assert.AreEqual(4, graph.GetEdge(new Vertex<string>("c"), new Vertex<string>("d")).Capacity);
            Assert.AreEqual(0, graph.GetEdge(new Vertex<string>("c"), new Vertex<string>("d")).Flow);

            Assert.AreEqual(5, graph.GetEdge(new Vertex<string>("c"), new Vertex<string>("t")).Capacity);
            Assert.AreEqual(5, graph.GetEdge(new Vertex<string>("c"), new Vertex<string>("t")).Flow);

            Assert.AreEqual(6, graph.GetEdge(new Vertex<string>("d"), new Vertex<string>("t")).Capacity);
            Assert.AreEqual(3, graph.GetEdge(new Vertex<string>("d"), new Vertex<string>("t")).Flow);
        }
        #endregion

        #region LoadTaskGraph
        [TestMethod]
        public void LoadTaskGraph()
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "TestFiles", "SPIDER.txt");
            string solutionPath = @"C:\Users\Moritz\Dropbox\Studium\Fulda\4_WS1819\GraphenNetzwerke\Klausur\SPIDER_solution.txt";

            // Sollte eine Datei mit einer Lösung bereits existieren, diese löschen
            if (File.Exists(solutionPath))
            {
                File.Delete(solutionPath);
            }

            // Den Pfad aus der Datei laden.
            Graph<string> graph = GraphGenerator.LoadFromFile(path);

            // Der Startknoten
            Vertex<string> start = new Vertex<string>("1");

            // die kürzesten Distanzen berechnen
            graph = graph.Dijkstra(start);

            // den Prim algrotihmus anwenden
            //Graph<string> minimalSpanningTree = graph.Prim();

            // Den Pfad zusammenstellen
            Stack<Vertex<string>> dijkstraPath = new Stack<Vertex<string>>();
            Vertex<string> actual = graph.GetVertex(new Vertex<string>("996"));
            while(!actual.Equals(start))
            {
                dijkstraPath.Push(actual);
                actual = actual.DijkstraAncestor;
            }

            dijkstraPath.Push(start);


            // Die Knoten auf dem Pfad in eine neue Datei schreiben.
            File.AppendAllLines(@"C:\Users\Moritz\Dropbox\Studium\Fulda\4_WS1819\GraphenNetzwerke\Klausur\SPIDER_solution.txt", dijkstraPath.Select(v => v.Value));
        }
        #endregion

        #region GetPossibleChildNodes_must_work
        /// <summary>
        /// Tests the Method 'GethPossibleNeighborNodes'
        /// </summary>
        [TestMethod]
        public void GetPossibleChildNodes_must_work()
        {
            // Node from where childNodes should be calculated
            TwoBuckets tb1 = new TwoBuckets(3, 5, 2, 3);

            // Get the list with possible child nodes.
            IEnumerable<TwoBuckets> neighborVertices = GraphGenerator.GetNeighborVertices(tb1);

            // Expected NeighborNodes
            TwoBuckets tb2 = new TwoBuckets(3, 5, 3, 2); // Fill1From2
            TwoBuckets tb3 = new TwoBuckets(3, 5, 0, 5); // Fill2From1
            TwoBuckets tb4 = new TwoBuckets(3, 5, 0, 3); // Empty1
            TwoBuckets tb5 = new TwoBuckets(3, 5, 2, 0); // Empty2
            TwoBuckets tb6 = new TwoBuckets(3, 5, 2, 5); // Fill1
            TwoBuckets tb7 = new TwoBuckets(3, 5, 3, 3); // Fill2

            // Check result
            Assert.AreEqual(6, neighborVertices.Count());
            Assert.IsTrue(neighborVertices.Contains(tb2));
            Assert.IsTrue(neighborVertices.Contains(tb3));
            Assert.IsTrue(neighborVertices.Contains(tb4));
            Assert.IsTrue(neighborVertices.Contains(tb5));
            Assert.IsTrue(neighborVertices.Contains(tb6));
            Assert.IsTrue(neighborVertices.Contains(tb7));

            // Test special case
            TwoBuckets tb8 = new TwoBuckets(3, 5);
            neighborVertices = GraphGenerator.GetNeighborVertices(tb8);

            // Check Result
            Assert.AreEqual(2, neighborVertices.Count());
            Assert.IsTrue(neighborVertices.Contains(new TwoBuckets(3, 5, 0, 5)));
            Assert.IsTrue(neighborVertices.Contains(new TwoBuckets(3, 5, 3, 0)));
        }
        #endregion

        #region BucketGraph_must_work
        /// <summary>
        /// Tests the Method 'BucketGraph'
        /// </summary>
        [TestMethod]
        public void BucketGraph_must_work()
        {
            Graph<TwoBuckets> bucketGraph = GraphGenerator.BucketGraph(3, 5);
            string expectedAdjacencyMatrix = "   ||0-0|3-0|0-5|3-5|0-3|3-2|3-3|0-2|1-5|2-0|1-0|2-5|0-1|3-4|3-1|0-4\r\n" +
            "======================================================================\r\n" +
            "0-0|| 1 | 1 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 \r\n" +
            "3-0|| 1 | 1 | 0 | 1 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 \r\n" +
            "0-5|| 1 | 0 | 1 | 1 | 0 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 \r\n" +
            "3-5|| 0 | 1 | 1 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 \r\n" +
            "0-3|| 1 | 1 | 1 | 0 | 1 | 0 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 \r\n" +
            "3-2|| 0 | 1 | 1 | 1 | 0 | 1 | 0 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 \r\n" +
            "3-3|| 0 | 1 | 0 | 1 | 1 | 0 | 1 | 0 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 \r\n" +
            "0-2|| 1 | 0 | 1 | 0 | 0 | 1 | 0 | 1 | 0 | 1 | 0 | 0 | 0 | 0 | 0 | 0 \r\n" +
            "1-5|| 0 | 0 | 1 | 1 | 0 | 0 | 1 | 0 | 1 | 0 | 1 | 0 | 0 | 0 | 0 | 0 \r\n" +
            "2-0|| 1 | 1 | 0 | 0 | 0 | 0 | 0 | 1 | 0 | 1 | 0 | 1 | 0 | 0 | 0 | 0 \r\n" +
            "1-0|| 1 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 1 | 0 | 1 | 0 | 1 | 0 | 0 | 0 \r\n" +
            "2-5|| 0 | 0 | 1 | 1 | 0 | 0 | 0 | 0 | 0 | 1 | 0 | 1 | 0 | 1 | 0 | 0 \r\n" +
            "0-1|| 1 | 0 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 1 | 0 | 1 | 0 | 1 | 0 \r\n" +
            "3-4|| 0 | 1 | 0 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 1 | 0 | 1 | 0 | 1 \r\n" +
            "3-1|| 0 | 1 | 0 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 1 | 0 | 1 | 1 \r\n" +
            "0-4|| 1 | 0 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 1 | 1 | 1 \r\n";

            string adjacencyMatrix = bucketGraph.GetAdjacencyMatrix();

            Assert.AreEqual(expectedAdjacencyMatrix, adjacencyMatrix);
        }
        #endregion
    }
}
