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
            SearchHelper(ref stack, start, goal);

            return stack;
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
                foreach(Node node in start.Neighbours)
                {
                    stack.Pop();
                    if (!node.Visited)
                    {
                        node.Visited = true;
                        if (node.Equals(goal))
                        {
                            return true;
                        }
                        
                        stack.Push(node);
                    }
                }
            }

            return false;
        }
    }
}
