using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphCollection.SearchAlgorithms
{
    public static class StronglyConnectedComponents<T>
    {
        #region fields
        private static int i;
        private static Dictionary<Vertex<T>, int> lowpt;
        private static Dictionary<Vertex<T>, int> lowvine;
        private static Stack<Vertex<T>> vertexStack;
        private static List<List<Vertex<T>>> strongComponents;
        #endregion

        #region Search
        /// <summary>
        /// Startet die Suche nach den 'StronglyConnectedComponents' in einem Graph.
        /// </summary>
        /// <param name="graph">Der Graph in dem gesucht werden soll.</param>
        /// <returns>Eine Liste mit StronglyConnectedComponents.</returns>
        public static List<List<Vertex<T>>> Search(Graph<T> graph)
        {
            if (!graph.IsDirected)
            {
                throw new ArgumentException("Die 'strongly connected components' können nur in gerichteten Graphen berechnet werden.");
            }

            i = 0;
            lowpt = new Dictionary<Vertex<T>, int>();
            lowvine = new Dictionary<Vertex<T>, int>();
            vertexStack = new Stack<Vertex<T>>();
            strongComponents = new List<List<Vertex<T>>>();

            foreach(Vertex<T> vertex in graph.Vertices)
            {
                if (vertex.Number == null)
                {
                    StrongConnect(vertex);
                }
            }

            return strongComponents;
        }
        #endregion

        #region StrongConnect
        /// <summary>
        /// Die Funktion geht von einem Knoten aus einem Graphen aus los und sucht eine StronglyConnectetComponent.
        /// </summary>
        /// <param name="v">Der Knoten von dem aus gesucht werden soll.</param>
        private static void StrongConnect(Vertex<T> v)
        {
            lowpt.Add(v, i);
            lowvine.Add(v, i);
            v.Number = i;
            i++;
            vertexStack.Push(v);

            foreach (Vertex<T> w in v.Neighbors)
            {
                if (w.Number == null) // (v,w) is a tree arc
                {
                    StrongConnect(w);
                    lowpt[v] = Math.Min(lowpt[v], lowpt[w]);
                    lowvine[v] = Math.Min(lowvine[v], lowvine[w]);
                }
                else if (w.Neighbors.Contains(v)) // (v,w) is a frond
                {
                    lowpt[v] = Math.Min(lowpt[v], w.Number.Value);
                }
                else if (w.Number.Value < v.Number.Value) // (v,w) is a vine
                {
                    if (vertexStack.Contains(w))
                    {
                        lowvine[v] = Math.Min(lowvine[v], w.Number.Value);
                    }
                }
            }

            if ((lowpt[v] == v.Number) && (lowvine[v] == v.Number.Value)) // v is the root of a component
            {
                List<Vertex<T>> strongComponent = new List<Vertex<T>>();
                while (vertexStack.Count() > 0 && vertexStack.First().Number.Value >= v.Number.Value)
                {
                    Vertex<T> w = vertexStack.Pop();
                    strongComponent.Add(w);
                }

                strongComponents.Add(strongComponent);
            }
        }
        #endregion
    }
}
