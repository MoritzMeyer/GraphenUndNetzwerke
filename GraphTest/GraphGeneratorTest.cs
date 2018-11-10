using GraphCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    [TestClass]
    public class GraphGeneratorTest
    {
        #region GetPossibleChildNodes_must_work
        /// <summary>
        /// Tests the Method 'GetPossibleChildNodes'
        /// </summary>
        [TestMethod]
        public void GetPossibleChildNodes_must_work()
        {
            // Node from where childNodes should be calculated
            BucketNode bucketNode = new BucketNode("0-0", 3, 5, 0, 0);

            // Get the list with possible child nodes.
            IEnumerable<BucketNode> possibleChildNodes = GraphGenerator.GetPossibleChildNodes(bucketNode);

            // Check result
            Assert.AreEqual(2, possibleChildNodes.Count());
            Assert.AreEqual(1, possibleChildNodes.Where(n => n.Caption.Equals("3-0")).Count());
            Assert.AreEqual(1, possibleChildNodes.Where(n => n.Caption.Equals("0-5")).Count());

            // Get Node instances.
            BucketNode threeZero = possibleChildNodes.Where(n => n.Caption.Equals("3-0")).Single();
            BucketNode zeroFive = possibleChildNodes.Where(n => n.Caption.Equals("0-5")).Single();

            // Check node 1
            Assert.AreEqual(3, threeZero.Capacity1);
            Assert.AreEqual(5, threeZero.Capacity2);
            Assert.AreEqual(3, threeZero.Content1);
            Assert.AreEqual(0, threeZero.Content2);

            // Check node 2
            Assert.AreEqual(3, zeroFive.Capacity1);
            Assert.AreEqual(5, zeroFive.Capacity2);
            Assert.AreEqual(0, zeroFive.Content1);
            Assert.AreEqual(5, zeroFive.Content2);

            IEnumerable<BucketNode> possibleChildNodesThreeZero = GraphGenerator.GetPossibleChildNodes(threeZero);
            Assert.AreEqual(3, possibleChildNodesThreeZero.Count());
            Assert.AreEqual(1, possibleChildNodesThreeZero.Where(n => n.Caption.Equals("0-3")).Count());
            Assert.AreEqual(1, possibleChildNodesThreeZero.Where(n => n.Caption.Equals("3-5")).Count());
            Assert.AreEqual(1, possibleChildNodesThreeZero.Where(n => n.Caption.Equals("0-0")).Count());

            IEnumerable<BucketNode> possibleChildNodesZeroFive = GraphGenerator.GetPossibleChildNodes(zeroFive);
            Assert.AreEqual(3, possibleChildNodesZeroFive.Count());
            Assert.AreEqual(1, possibleChildNodesZeroFive.Where(n => n.Caption.Equals("3-2")).Count());
            Assert.AreEqual(1, possibleChildNodesZeroFive.Where(n => n.Caption.Equals("3-5")).Count());
            Assert.AreEqual(1, possibleChildNodesZeroFive.Where(n => n.Caption.Equals("0-0")).Count());
        }
        #endregion

        #region BucketGraph_must_work
        /// <summary>
        /// Tests the Method 'BucketGraph'
        /// </summary>
        [TestMethod]
        public void BucketGraph_must_work()
        {
            Graph<BucketNode> bucketGraph = GraphGenerator.BucketGraph(3, 5);
        }
        #endregion
    }
}
