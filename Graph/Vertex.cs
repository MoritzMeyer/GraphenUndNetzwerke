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
        /// Die Liste mit Nachbarn des Knotens.
        /// </summary>
        public virtual List<Vertex<T>> Neighbors { get; internal set; }

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
        /// Die Anzahl an Kanten, die in dem Knoten enden.
        /// </summary>
        public int InDegree { get; set; }

        /// <summary>
        /// Die Anzahl an Kanten, die von diesem Knoten ausgehen.
        /// </summary>
        public int OutDegree { get; set; }

        /// <summary>
        /// Die Rangfolge in einer Sortierung.
        /// </summary>
        public int SortOrder { get; set; }
        #endregion

        #region ctors
        /// <summary>
        /// Erzeugt eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="value">Der Wert des Knotens.</param>
        public Vertex(T value)
            : this(value, new List<Vertex<T>>())
        {
        }

        /// <summary>
        /// Erzeugt eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="value">Der Wert des Knotens.</param>
        /// <param name="neighbors"></param>
        public Vertex(T value, List<Vertex<T>> neighbors)
        {
            this.Value = value;
            this.Neighbors = neighbors;
            this.IsVisited = false;
            this.OutDegree = neighbors.Count();
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

        #region AddEdge
        /// <summary>
        /// Erstellt eine neue Kante (wenn nicht bereits vorhanden) zwischen diesem Knoten und dem newNeighbor.
        /// </summary>
        /// <param name="newNeighbor">Der Knoten zu dem die neue Kante erzeugt werden soll.</param>
        /// <returns>True, wenn die Kante erzeugt wurde, false wenn diese bereits vorhanden ist.</returns>
        public bool AddEdge(Vertex<T> newNeighbor)
        {
            // Wenn die Kante bereits vorhanden ist, nicht erneut hinzufügen und false liefern.
            if (this.Neighbors.Contains(newNeighbor))
            {
                return false;
            }

            this.Neighbors.Add(newNeighbor);
            return true;
        }
        #endregion

        #region AddEdges
        /// <summary>
        /// Erstellt neue Kanten aus einer Liste mit Vertices.
        /// </summary>
        /// <param name="newNeighbors">Die Vertices zu denen die Kanten erstellt werden sollen.</param>
        /// <returns>True, wenn die Kanten erstellt werden konnten, false wenn nicht.</returns>
        public bool AddEdges(HashSet<Vertex<T>> newNeighbors)
        {
            foreach(Vertex<T> vertex in newNeighbors)
            {
                if (this.Neighbors.Contains(vertex))
                {
                    return false;
                }
            }

            foreach(Vertex<T> vertex in newNeighbors)
            {
                this.Neighbors.Add(vertex);
            }

            return true;
        }
        #endregion

        #region RemoveEdge
        /// <summary>
        /// Entfernt eine Kante von diesem Knoten.
        /// </summary>
        /// <param name="vertex">Der Knoten wessen Kante entfernt werden soll.</param>
        /// <returns>True, wennd die Kante entfernt werden konnte, false wenn nicht.</returns>
        public bool RemoveEdge(Vertex<T> vertex)
        {
           return this.Neighbors.Remove(vertex);
        }
        #endregion

        #region HasEdgeTo
        /// <summary>
        /// Prüft, ob zwischen diesem Knoten und dem übergebenen eine Kante existiert.
        /// </summary>
        /// <param name="vertex">Der Knoten zu dem eine Kantenexistenz geprüft werden soll.</param>
        /// <returns>True, wenn zwischen diesem Knoten und dem übergebenen eine Kante existiert, false wenn nicht.</returns>
        public bool HasEdgeTo(Vertex<T> vertex)
        {
            return this.Neighbors.Contains(vertex);
        }
        #endregion

        #region CountVerticesOfSubGraph
        /// <summary>
        /// Zählt die Anzahl an Knoten in einem möglichen SubGraph, ausgehend von diesem Knoten.
        /// </summary>
        /// <returns>Die Anzahl an Knoten in dem Subgraph.</returns>
        public int CountVerticesOfSubGraph()
        {
            int sum = 1;

            if (this.Neighbors.Count == 0)
            {
                return sum;
            }
            else
            {
                foreach(Vertex<T> neighbor in this.Neighbors)
                {
                    sum += neighbor.CountVerticesOfSubGraph();
                }

                return sum;
            }
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

        #region ToCompleteString
        /// <summary>
        /// Erzeugt einen String, welcher diesen Knoten und alle seine Nachbarn ausgibt.
        /// </summary>
        /// <returns>Den String.</returns>
        public string ToCompleteString()
        {
            return this.Value.ToString() + ": " + this.Neighbors.Select(n => "-" + n.ToString()).Aggregate((a, b) => a + ", " + b);
        }
        #endregion
    }
}
