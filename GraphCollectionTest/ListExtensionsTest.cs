using GraphCollection;
using GraphCollection.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GraphCollectionTest
{
    [TestClass]
    public class ListExtensionsTest
    {
        #region TryGetValue_must_work
        /// <summary>
        /// Testet die Methode TryGetValue.
        /// </summary>
        [TestMethod]
        public void TryGetValue_must_work()
        {
            TwoBuckets tb1 = new TwoBuckets(3, 5, 2, 4);
            TwoBuckets tb2 = new TwoBuckets(2, 7, 1, 5);
            TwoBuckets tb3 = new TwoBuckets(4, 9, 3, 7);

            List<TwoBuckets> list = new List<TwoBuckets>
            {
                tb1,
                tb2,
                tb3
            };

            Assert.IsTrue(list.TryGetValue(new TwoBuckets(3, 5, 2, 4), out TwoBuckets tb4));
            Assert.AreEqual(tb1, tb4);
            Assert.IsTrue(object.ReferenceEquals(tb1, tb4));

            Assert.IsTrue(list.TryGetValue(new TwoBuckets(2, 7, 1, 5), out TwoBuckets tb5));
            Assert.AreEqual(tb2, tb5);
            Assert.IsTrue(object.ReferenceEquals(tb2, tb5));

            Assert.IsTrue(list.TryGetValue(new TwoBuckets(4, 9, 3, 7), out TwoBuckets tb6));
            Assert.AreEqual(tb3, tb6);
            Assert.IsTrue(object.ReferenceEquals(tb3, tb6));
        }
        #endregion
    }
}
