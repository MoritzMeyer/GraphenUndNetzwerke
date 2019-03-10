using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollection.SearchAlgorithms
{
    public static class MinimalSpanningTree
    {
        #region Kruskal
        /// <summary>
        /// Der Kruskal Algorithmus für minimale Spannbäume
        /// </summary>
        /// <typeparam name="T">Der Datentyp des Graphen.</typeparam>
        /// <param name="graph">Der Graph.</param>
        /// <returns>Der minimale Spannbaum.</returns>
        public static Graph<T> Kruskal<T>(this Graph<T> graph)
        {
            if (!graph.IsWeighted)
            {
                throw new InvalidOperationException("Der Algorithmus von Kruskal kann nur auf gewichteten Graphen durchgeführt werden.");
            }

            if (graph.IsDirected)
            {
                throw new InvalidOperationException("Der Algorithmus von Kruskal kann nur auf ungerichteten Graphen durchgeführt werden.");
            }

            // Den neuen Graphen und die working List initialisieren.
            Graph<T> minimalSpanningTree = new Graph<T>(graph.Vertices);
            List<Edge<T>> workingList = new List<Edge<T>>();

            foreach(Edge<T> edge in graph.Edges)
            {
                workingList.Add(new Edge<T>(edge));
            }

            workingList = workingList.OrderBy(e => e.Weight).ToList();

            while (workingList.Count > 0)
            {
                Edge<T> actualEdge = workingList.First();
                workingList.Remove(actualEdge);

                minimalSpanningTree.AddEdge(actualEdge.From, actualEdge.To, actualEdge.Weight);

                if (minimalSpanningTree.HasCycle())
                {
                    minimalSpanningTree.RemoveEdge(actualEdge);
                }
            }

            return minimalSpanningTree;
        }
        #endregion

        #region Prim
        /// <summary>
        /// Der Prim-Algorithmus für minimale Spannbäume
        /// </summary>
        /// <typeparam name="T">Der Datentyp des Graphen.</typeparam>
        /// <param name="graph">Der Graph.</param>
        /// <param name="start">Wenn angegeben, wird der min. spanningtree ausgehend von diesem Knoten generiert.</param>
        /// <returns>Der minimale Spannbaum.</returns>
        public static Graph<T> Prim<T>(this Graph<T> graph, Vertex<T> start = null)
        {
            if (!graph.IsWeighted)
            {
                throw new InvalidOperationException("Der Algorithmus von Kruskal kann nur auf gewichteten Graphen durchgeführt werden.");
            }

            if (graph.IsDirected)
            {
                throw new InvalidOperationException("Der Algorithmus von Kruskal kann nur auf ungerichteten Graphen durchgeführt werden.");
            }

            // Den minimalen Spannbaum und die Workingliste initialisieren
            Graph<T> minimalSpanningTree = new Graph<T>();
            List<Vertex<T>> workingList = new List<Vertex<T>>();
            foreach (Vertex<T> vertex in graph.Vertices)
            {
                vertex.SortOrder = int.MaxValue;
                workingList.Add(vertex);
            }

            // Den Startknoten auswählen.
            if (start != null)
            {
                start = graph.GetVertex(start);
                workingList[workingList.IndexOf(start)].SortOrder = 0;
            }
            else
            {
                workingList.First().SortOrder = 0;
            }

            // Die Liste initial sortieren.
            workingList = workingList.OrderBy(v => v.SortOrder).ToList();

            while (workingList.Count > 0)
            {
                // Den ersten Knoten der Liste entnehmen
                Vertex<T> actual = workingList.First();
                workingList.Remove(actual);

                // Den aktuellen Knoten dem Baum hinzufügen
                if (!minimalSpanningTree.HasVertex(actual))
                {
                    minimalSpanningTree.AddVertex(new Vertex<T>(actual.Value));
                }

                // Für alle Nachbarn von actual, die in der WorkingList enthalten sind.
                List<Vertex<T>> neighbours = graph.GetNeighbours(actual);
                foreach (Vertex<T> neighbour in neighbours.Where(n => workingList.Contains(n)))
                {
                    Edge<T> actualEdge = graph.GetEdge(actual, neighbour);
                    
                    if (actualEdge.Weight < neighbour.SortOrder)
                    {
                        // Denn Nachbarn dem Baum hinzufügen, wenn noch nicht vorhanden.
                        if (!minimalSpanningTree.HasVertex(neighbour))
                        {
                            minimalSpanningTree.AddVertex(new Vertex<T>(neighbour.Value));
                        }

                        // Eine möglich vorhandene Kante entfernen
                        IEnumerable<Edge<T>> existingEdges = minimalSpanningTree.Edges.Where(e => e.From.Equals(neighbour) || e.To.Equals(neighbour));
                        if (existingEdges.Any())
                        {
                            minimalSpanningTree.RemoveEdge(existingEdges.Single());
                        }

                        minimalSpanningTree.AddEdge(actual.Value, neighbour.Value, actualEdge.Weight);
                        neighbour.SortOrder = actualEdge.Weight.Value;
                    }
                }

                workingList = workingList.OrderBy(v => v.SortOrder).ToList();
            }

            return minimalSpanningTree;
        }
        #endregion
    }
}
