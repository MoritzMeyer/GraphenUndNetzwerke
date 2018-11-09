using GraphCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphSearch
{
    public static class StronglyConnectedComponents
    {
        private static int i = 0;
        private static Dictionary<Node, int> lowpt = new Dictionary<Node, int>();
        private static Dictionary<Node, int> lowvine = new Dictionary<Node, int>();
        private static Stack<Node> nodeStack = new Stack<Node>();
        private static List<List<Node>> strongComponents;

        public static List<List<Node>> Search(Graph<Node> graph)
        {
            i = 0;
            lowpt = new Dictionary<Node, int>();
            lowvine = new Dictionary<Node, int>();
            nodeStack = new Stack<Node>();
            strongComponents = new List<List<Node>>();

            graph.Nodes.ForEach(n =>
            {
                if (n.Number == null)
                {
                    StrongConnect(n);
                }
            });

            return strongComponents;
        }

        private static void StrongConnect(Node v)
        {
            lowpt.Add(v, i);
            lowvine.Add(v, i);
            v.Number = i;
            nodeStack.Push(v);
            i++;

            foreach (Node w in v.Neighbors)
            {
                if (w.Number == null) // (v,w) is a tree arc
                {
                    StrongConnect(w);
                    lowpt[v] = Math.Min(lowpt[v], lowpt[w]);
                    lowvine[v] = Math.Min(lowvine[v], v.Number.Value);
                }
                else if (w.Neighbors.Contains(v)) // (v,w) is a frond
                {
                    lowpt[v] = Math.Min(lowpt[v], w.Number.Value);
                }
                else if (w.Number.Value < v.Number.Value) // (v,w) is a vine
                {
                    if (nodeStack.Contains(w))
                    {
                        lowvine[v] = Math.Min(lowvine[v], w.Number.Value);
                    }
                }
            }

            if ((lowpt[v] == v.Number) && (lowvine[v] == v.Number.Value)) // v is the root of a component
            {
                List<Node> strongComponent = new List<Node>();
                while (nodeStack.First().Number.Value >= v.Number.Value)
                {
                    Node w = nodeStack.Pop();
                    strongComponent.Add(w);                    
                }
            }            
        }
    }
}
