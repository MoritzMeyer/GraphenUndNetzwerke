using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollection.SearchAlgorithms
{
    public static class MinimalSpanningTreeKruskal
    {
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
    }
}
