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
                foreach(Vertex<T> neighbor in vertex.Neighbors)
                {
                    neighbor.InDegree++;
                }
            }

            // Die workerQueue mit den Knoten initialisieren, deren InDegree 0 ist.
            workerQueue = new Queue<Vertex<T>>(graph.Vertices.Where(v => v.InDegree == 0));

            while(workerQueue.Count > 0)
            {
                // Den nächsten Knoten aus der Queue holen und die SortOrder setzen.
                Vertex<T> actualNode = workerQueue.Dequeue();
                actualNode.SortOrder = sortOrder++;

                foreach(Vertex<T> neighbor in actualNode.Neighbors)
                {
                    neighbor.InDegree--;
                    if(neighbor.InDegree == 0)
                    {
                        workerQueue.Enqueue(neighbor);
                    }
                }
            }

            return graph.VerticesInSortOrder();
        }
        #endregion
    }
}
