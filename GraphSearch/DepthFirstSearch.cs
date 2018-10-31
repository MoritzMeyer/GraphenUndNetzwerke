using GraphCollection;
using System;
using System.Collections.Generic;

namespace GraphSearch
{
    public static class DepthFirstSearch
    {
        public static IEnumerable<Node> Search(Graph graph, Node start, Node goal)
        {
            Stack<Node> stack = new Stack<Node>();
        }

        public static bool SearchHelper(ref Stack<Node> stack, Node start, Node goal)
        {
            stack.Push(start);
            start.Visited = true;
            if (start.Equals(goal))
            {
                return true;
            }

            while(stack.Count > 0)
            {

            }
        }
    }
}
