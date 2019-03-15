using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollection.SearchAlgorithms
{
    public static class DijkstraExtension
    {
        #region Dijkstra
        /// <summary>
        /// Ermittelt die kürzeseten Wege zwischen alle Knoten im Graphen, soweit kein Zielknoten angegeben ist.
        /// Ist hingegen einer angegeben, werden die Distanzen nur so lange berechnet, bis die Distanz des Zielknotens berechnet wurde.
        /// </summary>
        /// <typeparam name="T">Der Datentyp des Graphen.</typeparam>
        /// <param name="graph">Der Graph.</param>
        /// <param name="start">Der Startknoten</param>
        /// <param name="target">Wenn angegeben, wird der Algorithmus so lange ausgeführt, bis die Distanz dieses Knotens berechnet wurde.</param>
        public static Graph<T> Dijkstra<T>(this Graph<T> graph, Vertex<T> start, Vertex<T> target = null)
        {
            if (!graph.IsWeighted)
            {
                throw new ArgumentException("Der Dijkstra Algorithmus kann nur auf Graphen mit gewichteten Kanten durchgeführt werden.");
            }

            // Die Workinglist initialisieren.
            List<Vertex<T>> workingList = new List<Vertex<T>>();

            // Die Distanzen initialisieren.
            foreach (Vertex<T> vertex in graph.Vertices)
            {
                if (vertex.Equals(start))
                {
                    vertex.DijkstraDistance = 0;
                }
                else
                {
                    vertex.DijkstraDistance = int.MaxValue;
                }

                // Den Knoten der working List hinzufügen.
                workingList.Add(vertex);
            }

            start.DijkstraAncestor = start;

            while (workingList.Count > 0)
            {
                // Ermittel den Knoten mit der aktuell geringsten Dijkstra Distanz
                Vertex<T> actual = workingList
                    .Aggregate((curMin, v) =>
                        (curMin == null || (v.DijkstraDistance ?? int.MaxValue) < curMin.DijkstraDistance ? v : curMin));

                workingList.Remove(actual);
                //actual.Visit();

                // Dijkstradistanz und -vorgänger anpassen (falls nötig) für alle Nachbarknoten der ausgehenden Kanten des aktuellen Knotens
                foreach (Edge<T> edge in graph.Edges.Where(e => e.From.Equals(actual)))
                {
                    if (workingList.Contains(edge.To) && graph.GetVertex(edge.From).DijkstraDistance + edge.Weight < graph.GetVertex(edge.To).DijkstraDistance)
                    {
                        edge.To.DijkstraDistance = edge.From.DijkstraDistance + edge.Weight;
                        edge.To.DijkstraAncestor = actual;
                    }
                }

                // im ungerichteten Graphen müssen auch die Nachbarn der rückläufigen Kanten angepasst werden.
                if (!graph.IsDirected)
                {
                    foreach (Edge<T> edge in graph.Edges.Where(e => e.To.Equals(actual)))
                    {
                        if (workingList.Contains(edge.From) && graph.GetVertex(edge.To).DijkstraDistance + edge.Weight < graph.GetVertex(edge.From).DijkstraDistance)
                        {
                            edge.From.DijkstraDistance = edge.To.DijkstraDistance + edge.Weight;
                            edge.From.DijkstraAncestor = actual;
                        }
                    }
                }

                // Wenn die Distanz zum gesuchten Knoten gefunden ist, kann hier abgebrochen werden.
                if (target != null && actual.Equals(target))
                {
                    return graph;
                }
            }

            return graph;
        }
        #endregion
    }
}
