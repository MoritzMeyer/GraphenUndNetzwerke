using GraphCollection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphCollection.SearchAlgorithms
{
    public static class TopSortExtension
    {
        #region TopSort
        public static SortedList<int, Vertex<T>> TopSort<T>(this Graph<T> graph)
        {
            Queue<Vertex<T>> workerQueue;
            int sortOrder = 0;

            // Alle InDegrees der Knoten auf 0 initialisieren.
            foreach(Vertex<T> vertex in graph.Vertices)
            {
                vertex.InDegree = 0;
            }

            // Die InDegrees der Knoten berechnen.
            foreach(Vertex<T> vertex in graph.Vertices)
            {
                foreach(Vertex<T> neighbor in graph.GetNeighbours(vertex))
                {
                    neighbor.InDegree++;
                }
            }

            // Die workerQueue mit den Knoten initialisieren, deren InDegree 0 ist.
            workerQueue = new Queue<Vertex<T>>(graph.Vertices.Where(v => v.InDegree == 0));

            while(workerQueue.Count > 0 && sortOrder <= graph.Vertices.Count)
            {
                // Den nächsten Knoten aus der Queue holen und die SortOrder setzen.
                Vertex<T> actualNode = workerQueue.Dequeue();
                actualNode.SortOrder = sortOrder++;

                List<Vertex<T>> neighbours = graph.GetNeighbours(actualNode);
                foreach(Vertex<T> neighbor in neighbours)
                {
                    neighbor.InDegree--;
                    if(neighbor.InDegree == 0)
                    {
                        workerQueue.Enqueue(neighbor);
                    }
                }
            }

            if (sortOrder != graph.Vertices.Count)
            {
                throw new ArgumentException("Der Graph enthält einen Zyklus");
            }

            return graph.VerticesInSortOrder();
        }
        #endregion

        #region CountTopologies
        /// <summary>
        /// Zählt die Anzahl an möglichen TopologieSortierungen eines Graphens.
        /// </summary>
        /// <typeparam name="T">Der Typ der Knoten.</typeparam>
        /// <param name="graph">Der Graph.</param>
        /// <returns>Die Anzahl.</returns>
        public static int CountTopologies<T>(this Graph<T> graph)
        {
            int numerator = graph.Vertices.Count.Faculty();
            int denominator = 1;

            SortedList<int, Vertex<T>> sortedVertices = graph.TopSort();
            Queue<Vertex<T>> workerQueue = new Queue<Vertex<T>>(sortedVertices.Values);

            while(workerQueue.Count > 0)
            {
                Vertex<T> actualVertex = workerQueue.Dequeue();

                denominator = denominator * graph.CountVerticesOfSubgraph(actualVertex);
            }

            return numerator / denominator;
        }
        #endregion
    }
}
