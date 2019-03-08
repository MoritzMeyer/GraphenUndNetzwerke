
using GraphCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollectionTest
{
    [TestClass]
    public class FlowGraphTest
    {
        /// <summary>
        /// Testet die Methode Copy
        /// </summary>
        [TestMethod]
        public void Copy_must_work()
        {
            Vertex<string> source = new Vertex<string>("s");
            Vertex<string> target = new Vertex<string>("t");

            FlowGraph<string> flowGraph = new FlowGraph<string>(source, target, new List<Vertex<string>>() { source, target });

            flowGraph.AddVertex("a");
            flowGraph.AddVertex("b");

            flowGraph.AddEdge("s", "a", 5, 2);
            flowGraph.AddEdge("s", "b", 8, 4);
            flowGraph.AddEdge("a", "b", 3, 0);
            flowGraph.AddEdge("a", "t", 4, 2);
            flowGraph.AddEdge("b", "t", 6, 4);

            FlowGraph<string> flowGraphCopy = flowGraph.Copy();

            Assert.IsFalse(ReferenceEquals(flowGraph.Vertices, flowGraphCopy.Vertices));
            Assert.IsFalse(ReferenceEquals(flowGraph.Edges, flowGraphCopy.Edges));

            Assert.IsFalse(ReferenceEquals(flowGraph.Vertices[0], flowGraphCopy.Vertices[0]));
            Assert.IsFalse(ReferenceEquals(flowGraph.Edges[0], flowGraphCopy.Edges[0]));

        }
    }
}
