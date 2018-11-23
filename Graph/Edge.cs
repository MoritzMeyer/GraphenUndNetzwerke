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
        #endregion

        #region ctors
        /// <summary>
        /// Erstellt eine neue Kante gewichtet und gerichtet.
        /// </summary>
        /// <param name="from">Der Knoten von dem die Kante ausgeht (wenn gerichtet).</param>
        /// <param name="to">Der Knoten in dem die Kante endet (wenn gerichtet).</param>
        /// <param name="weight">Das Gewicht der Kante (null, wenn ungewichtet)</param>
        /// <param name="isDirected">Gibt an ob die Kante gerichtet ist.</param>
        public Edge(Vertex<T> from, Vertex<T> to, int? weight, bool isDirected)
        {
            this.From = from;
            this.To = to;
            this.Weight = weight;
            this.IsDirected = isDirected;
        }

        /// <summary>
        /// Erstellt eine neue Kante gewichtet und ungerichtet
        /// </summary>
        /// <param name="from">Der Knoten von dem die Kante ausgeht (wenn gerichtet).</param>
        /// <param name="to">Der Knoten in dem die Kante endet (wenn gerichtet).</param>
        /// <param name="weight">Das Gewicht der Kante (null, wenn ungewichtet)</param>
        public Edge(Vertex<T> from, Vertex<T> to, int? weight)
            : this(from: from, to: to, weight: weight, isDirected: false)
        {
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
        /// Erzeugt eine neue Kante ohne Gewicht und ungereichtet        /// 
        /// </summary>
        /// <param name="from">Der Knoten von dem die Kante ausgeht (wenn gerichtet).</param>
        /// <param name="to">Der Knoten in dem die Kante endet (wenn gerichtet).</param>        
        public Edge(Vertex<T> from, Vertex<T> to)
            : this(from: from, to: to, weight: null, isDirected: false)
        {
        }
        #endregion
    }
}
