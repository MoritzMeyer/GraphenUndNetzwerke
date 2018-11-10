using GraphCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public class NodeTest
    {
        [TestMethod]
        public void Equals_must_work()
        {
            Node node1 = new Node("Node")
            {
                Visited = false
            };

            Node node2 = new Node("Node")
            {
                Visited = false
            };

            Node node3 = new Node("Node3")
            {
                Visited = false
            };

            Assert.IsTrue(node1.Equals(node2));
            Assert.IsFalse(node1.Equals(node3));
        }
    }
}
