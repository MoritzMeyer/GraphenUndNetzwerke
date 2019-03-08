using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollection.SearchAlgorithms
{
    public static class AllPairsShortestPathExtension
    {
        #region AllPairsShortestPath
        /// <summary>
        /// Berechnet die AllPairsShortestPath => Floyd-Warshall Algorithm
        /// </summary>
        /// <typeparam name="T">Der Datentyp.</typeparam>
        /// <param name="graph">Der Graph.</param>
        /// <returns>Die AllPairsShortestPath als string.</returns>
        public static string AllPairsShortestPath<T>(this Graph<T> graph)
        {
            if (!graph.IsDirected || !graph.IsWeighted)
            {
                throw new ArgumentException("Der AllPairsShortestPath Algorithmus kann nur auf gerichteten Graphen mit gewichteten Kanten durchgeführt werden.");
            }

            Trictionary<Vertex<T>, Vertex<T>, int> weights = new Trictionary<Vertex<T>, Vertex<T>, int>();

            foreach(Edge<T> edge in graph.Edges)
            {
                if (edge.Weight == null)
                {
                    throw new ArgumentException($"Die Kante {edge.ToString()} besitzt kein Gewicht");
                }

                weights.Add(edge.From, edge.To, edge.Weight.Value);
            }

            foreach(Vertex<T> vertex in graph.Vertices)
            {
                weights.Add(vertex, vertex, 0);
            }

            foreach (Vertex<T> k in graph.Vertices)
            {
                foreach (Vertex<T> i in graph.Vertices)
                {
                    foreach (Vertex<T> j in graph.Vertices)
                    {
                        if (!weights.HasValueForKeys(i, k) || !weights.HasValueForKeys(k, j))
                        {
                            continue;
                        }

                        if (!weights.HasValueForKeys(i,j))
                        {
                            weights.Add(i, j, weights.Get(i, k) + weights.Get(k, j));
                        }
                        else
                        {
                            if ((weights.Get(i, j) > weights.Get(i, k) + weights.Get(k, j)))
                            {
                                weights.Set(i, j, weights.Get(i, k) + weights.Get(k, j));
                            }
                        }                        
                    }
                }
            }

            // Den String erstellen
            string stringMatrix = string.Empty;

            // Die maximale Länge einer Node-Repräsentation ermitteln.
            int maxVertexStringLength = graph.Vertices.Select(v => v.ToString()).Max(v => v.Length);

            if (maxVertexStringLength < weights.GetMaxStringLength())
            {
                maxVertexStringLength = weights.GetMaxStringLength();
            }            

            // Die Titelleiste erstellen.
            stringMatrix +=
                string.Concat(Enumerable.Repeat(" ", maxVertexStringLength)) +
                "||" +
                graph.Vertices
                    .Select(v => v.ToString())
                    .Aggregate((a, b) => a.CenterStringWithinLength(maxVertexStringLength) + "|" + b.CenterStringWithinLength(maxVertexStringLength)) +
                Environment.NewLine;

            stringMatrix += string.Concat(Enumerable.Repeat("=", stringMatrix.Length)) + Environment.NewLine;

            // Die einzelnen Zeilen erstellen.
            foreach (Vertex<T> v in graph.Vertices)
            {
                stringMatrix += v.ToString().CenterStringWithinLength(maxVertexStringLength) + "|";                    

                foreach(Vertex<T> w in graph.Vertices)
                {
                    if (!weights.HasValueForKeys(v,w))
                    {
                        stringMatrix += "|" + " ".CenterStringWithinLength(maxVertexStringLength);
                    }
                    else
                    {
                        stringMatrix += "|" + weights.Get(v, w).ToString().CenterStringWithinLength(maxVertexStringLength);
                    }                    
                }

                stringMatrix += Environment.NewLine;
            }

            return stringMatrix;
        }
        #endregion
    }
}
