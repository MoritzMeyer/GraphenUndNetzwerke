using GraphCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]

namespace GraphSearch
{
    public static class DepthFirstSearch
    {
        public static bool Search<T>(Graph<T> graph, T start, T goal) where T : Node
        {
            Stack<T> stack = new Stack<T>();
            bool success = SearchHelper<T>(ref stack, start, goal);

            if (success)
            {
                T previous = stack.Pop();
                string output = previous.ToString();

                while(stack.Count > 0)
                {
                    T actual = stack.Pop();
                    if (previous.HasChild(actual))
                    {
                        previous = actual;
                        output = previous.ToString() + " -> " + output;
                    }
                }

                Console.WriteLine("DepthFirstSearch was successful!: ");
                Console.WriteLine(output);
            }
            else
            {
                Console.WriteLine("DepthFirstSearch wasn't successful!");
            }

            return success;
        }

        internal static bool SearchHelper<T>(ref Stack<T> stack, T start, T goal) where T : Node
        {
            stack.Push(start);
            start.Visited = true;
            if (start.Equals(goal))
            {
                return true;
            }

            while(stack.Count > 0)
            {
                T actual = stack.First();
                if (actual.Neighbors.Where(n => !n.Visited).Any())
                {
                    foreach (T node in actual.Neighbors)
                    {
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
                else
                {
                    stack.Pop();
                }                
            }

            return false;
        }
    }
}
