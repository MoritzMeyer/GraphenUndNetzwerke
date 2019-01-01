using GraphCollection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollection
{
    public class FlowGraph<T> : Graph<T>
    {
        #region fields
        /// <summary>
        /// Die Quelle des Flusses.
        /// </summary>
        public Vertex<T>  Source { get; set; }

        /// <summary>
        /// Die Senke des Flusses.
        /// </summary>
        public Vertex<T> Target { get; set; }        
        #endregion

        #region ctors
        /// <summary>
        /// Erstellt eine neue Instanz eines Flussgraphen.
        /// </summary>
        /// <param name="source">Die Quelle.</param>
        /// <param name="target">Die Senke.</param>
        /// <param name="vertices">Die Knoten des Graphen.</param>
        /// <param name="edges">Die Kanten des Grapen.</param>
        /// <param name="isDirected">Gibt an, ob der Flussgraph gerichtet ist.</param>
        public FlowGraph(Vertex<T> source, Vertex<T> target, IEnumerable<Vertex<T>> vertices, IEnumerable<Edge<T>> edges = null, bool isDirected = false)
        {
            if (!vertices.Contains(source))
            {
                throw new ArgumentException("Die Quelle muss Teil des Graphen sein.");
            }

            if (!vertices.Contains(target))
            {
                throw new ArgumentException("Die Senke muss Teil des Graphen sein.");
            }

            this.Source = source;
            this.Target = target;
            this.Vertices = vertices.ToList();
            this.IsDirected = isDirected;

            // Die Kanten setzen wenn vorhanden.
            if (edges == null)
            {
                this.Edges = new List<Edge<T>>();
            }
            else
            {
                this.Edges = edges.ToList();
            }

            if (this.Edges.Where(e => !e.Capacity.HasValue).Any())
            {
                throw new ArgumentException("Die Kanten eines Flussgraphen muss eine Kapazität definieren.");
            }

            if (this.Edges.Where(e => !this.HasVertex(e.From) || !this.HasVertex(e.To)).Any())
            {
                throw new ArgumentException("Die Aus- und Eingangskonten der Kanten müssen im Graphen enthalten sein.");
            }            

            // Sicherstellen, dass alle Kantenflüsse initialisiert sind.
            foreach (Edge<T> e in this.Edges.Where(e => !e.Flow.HasValue))
            {
                e.Flow = 0;
            }            
        }

        /// <summary>
        /// Erstellt einen neuen Flussgraphen.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="verticeValues"></param>
        /// <param name="isDirected"></param>
        public FlowGraph(T source, T target, IEnumerable<T> verticeValues, bool isDirected = false)
        {
            if (!verticeValues.Contains(source))
            {
                throw new ArgumentException("Die Quelle muss Teil des Graphens sein.");
            }

            if (!verticeValues.Contains(target))
            {
                throw new ArgumentException("Die Senke muss Teil des Graphens sein.");

            }

            this.Vertices = verticeValues.Select(v => new Vertex<T>(v)).ToList();
            this.Source = this.Vertices.Where(v => v.Value.Equals(source)).Single();
            this.Target = this.Vertices.Where(v => v.Value.Equals(target)).Single();
            this.IsDirected = isDirected;
        }

        /// <summary>
        /// Erzeugt einen neuen Flussgraphen.
        /// </summary>
        /// <param name="source">Die Quelle.</param>
        /// <param name="target">Die Senke.</param>
        /// <param name="isDirected">Gibt an, ob der Flussgraph gerichtet ist.</param>
        public FlowGraph(Vertex<T> source, Vertex<T> target, bool isDirected = false)
        {
            this.Source = source;
            this.Target = target;
            this.IsDirected = isDirected;

            this.Vertices.Add(source);
            this.Vertices.Add(target);
        }
        #endregion

        #region AddEdge
        /// <summary>
        /// Fügt dem Graphen eine Flusskante hinzu.
        /// </summary>
        /// <param name="valueFrom">Der Wert des ausgehenden Knotens.</param>
        /// <param name="valueTo">Der Wert des eingehenden Knotens.</param>
        /// <param name="capacity">Die Kapazität der Flusskante (wenn es sich um einen Flussgraphen handelt).</param>
        /// <param name="flow">Die Höhe des Flusses über diese Kante (wenn es sich um einen Flussgraphen handelt).</param>
        /// <returns></returns>
        public bool AddEdge(T valueFrom, T valueTo, int capacity, int flow)
        {
            if (!this.HasVertexWithValue(valueFrom) || !this.HasVertexWithValue(valueTo))
            {
                return false;
            }

            Vertex<T> vertexFrom = this.Vertices.Where(v => v.Value.Equals(valueFrom)).Single();
            Vertex<T> vertexTo = this.Vertices.Where(v => v.Value.Equals(valueTo)).Single();

            return this.AddEdge(vertexFrom, vertexTo, capacity, flow);
        }

        /// <summary>
        /// Fügt dem Graphen eine Flusskante hinzu.
        /// </summary>
        /// <param name="from">Der ausgehende Knoten.</param>
        /// <param name="to">Der eingehende Knoten.</param>
        /// <param name="capacity">Die Kapazität der Flusskante (wenn es sich um einen Flussgraphen handelt).</param>
        /// <param name="flow">Die Höhe des Flusses über diese Kante (wenn es sich um einen Flussgraphen handelt).</param>
        /// <returns></returns>
        public bool AddEdge(Vertex<T> from, Vertex<T> to, int capacity, int flow)
        {
            if (!this.HasVertex(from) || !this.HasVertex(to))
            {
                return false;
            }

            this.Vertices.TryGetValue(from, out from);
            this.Vertices.TryGetValue(to, out to);

            Edge<T> edge = new Edge<T>(from, to, capacity, flow, isDirected: this.IsDirected);

            if (this.Edges.Contains(edge))
            {
                return false;
            }

            this.Edges.Add(edge);
            return true;
        }
        #endregion

        #region Copy
        /// <summary>
        /// Erstellt eine Kopie des Graphens.
        /// </summary>
        /// <returns></returns>
        public new FlowGraph<T> Copy()
        {
            List<Vertex<T>> vertices = new List<Vertex<T>>();
            this.Vertices.ForEach(v =>
            {
                vertices.Add(new Vertex<T>(v.Value)
                {
                    IsVisited = v.IsVisited,
                    DijkstraDistance = v.DijkstraDistance,
                    DijkstraAncestor = v.DijkstraAncestor,
                    Number = v.Number,
                    SortOrder = v.SortOrder
                });
            });

            List<Edge<T>> edges = new List<Edge<T>>();
            this.Edges.ForEach(e =>
            {
                // die Referenzen der Knoten holen.
                Vertex<T> from = vertices.Where(v => v.Equals(e.From)).Single();
                Vertex<T> to = vertices.Where(v => v.Equals(e.To)).Single();

                edges.Add(new Edge<T>(from, to)
                {
                    Capacity = e.Capacity,
                    Flow = e.Flow,
                    IsDirected = e.IsDirected,
                    Weight = e.Weight,
                    IsVisited = e.IsVisited
                });
            });

            Vertex<T> source = vertices.Where(v => v.Equals(this.Source)).Single();
            Vertex<T> target = vertices.Where(v => v.Equals(this.Target)).Single();

            FlowGraph<T> copy = new FlowGraph<T>(source, target, vertices, edges, this.IsDirected);

            return copy;
        }
        #endregion
    }
}
