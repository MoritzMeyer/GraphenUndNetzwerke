using GraphCollection;
using GraphSearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace Tests
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
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "TestFiles", "TestGraph.txt");
            Graph<Node> graph = Graph<Node>.Load(path);

            List<List<Node>> stronglyConnectedComponents = StronglyConnectedComponents.Search(graph);

            Assert.AreEqual(2, stronglyConnectedComponents.Count);
            Assert.AreEqual(4, stronglyConnectedComponents[0].Count);
            Assert.AreEqual(1, stronglyConnectedComponents[1].Count);
        }
        #endregion
    }
}
