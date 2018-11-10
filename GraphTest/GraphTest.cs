using GraphCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tests
{
    [TestClass]
    public class GraphTest
    {
        #region Contains_must_work
        /// <summary>
        /// Tests the Method 'Contains'
        /// </summary>
        [TestMethod]
        public void Contains_must_work()
        {
            BucketNode node1 = new BucketNode("3-2", 3, 5, 3, 2);
            BucketNode node2 = new BucketNode("3-2", 3, 5, 3, 2);
            Graph<BucketNode> graph = new Graph<BucketNode>(node1);


            Assert.IsTrue(graph.Contains(node2));
            Assert.IsTrue(ReferenceEquals(node1, graph.GetNodeInstance(node2)));
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
            Node node1 = new Node("1");
            Node node2 = new Node("2");
            Node node3 = new Node("3");
            Node node4 = new Node("4");
            Node node5 = new Node("5");

            // Die Nachbarn setzen.
            node1.AddNeighbor(node2);
            node1.AddNeighbor(node3);
            node2.AddNeighbor(node3);
            node3.AddNeighbor(node4);
            node4.AddNeighbor(node5);
            node5.AddNeighbor(node2);

            // Den Graphen erstellen.
            Graph<Node> graph = new Graph<Node>(node1);
            graph.AddNode(node2);
            graph.AddNode(node3);
            graph.AddNode(node4);
            graph.AddNode(node5);

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
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "TestFiles", "TestGraph.txt");

            Graph<Node> graph = Graph<Node>.Load(path);
            string adjacencyMatrix = graph.GetAdjacencyMatrix();

            Assert.AreEqual(" ||1|2|3|4|5\r\n==============\r\n1||1|1|1|0|0\r\n2||0|1|1|0|0\r\n3||0|0|1|1|0\r\n4||0|0|0|1|1\r\n5||0|1|0|0|1\r\n", adjacencyMatrix);
        }
        #endregion
    }
}
