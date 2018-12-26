using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollection
{
    public class Edge<T>
    {
        #region fields
        /// <summary>
        /// Der Knoten von dem die Kante ausgeht (wenn gerichtet).
        /// </summary>
        public Vertex<T> From { get; set; }

        /// <summary>
        /// Der Knoten in dem die Kante endet (wenn gerichtet).
        /// </summary>
        public Vertex<T> To { get; set; }

        /// <summary>
        /// Das Gewicht der Kante (null wenn ungewichtet).
        /// </summary>
        public int? Weight { get; set; }

        /// <summary>
        /// Gibt an ob die Kante gerichtet ist.
        /// </summary>
        public bool IsDirected { get; set; }

        /// <summary>
        /// Gibt an, ob die Kante schon besucht wurde.
        /// </summary>
        public bool IsVisited { get; set; }

        /// <summary>
        /// Die Kapazität einer Kante in einem Flussgraphen.
        /// </summary>
        public int? Capacity { get; set; }

        /// <summary>
        /// Der Fluss über eine Kante in einem Flussgraphen.
        /// </summary>
        public int? Flow { get; set; }
        #endregion

        #region ctors
        /// <summary>
        /// Erstellt eine neue Fluss-Kante für einen Flussgraphen.
        /// </summary>
        /// <param name="from">Der ausgehende Knoten.</param>
        /// <param name="to">Der eingehende Knoten.</param>
        /// <param name="capacity">Die Kapazität der Flusskante.</param>
        /// <param name="flow">Die größes des aktuellen Flusses über diese Kante.</param>
        public Edge(Vertex<T> from, Vertex<T> to, int capacity, int flow, bool isDirected = false)
            : this(from, to)
        {
            this.Capacity = capacity;
            this.Flow = flow;
            this.Weight = null;
            this.IsDirected = isDirected;
        }

        /// <summary>
        /// Erstellt eine neue Kante gewichtet und gerichtet.
        /// </summary>
        /// <param name="from">Der Knoten von dem die Kante ausgeht (wenn gerichtet).</param>
        /// <param name="to">Der Knoten in dem die Kante endet (wenn gerichtet).</param>
        /// <param name="weight">Das Gewicht der Kante (null, wenn ungewichtet)</param>
        /// <param name="isDirected">Gibt an ob die Kante gerichtet ist.</param>
        public Edge(Vertex<T> from, Vertex<T> to, int? weight, bool isDirected = false)
        {
            this.From = from;
            this.To = to;
            this.Weight = weight;
            this.IsDirected = isDirected;
        }

        /// <summary>
        /// Erstellt eine nue Kante ungewichtet und gerichtet.
        /// </summary>
        /// <param name="from">Der Knoten von dem die Kante ausgeht (wenn gerichtet).</param>
        /// <param name="to">Der Knoten in dem die Kante endet (wenn gerichtet).</param>
        /// <param name="isDirected">Gibt an ob die Kante gerichtet ist.</param>
        public Edge(Vertex<T> from, Vertex<T> to, bool isDirected)
            : this(from: from, to: to, weight: null, isDirected: isDirected)
        {
        }

        /// <summary>
        /// Erzeugt eine neue Kante ohne Gewicht und ungereichtet
        /// </summary>
        /// <param name="from">Der Knoten von dem die Kante ausgeht (wenn gerichtet).</param>
        /// <param name="to">Der Knoten in dem die Kante endet (wenn gerichtet).</param>        
        public Edge(Vertex<T> from, Vertex<T> to)
            : this(from: from, to: to, weight: null, isDirected: false)
        {
        }

        /// <summary>
        /// Erzeugt eine neue Kante auf Basis einer bereits vorhandenen Kante.
        /// </summary>
        /// <param name="other">Die vorhandene Kante.</param>
        public Edge(Edge<T> other)
        {
            this.From = other.From;
            this.To = other.To;
            this.IsDirected = other.IsDirected;
            this.IsVisited = other.IsVisited;
            this.Weight = other.Weight;
        }
        #endregion

        #region Equals
        /// <summary>
        /// Überschreibt die Equals Methode für diese Klasse.
        /// </summary>
        /// <param name="obj">Das Objekt mit dem verglichen werden soll.</param>
        /// <returns>True, wenn das Objekt und diese Instanz der Klasse gleich sind, false wenn nicht.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !obj.GetType().Equals(this.GetType()))
            {
                return false;
            }

            Edge<T> other = (Edge<T>)obj;
            if (this.IsDirected != other.IsDirected)
            {
                return false;
            }

            if (this.IsDirected)
            {
                return this.From.Equals(other.From) && this.To.Equals(other.To) && this.Weight.Equals(other.Weight);
            }
            else
            {
                return (this.From.Equals(other.From) || this.From.Equals(other.To)) && (this.To.Equals(other.To) || this.To.Equals(other.From)) && this.Weight.Equals(other.Weight);
            }
        }
        #endregion

        #region GetHashCode
        /// <summary>
        /// Überschreibt die Berechnung des HashCodes für diese Klasse.
        /// </summary>
        /// <returns>Den HashCode.</returns>
        public override int GetHashCode()
        {
            return this.From.GetHashCode() ^ this.To.GetHashCode() ^ this.Weight.GetHashCode() ^ this.IsDirected.GetHashCode();            
        }
        #endregion

        #region ToString
        /// <summary>
        /// Überschreibt die ToString Methode.
        /// </summary>
        /// <returns>Die String repräsentation</returns>
        public override string ToString()
        {
            string connector = this.IsDirected ? "-->" : "---";

            if (this.Weight != null)
            {
                connector = connector[0] + this.Weight.ToString() + connector[2];
            }

            return $"{this.From.ToString()} {connector} {this.To.ToString()}";
        }
        #endregion
    }
}
