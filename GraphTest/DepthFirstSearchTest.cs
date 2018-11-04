using GraphCollection;
using GraphSearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphTest
{
    [TestClass]
    public class DepthFirstSearchTest
    {
        [TestMethod]
        public void DepthFirstSearch_must_work()
        {
            Node zerozero = new Node("0-0");
            Node threezero = new Node("3-0");
            Node zerothree = new Node("0-3");
            Node threefive = new Node("3-5");
            Node threethree = new Node("3-3");
            Node onefive = new Node("1-5");
            Node onezero = new Node("1-0");
            Node zeroone = new Node("0-1");
            Node threeone = new Node("3-1");
            Node zerofour = new Node("0-4");

            Node zerofive = new Node("0-5");
            Node threetwo = new Node("3-2");
            Node zerotwo = new Node("0-2");
            Node twozero = new Node("2-0");
            Node twofive = new Node("2-5");
            Node threefour = new Node("3-4");

            zerozero.AddNeighbor(threezero);
            zerozero.AddNeighbor(zerofive);

            threezero.AddNeighbor(zerothree);
            threezero.AddNeighbor(threefive);

            zerofive.AddNeighbor(threefive);
            zerofive.AddNeighbor(threetwo);

            zerothree.AddNeighbor(threethree);
            threethree.AddNeighbor(onefive);
            onefive.AddNeighbor(onezero);
            onezero.AddNeighbor(zeroone);
            zeroone.AddNeighbor(threeone);
            threeone.AddNeighbor(zerofour);

            threetwo.AddNeighbor(zerotwo);
            zerotwo.AddNeighbor(twozero);
            twozero.AddNeighbor(twofive);
            twofive.AddNeighbor(threefour);

            Graph<Node> graph = new Graph<Node>(zerozero);
            Assert.IsTrue(DepthFirstSearch.Search(graph, zerozero, threefour));
        }
    }
}
