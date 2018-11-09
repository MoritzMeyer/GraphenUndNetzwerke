using GraphCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphTest
{
    [TestClass]
    public class GraphTest
    {
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

        [TestMethod]
        public void GetAdjacencyMatrix_must_work()
        {
            Graph<BucketNode> graph = GraphGenerator.BucketGraph(3, 5);

            string adjacencyMatrix = graph.GetAdjacencyMatrix();
        }
    }
}
