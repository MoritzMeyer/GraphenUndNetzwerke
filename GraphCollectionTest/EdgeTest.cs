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
    public class EdgeTest
    {
        [TestMethod]
        public void Equals_must_work()
        {
            Edge<int> e1 = new Edge<int>(new Vertex<int>(1), new Vertex<int>(2));
            Edge<int> e2 = new Edge<int>(new Vertex<int>(2), new Vertex<int>(1));
            Edge<int> e3 = new Edge<int>(new Vertex<int>(1), new Vertex<int>(2), isDirected: true);
            Edge<int> e4 = new Edge<int>(new Vertex<int>(1), new Vertex<int>(2));

            Assert.IsTrue(e1.Equals(e2));
            Assert.IsTrue(e1.Equals(e4));

            e1.IsDirected = true;
            
            Assert.IsFalse(e1.Equals(e2));
            Assert.IsTrue(e1.Equals(e3));

            e1.IsDirected = true;

            Assert.IsFalse(e1.Equals(e2));

            e1.Weight = 10;

            Assert.IsFalse(e1.Equals(e3));

            e3.Weight = 10;

            Assert.IsTrue(e1.Equals(e3));

            e1 = new Edge<int>(new Vertex<int>(1), new Vertex<int>(2));
            e2 = new Edge<int>(new Vertex<int>(1), new Vertex<int>(2));

            List<Edge<int>> list = new List<Edge<int>>() { e1, e2 };

            IEnumerable<Edge<int>> test1 = list.Where(e => e.Equals(e1));
            Assert.AreEqual(2, test1.Count());

            e1.IsDirected = true;
            IEnumerable<Edge<int>> test2 = list.Where(e => e.Equals(e2));            
            Assert.AreEqual(1, test2.Count());
        }
    }
}
