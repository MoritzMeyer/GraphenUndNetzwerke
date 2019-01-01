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
        public static bool DepthFirstSearch<T>(this Graph<T> graph, Vertex<T> start, Vertex<T> goal, out List<Edge<T>> path)
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

            bool sucess = SearchHelper<T>(graph, ref stack, start, goal);

            // Die Liste mit Kanten auf dem Pfad initialisieren.
            path = new List<Edge<T>>();

            if (sucess)
            {
                Vertex<T> previous = stack.Pop();
                string output = previous.ToString();

                while(stack.Count > 0)
                {
                    Vertex<T> actual = stack.Pop();
                    if (graph.HasEdge(actual, previous))
                    {
                        // Die Kante dem Pfad hinzufügen.
                        path.Add(graph.GetEdge(actual, previous));

                        previous = actual;
                        output = previous.ToString() + " -> " + output;
                    }
                }

                // Die Liste mit Kanten auf dem Pfad wird in falscher Reihenfolge befüllt, hier die richtige Reihenfolge herstellen.
                path.Reverse();

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
        /// <param name="graph">Der Graph.</param>
        /// <typeparam name="T">Der Datentyp der Werte in den Vertices.</typeparam>
        /// <param name="stack">Der Stack mit den abzuarbeitenden Vertices.</param>
        /// <param name="start">Der Start Vertex.</param>
        /// <param name="goal">Der Ziel Vertex.</param>
        /// <returns>True, wenn der gesuchte Vertex gefunden wurde, false wenn nicht.</returns>
        internal static bool SearchHelper<T>(Graph<T> graph, ref Stack<Vertex<T>> stack, Vertex<T> start, Vertex<T> goal)
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
                List<Vertex<T>> neighbours = graph.GetNeighbours(actual);
                if (neighbours.Where(n => !n.IsVisited).Any())
                {
                    foreach (Vertex<T> neighbor in neighbours)
                    {
                        if (!neighbor.IsVisited)
                        {
                            neighbor.Visit();
                            if (neighbor.Equals(goal))
                            {
                                // Das Ziel muss auch noch auf den Stack.
                                stack.Push(neighbor);
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

        #region HasCycle
        /// <summary>
        /// Prüft, ob der Graph einen Zyklus enthält.
        /// </summary>
        /// <typeparam name="T">Der Datentyp der Vertices.</typeparam>
        /// <param name="graph">Der Graph.</param>
        /// <returns>True, wenn der Graph einen Zyklus enthält, false wenn nicht.</returns>
        public static bool HasCycle<T>(this Graph<T> graph)
        {
            // Die visited Values zwischenspeichern.
            bool[] visitedVertices = new bool[graph.Vertices.Count];
            for (int i = 0; i < graph.Vertices.Count; i++)
            {
                visitedVertices[i] = graph.Vertices[i].IsVisited;
            }

            bool[] visitedEdges = new bool[graph.Edges.Count];
            for (int i = 0; i < graph.Edges.Count; i++)
            {
                visitedEdges[i] = graph.Edges[i].IsVisited;
            }

            // die Visited Values zurücksetzen
            graph.ResetVisitedProperty();

            bool hasCycle = HasCycleHelper<T>(graph, graph.Vertices[0]);

            // Die Visitedvalues wieder auf den Ursprung setzen
            for (int i = 0; i < graph.Vertices.Count; i++)
            {
                if (visitedVertices[i])
                {
                    graph.Vertices[i].Visit();
                }
            }

            for (int i = 0; i < graph.Edges.Count; i++)
            {
                graph.Edges[i].IsVisited = visitedEdges[i];
            }

            return hasCycle;
        }
        #endregion

        #region HasCycleHelper
        /// <summary>
        /// Rekursive Funktion, die nach einem Zyklus in einem Graphen sucht.
        /// </summary>
        /// <typeparam name="T">Der Datentyp der Vertices.</typeparam>
        /// <param name="start">Der Knoten, von dem aus gesucht werden soll.</param>
        /// <returns>True, wenn ein Zyklus gefunden wurde, false wenn nicht.</returns>
        public static bool HasCycleHelper<T>(Graph<T> graph, Vertex<T> start)
        {
            if (start.IsVisited)
            {
                return true;
            }

            start.Visit();
            List<Vertex<T>> neighbours = graph.GetNeighbours(start);
            foreach(Vertex<T> vertex in neighbours)
            {
                if (!graph.GetEdge(start, vertex).IsVisited)
                {
                    graph.GetEdge(start, vertex).IsVisited = true;
                    if (HasCycleHelper<T>(graph, vertex))
                    {
                        return true;
                    }

                }
            }

            return false;
        }
        #endregion
    }
}
