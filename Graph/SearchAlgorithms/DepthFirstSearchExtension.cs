using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphCollection.SearchAlgorithms
{
    /// <summary>
    /// Extensionklasse für die Tiefensuche
    /// </summary>
    public static class DepthFirstSearchExtension
    {
        #region DepthFirstSearch
        /// <summary>
        /// Führt eine Tiefensuche in dem Graphen durch.s
        /// </summary>
        /// <typeparam name="T">Der Datentyp der Werte in den Vertices.</typeparam>
        /// <param name="graph">Der Graph, der durchsucht werden soll.</param>
        /// <param name="start">Der Start-Vertex, von dem aus gesucht werden soll.</param>
        /// <param name="goal">Der gesuchte Vertex.</param>
        /// <returns>True, wenn die Suche erfolgreich war, false sonst.</returns>
        public static bool DepthFirstSearch<T>(this Graph<T> graph, Vertex<T> start, Vertex<T> goal)
        {
            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
            graph.ResetVisitedProperty();
            start = graph.GetVertex(start);

            if (!graph.HasVertex(start))
            {
                throw new ArgumentException($"Der Graph enthält den StartKnoten({start}) nicht.");
            }

            if (!graph.HasVertex(goal))
            {
                throw new ArgumentException($"Der Graph enthält den Zielknoten({goal}) nicht.");
            }

            bool sucess = SearchHelper<T>(ref stack, start, goal);           

            if (sucess)
            {
                Vertex<T> previous = stack.Pop();
                string output = previous.ToString();

                while(stack.Count > 0)
                {
                    Vertex<T> actual = stack.Pop();
                    if (graph.HasEdge(previous, actual))
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

            return sucess;
        }
        #endregion

        #region SearchHelper
        /// <summary>
        /// Hilfsmethode für die Tiefensuche.
        /// </summary>
        /// <typeparam name="T">Der Datentyp der Werte in den Vertices.</typeparam>
        /// <param name="stack">Der Stack mit den abzuarbeitenden Vertices.</param>
        /// <param name="start">Der Start Vertex.</param>
        /// <param name="goal">Der Ziel Vertex.</param>
        /// <returns>True, wenn der gesuchte Vertex gefunden wurde, false wenn nicht.</returns>
        internal static bool SearchHelper<T>(ref Stack<Vertex<T>> stack, Vertex<T> start, Vertex<T> goal)
        {
            stack.Push(start);
            start.Visit();

            if (start.Equals(goal))
            {
                return true;
            }

            while (stack.Count > 0)
            {
                Vertex<T> actual = stack.First();
                if (actual.Neighbors.Where(n => !n.IsVisited).Any())
                {
                    foreach (Vertex<T> neighbor in actual.Neighbors)
                    {
                        if (!neighbor.IsVisited)
                        {
                            neighbor.Visit();
                            if (neighbor.Equals(goal))
                            {
                                return true;
                            }

                            stack.Push(neighbor);
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
        #endregion
    }
}
