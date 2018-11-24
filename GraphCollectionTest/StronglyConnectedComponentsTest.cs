using System;
using System.Collections.Generic;
using System.IO;
using GraphCollection;
using GraphCollection.SearchAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphCollectionTest
{
    [TestClass]
    public class StronglyConnectedComponentsTest
    {
        #region Search_must_work
        /// <summary>
        /// Testet den Algorithmus zu den StronglyConnectedComponents
        /// </summary>
        [TestMethod]
        public void Search_must_work()
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "TestFiles", "TestGraph.txt");
            Graph<string> graph = GraphGenerator.LoadFromFile(path, true);

            List<List<Vertex<string>>> stronglyConnectedComponents = StronglyConnectedComponents<string>.Search(graph);

            Assert.AreEqual(2, stronglyConnectedComponents.Count);
            Assert.AreEqual(4, stronglyConnectedComponents[0].Count);
            Assert.AreEqual(1, stronglyConnectedComponents[1].Count);
        }
        #endregion
    }
}
