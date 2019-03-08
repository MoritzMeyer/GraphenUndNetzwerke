using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollection.SearchAlgorithms
{
    public static class MaximumFlowExtensions
    {
        public static FlowGraph<T> FordFulkerson<T>(this FlowGraph<T> graph)
        {
            if (graph.Edges.Where(e => !e.Capacity.HasValue).Any())
            {
                throw new ArgumentException("In dem Graphen muss für jede Kante eine Kapazität definiert sein.");
            }

            // Sicherstellen, dass der Fluss bei allen Kanten initialisiert ist.
            foreach (Edge<T> e in graph.Edges.Where(e => !e.Flow.HasValue))
            {
                e.Flow = 0;
            }

            // Eine Kopie des Graphens erstellen.
            FlowGraph<T> maxFlowGraph = graph.Copy();

            // Den Residualgraph erstellen
            FlowGraph<T> residualGraph = maxFlowGraph.GetResidualGraph();

            while(residualGraph.DepthFirstSearch(residualGraph.Source, residualGraph.Target, out List<Edge<T>> path))
            {
                int minAugmentingFlow = path.Select(e => Math.Abs(e.Flow.Value)).Min();

                foreach (Edge<T> edge in path)
                {
                    //Edge<T> edgeToModify = maxFlowGraph.Edges.Where(e => e.From.Equals(edge.From) && e.To.Equals(edge.To)).Single();
                    Edge<T> edgeToModify = maxFlowGraph.GetEdge(edge.From, edge.To);
                    if (edge.Flow.Value > 0)
                    {
                        edgeToModify.Flow += minAugmentingFlow;
                    }
                    else
                    {
                        edgeToModify.Flow -= minAugmentingFlow;
                    }
                    // edgeToModify.Flow += edge.Flow;
                }

                residualGraph = maxFlowGraph.GetResidualGraph();
            }

            return maxFlowGraph;
        }

        #region GetResidualGraph
        /// <summary>
        /// Berechnet zu einem Flussgraphen den Residual-Graphen.
        /// </summary>
        /// <typeparam name="T">Der Datentyp des Graphen.</typeparam>
        /// <param name="graph">Der Graph.</param>
        /// <returns>Der Residualgraph.</returns>
        public static FlowGraph<T> GetResidualGraph<T> (this FlowGraph<T> graph)
        {
            if (graph.Edges.Where(e => !e.Capacity.HasValue).Any())
            {
                throw new ArgumentException("In dem Graphen muss für jede Kante eine Kapazität definiert sein.");
            }

            // Sicherstellen, dass der Fluss bei allen Kanten initialisiert ist.
            foreach (Edge<T> e in graph.Edges.Where(e => !e.Flow.HasValue))
            {
                e.Flow = 0;
            }

            FlowGraph<T> residualGraph = new FlowGraph<T>(graph.Source.Value, graph.Target.Value, graph.Vertices.Select(v => v.Value), isDirected: true);

            foreach (Edge<T> e in graph.Edges)
            {
                if ((e.Capacity - e.Flow) > 0)
                {
                    residualGraph.AddEdge(e.From, e.To, capacity: e.Capacity.Value, flow: e.Capacity.Value - e.Flow.Value);
                }

                if (e.Flow > 0)
                {
                    residualGraph.AddEdge(e.To, e.From, capacity: e.Capacity.Value, flow: e.Flow.Value * (-1));
                }
                
            }

            return residualGraph;
        }
        #endregion
    }
}
