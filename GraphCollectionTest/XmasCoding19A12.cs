using System;
using System.IO;
using GraphCollection;
using GraphCollection.SearchAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GraphCollectionTest
{
    [TestClass]
    public class XmasCoding19A12
    {
        [TestMethod]
        public void CalcMinimumCost()
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "TestFiles", "xmasCoding12.txt");
            Graph<string> graph = GraphGenerator.LoadXmasCodingGraph(path);
            graph.Vertices.OrderBy(v => v.Value);
            
            string allPairsShortestPath = graph.AllPairsShortestPath();
        }
    }
}
