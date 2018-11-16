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
    public class TwoBucketsTest
    {
        #region Fill_must_work
        [TestMethod]
        public void Fill_must_work()
        {
            TwoBuckets tb1 = new TwoBuckets(3, 5);

            Assert.IsTrue(tb1.CanFillB1());
            Assert.IsTrue(tb1.CanFillB2());

            TwoBuckets tb2 = tb1.FillB1();
            TwoBuckets tb3 = tb1.FillB2();

            Assert.AreEqual(3, tb2.contentBucket1);
            Assert.AreEqual(5, tb3.contentBucket2);

            Assert.IsFalse(tb2.CanFillB1());
            Assert.IsFalse(tb3.CanFillB2());
        }
        #endregion

        #region Empty_must_work
        [TestMethod]
        public void Empty_must_work()
        {
            TwoBuckets tb1 = new TwoBuckets(3, 5);

            Assert.IsFalse(tb1.CanEmptyB1());
            Assert.IsFalse(tb1.CanEmptyB2());

            TwoBuckets tb2 = tb1.FillB1();
            TwoBuckets tb3 = tb1.FillB2();

            Assert.AreEqual(3, tb2.contentBucket1);
            Assert.AreEqual(5, tb3.contentBucket2);

            Assert.IsTrue(tb2.CanEmptyB1());
            Assert.IsTrue(tb3.CanEmptyB2());

            TwoBuckets tb4 = tb2.EmptyB1();
            TwoBuckets tb5 = tb3.EmptyB2();

            Assert.AreEqual(0, tb4.contentBucket1);
            Assert.AreEqual(0, tb5.contentBucket2);

        }
        #endregion

        #region FillFrom_must_work
        [TestMethod]
        public void FillFrom_must_work()
        {
            TwoBuckets tb1 = new TwoBuckets(3, 5, 3, 0);
            TwoBuckets tb2 = new TwoBuckets(3, 5, 0, 5);

            Assert.IsTrue(tb1.CanFillB2());
            Assert.IsTrue(tb2.CanFillB1());

            TwoBuckets tb3 = tb1.FillB2FromB1();
            TwoBuckets tb4 = tb2.FillB1FromB2();

            Assert.AreEqual(0, tb3.contentBucket1);
            Assert.AreEqual(3, tb3.contentBucket2);
            Assert.AreEqual(3, tb4.contentBucket1);
            Assert.AreEqual(2, tb4.contentBucket2);

            TwoBuckets tb5 = new TwoBuckets(0, 0);
            Assert.IsNull(tb5.FillB1FromB2());
            Assert.IsNull(tb5.FillB2FromB1());
        }
        #endregion
    }
}
