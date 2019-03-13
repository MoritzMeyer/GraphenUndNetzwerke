using System.Collections.Generic;
using System.Linq;

namespace GraphCollection
{
    /// <summary>
    /// Standardklasse für einen Knoten in einem Graphen.
    /// </summary>
    /// <typeparam name="T">Der Datentyp des Wertes, welcher den Knoten repräsentiert.</typeparam>
    public class Vertex<T>
    {
        #region fields
        /// <summary>
        /// Der Wert des Knotens.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Kennzeichnet, ob dieser Knoten bereits besucht wurde.
        /// </summary>
        public bool IsVisited { get; set; }

        /// <summary>
        /// Die Nummer des Knotens.
        /// </summary>
        public int? Number { get; set; }

        /// <summary>
        /// Die Anzahl an eingehenden Kanten.
        /// </summary>
        public int? InDegree { get; set; }

        /// <summary>
        /// Die Rangfolge in einer Sortierung.
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Der Vorgänger welcher durch den Dijkstra Alogrithmus berechnet wird.
        /// </summary>
        public Vertex<T> DijkstraAncestor { get; set; }

        /// <summary>
        /// Die Entfernung, welche durch den Dijkstra Algorithmus berechnet wird.
        /// </summary>
        public int? DijkstraDistance { get; set; }
        #endregion

        #region ctors
        /// <summary>
        /// Erzeugt eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="value">Der Wert des Knotens.</param>
        /// <param name="neighbors"></param>
        public Vertex(T value)
        {
            this.Value = value;
            this.IsVisited = false;
        }
        #endregion

        #region Visit
        /// <summary>
        /// Setzt die IsVisited Eigenschaft auf true.
        /// </summary>
        public void Visit()
        {
            this.IsVisited = true;
        }
        #endregion

        #region Equlas
        /// <summary>
        /// Implementierung der Equals-Methode.
        /// </summary>
        /// <param name="obj">Das zu vergleichende Objekt.</param>
        /// <returns>True, wenn die Objekte gleich sind, false wenn nicht.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !obj.GetType().Equals(this.GetType()))
            {
                return false;
            }

            Vertex<T> other = (Vertex<T>)obj;
            return this.Value.Equals(other.Value);
        }
        #endregion

        #region GetHashCode
        /// <summary>
        /// Überschreibt die GetHashCode Methode.
        /// </summary>
        /// <returns>Den HashCode des Values.</returns>
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
        #endregion

        #region ToString
        /// <summary>
        /// Überschreibt die ToString() Methode.
        /// </summary>
        /// <returns>Der String.</returns>
        public override string ToString()
        {
            return this.Value.ToString();
        }
        #endregion
    }
}
