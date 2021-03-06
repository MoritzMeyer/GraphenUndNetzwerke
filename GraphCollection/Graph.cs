﻿using GraphCollection.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphCollection
{
    public class Graph<T>
    {
        #region ctors
        /// <summary>
        /// Erzeugt eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="vertices">Die Liste mit Knoten.</param>
        /// <param name="edges">Die Liste mit Kanten.</param>
        /// <param name="isDirected">Gibt an, ob der Graph gerichtet sein soll.</param>
        public Graph(IEnumerable<Vertex<T>> vertices, IEnumerable<Edge<T>> edges, bool isDirected = false)
        {
            this.Vertices = new List<Vertex<T>>();
            this.Edges = edges.ToList();
            this.IsDirected = isDirected;

            foreach (Vertex<T> vertex in vertices)
            {
                if (!this.AddVertex(vertex))
                {
                    this.Vertices = new List<Vertex<T>>();
                    throw new ArgumentException("Die Liste mit Vertices enthält Knoten mehrfach, dies ist nicht zulässig.");
                }
            }
        }

        /// <summary>
        /// Erzeugt eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="vertices">Die Liste mit Vertices, bei der jeder Vertex nur einmal vorkommen darf.</param>
        /// <param name="isDirected">Gibt an, ob der Graph gerichtet sein soll.</param>
        public Graph(IEnumerable<Vertex<T>> vertices, bool isDirected = false)
            : this(vertices, new List<Edge<T>>(), isDirected: isDirected)
        {            
        }

        /// <summary>
        /// Erzeugt eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="values">Die Liste mit Werten, die in die Knoten geschrieben werden sollen.</param>
        /// <param name="isDirected">Gibt an, ob der Graph gerichtet sein soll.</param>
        public Graph(IEnumerable<T> values, bool isDirected = false)
            : this(values.Select(v => new Vertex<T>(v)), isDirected: isDirected)
        {
        }

        /// <summary>
        /// Erzeugt eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="values">Die Liste mit Werten, die in die Knoten geschrieben werden sollen.</param>
        /// <param name="edges">Die LIste mit Kanten.</param>
        /// <param name="isDirected">Gibt an, ob der Graph gerichtet sein soll.</param>
        public Graph(IEnumerable<T> values, IEnumerable<Edge<T>> edges, bool isDirected = false)
            : this(values.Select(v => new Vertex<T>(v)), isDirected: isDirected)
        {
        }

        /// <summary>
        /// Erzeugt eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="isDirected">Gibt an, ob der Graph gerichtet sein soll.</param>
        public Graph(bool isDirected = false)
        {
            this.Vertices = new List<Vertex<T>>();
            this.Edges = new List<Edge<T>>();
            this.IsDirected = isDirected;
        }
        #endregion

        #region IsDirected
        /// <summary>
        /// Gibt an, ob es sich um einen gerichteten Graphen handelt.
        /// </summary>
        public bool IsDirected { get; set; }
        #endregion

        #region IsWeighted
        /// <summary>
        /// Gibt an, ob der Graph gewichtet ist.
        /// </summary>
        public bool IsWeighted
        {
            get
            {
                return !this.Edges.Where(e => e.Weight == null).Any();
            }
            private set
            {
                this.IsWeighted = value;
            }
        }
        #endregion

        #region Vertices
        /// <summary>
        /// Die Liste mit Knoten des Graphen.
        /// </summary>
        public List<Vertex<T>> Vertices { get; set; }
        #endregion

        #region Edges
        /// <summary>
        /// Die Liste mit allen Kanten
        /// </summary>
        public List<Edge<T>> Edges { get; set; }
        #endregion

        #region AddVertex
        /// <summary>
        /// Fügt dem Graphen ein Knoten hinzu.
        /// </summary>
        /// <param name="vertex">Der neue Knoten.</param>
        /// <returns>True, wenn der Knoten hinzugefügt werden konnte, false wenn nicht.</returns>
        public bool AddVertex(Vertex<T> vertex)
        {
            // Wenn der neue Knoten bereits vorhanden, diesen nicht erneut hinzufügen.
            if (this.Vertices.Contains(vertex))
            {
                return false;
            }

            // Den neuen Knoten hinzufügen.
            this.Vertices.Add(vertex);
            return true;
        }

        /// <summary>
        /// Fügt dem Graphen einen neuen Knoten mit dem angegebenen Wert hinzu.
        /// </summary>
        /// <param name="value">Der Wert des neuen Knotens.</param>
        /// <returns>True, wenn der Knoten hinzugefügt werden konnte, false wenn nicht.</returns>
        public bool AddVertex(T value)
        {
            return this.AddVertex(new Vertex<T>(value));
        }
        #endregion

        #region GetVertex
        /// <summary>
        /// Liefert die Instanz zu einem Vertex.
        /// </summary>
        /// <param name="vertex">Der Vertex dessen Instanz gefordert ist.</param>
        /// <returns>Die Instanz des Vertex</returns>
        public Vertex<T> GetVertex(Vertex<T> vertex)
        {
            if (this.Vertices.TryGetValue(vertex, out Vertex<T> returnValue))
            {
                return returnValue;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region RemoveVertex
        /// <summary>
        /// Entfernt einen Knoten aus dem Graphen.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool RemoveVertex(Vertex<T> vertex)
        {
            // Alle Kanten von oder zu diesem Knoten löschen.
            for (int i = 0; i < this.Edges.Count(); i++)
            {
                if (this.Edges[i].From.Equals(vertex) || this.Edges[i].To.Equals(vertex))
                {
                    this.Edges.Remove(this.Edges[i]);
                }
            }

            return this.Vertices.Remove(vertex);
        }
        #endregion

        #region HasVertex
        /// <summary>
        /// Prüft, ob der Knoten im Graph vorhanden ist.
        /// </summary>
        /// <param name="vertex">Der zu prüfende Knoten.</param>
        /// <returns>True, wenn der Knoten im Graph enthalten ist, false wenn nicht.</returns>
        public bool HasVertex(Vertex<T> vertex)
        {
            return this.Vertices.Contains(vertex);
        }
        #endregion

        #region HasVertexWithValue
        /// <summary>
        /// Prüft, ob ein Vertex mit dem übergebenen Value in dem Graphen existiert.
        /// </summary>
        /// <param name="value">Der zu überprüfende Wert.</param>
        /// <returns>True, wenn der Wert in dem Graphen enthalten ist, false wenn nicht.</returns>
        public bool HasVertexWithValue(T value)
        {
            return this.Vertices.Where(v => v.Value.Equals(value)).Any();
        }
        #endregion

        #region RemoveEdge
        /// <summary>
        /// Entfernt eine Kante aus dem Graphen.
        /// </summary>
        /// <param name="edge">Die zu entfernende Kante</param>
        /// <returns>True, wenn die Kante entfernt werden konnte, false wenn nicht.</returns>
        public bool RemoveEdge(Edge<T> edge)
        {
            this.Vertices.TryGetValue(edge.From, out Vertex<T> from);
            this.Vertices.TryGetValue(edge.To, out Vertex<T> to);

            return this.Edges.Remove(edge);
        }

        /// <summary>
        /// Entfernt eine Kante aus dem Graphen.
        /// </summary>
        /// <param name="from">Der ausgehende Knoten der Kante.</param>
        /// <param name="to">Der eingehende Knoten der Kante.</param>
        /// <returns>Treu, wenn die Kante entfernt werden konnte, false wenn nicht.</returns>
        public bool RemoveEdge(Vertex<T> from , Vertex<T> to)
        {
            this.Vertices.TryGetValue(from, out from);
            this.Vertices.TryGetValue(to, out to);

            Edge<T> edge = this.Edges.Where(e => e.From.Equals(from) && e.To.Equals(to)).Single();

            return this.Edges.Remove(edge);
        }
        #endregion

        #region AddEdge
        /// <summary>
        /// Fügt dem Graphen eine Kante hinzu.
        /// </summary>
        /// <param name="valueFrom">Der Wert des Knoten von dem die Kante ausgehen soll.</param>
        /// <param name="valueTo">Der Wert des Knoten in dem die Kante enden soll.</param>
        /// <param name="weight">Das Gewicht der Kante (wenn null, dann ungewichtet).</param>
        /// <returns>True, wenn die Kante hinzugefügt werden konnte, false wenn nicht.</returns>
        public bool AddEdge(T valueFrom, T valueTo, int? weight = null)
        {
            if (!this.HasVertexWithValue(valueFrom) || !this.HasVertexWithValue(valueTo))
            {
                return false;
            }

            Vertex<T> vertexFrom = this.Vertices.Where(v => v.Value.Equals(valueFrom)).Single();
            Vertex<T> vertexTo = this.Vertices.Where(v => v.Value.Equals(valueTo)).Single();

            return this.AddEdge(vertexFrom, vertexTo, weight);
        }

        /// <summary>
        /// Fügt dem Graphen eine Kante hinzu.
        /// </summary>
        /// <param name="from">Der Knoten von dem die Kante ausgeht.</param>
        /// <param name="to">Der Knoten in dem die Kante enden soll.</param>
        /// <param name="weight"> Das Gewicht der Kante (wenn null, dann ungewichtet). </param>
        /// <returns>True, wenn die Kante hinzugefügt werden konnte, false wenn nicht.</returns>
        public bool AddEdge(Vertex<T> from, Vertex<T> to, int? weight = null)
        {
            if (!this.HasVertex(from) || !this.HasVertex(to))
            {
                return false;
            }

            this.Vertices.TryGetValue(from, out from);
            this.Vertices.TryGetValue(to, out to);

            Edge<T> edge = new Edge<T>(from, to, weight, this.IsDirected);

            if (this.Edges.Contains(edge))
            {
                return false;
            }

            this.Edges.Add(edge);
            return true;
        }
        #endregion

        #region HasEdge
        /// <summary>
        /// Prüft, ob in dem Graphen eine Kante zwischen den beiden gegebenen Knoten existiert.
        /// </summary>
        /// <param name="from">Im gerichteten Graphen die ausgehende Knoten.</param>
        /// <param name="to">Im gerichteten Graphen der eingehende Knoten.</param>
        /// <returns>True, wenn eine Kante zwischen den beiden gegebenen Knoten existiert, false wenn nicht.</returns>
        public bool HasEdge(Vertex<T> from, Vertex<T> to)
        {
            if (this.IsDirected)
            {
                return this.Edges.Where(e => e.From.Equals(from) && e.To.Equals(to)).Count() == 1;
            }
            else
            {
                return this.Edges.Where(e => (e.From.Equals(from) && e.To.Equals(to)) || (e.To.Equals(from) && e.From.Equals(to))).Count() > 0;
            }
        }
        #endregion

        #region GetEdge
        /// <summary>
        /// Gibt eine gesuchte Kante zurück.
        /// </summary>
        /// <param name="from">Der Ausgangsknoten.</param>
        /// <param name="to">Der Eingangsknoten.</param>
        /// <returns>Die gesuchte Kante.</returns>
        public Edge<T> GetEdge(Vertex<T> from, Vertex<T> to)
        {
            if (!this.HasEdge(from, to))
            {
                throw new ArgumentException($"Von dem Knoten {from.ToString()} zu dem Knoten {to.ToString()} existiert keine Kante.");
            }

            if (this.IsDirected)
            {
                return this.Edges.Where(e => e.From.Equals(from) && e.To.Equals(to)).Single();
            }
            else
            {
                return this.Edges.Where(e => (e.From.Equals(from) && e.To.Equals(to)) || (e.To.Equals(from) && e.From.Equals(to))).ElementAt(0);
            }
        }
        #endregion

        #region GetNeighbours
        /// <summary>
        /// Ermittelt für einen Knoten aus dem Graphen alle seine Nachbarn.
        /// </summary>
        /// <param name="vertex">Der Knoten für den die Nachbarn ermittelt werden sollen.</param>
        /// <param name="isDirected">Für ungerichtete Graphen kann hiermit angegeben werden, dass die 
        /// Rückwärtigenkanten nicht als Nachbarkanten gezählt werden.</param>
        /// <returns>Die Liste mit Nachbarn.</returns>
        public List<Vertex<T>> GetNeighbours(Vertex<T> vertex, bool isDirected = false)
        {
            // Es kann sinnvoll sein auch für ungerichtete Graphen die Suche nach Nachbarn nur in eine Richtung zu suchen.
            isDirected = this.IsDirected || isDirected;

            // von allen Kanten bei denen 'vertex' der ausgehende Knoten ist die eingehenden Knoten (die Nachbarn) liefern.
            List<Vertex<T>> neighbours = this.Edges
                .Where(e => e.From.Equals(vertex))
                .Select(e => e.To)
                .ToList();

            // Wenn der Graph ungerichtet ist auch die 'rückwärtigen' Kanten berücksichtigen.
            if (!isDirected)
            {
                neighbours.AddRange(this.Edges
                    .Where(e => e.To.Equals(vertex))
                    .Select(e => e.From));
            }

            // Das Ergebnis lierfern.
            return neighbours;            
        }
        #endregion

        #region HasNeighbour
        /// <summary>
        /// Prüft ob ein Knoten einen anderen Knoten als Nachbarn hat.
        /// </summary>
        /// <param name="vertex">Der Knoten.</param>
        /// <param name="neighbour">Der zu prüfende Nachbar des Knotens.</param>
        /// <returns>True, wenn 'neighbour' ein Nachbar von 'vertex' ist.</returns>
        public bool HasNeighbour(Vertex<T> vertex, Vertex<T> neighbour)
        {
            return this.GetNeighbours(vertex).Contains(neighbour);
        }
        #endregion

        #region CountVerticesOfSubgraph
        /// <summary>
        /// Berechnet die Anzahl an Knoten eines Subgraphen ausgehenden von dem übergebenen Vertex.
        /// </summary>
        /// <param name="vertex">Der Knoten von dem aus die größe des Subgraphen berechnet werden soll.</param>
        /// <returns>Die Anzahl an Knoten in dem Subgraphen.</returns>
        public int CountVerticesOfSubgraph(Vertex<T> vertex)
        {
            int sum = 1;

            List<Vertex<T>> neighbours = this.GetNeighbours(vertex, true);
            if (neighbours.Count() == 0)
            {
                return sum;
            }
            else
            {
                foreach(Vertex<T> neighbour in neighbours)
                {
                    sum += this.CountVerticesOfSubgraph(neighbour);
                }

                return sum;
            }
        }
        #endregion

        #region VerticesInSortOrder
        /// <summary>
        /// Liefert die Liste mit Vertices in sortierter Reihenfolge (sortiert nach ihrer Eigenschaft 'SortOrder')
        /// </summary>
        /// <returns>Die sortierte Liste.</returns>
        public SortedList<int, Vertex<T>> VerticesInSortOrder()
        {
            SortedList<int, Vertex<T>> sortedVertices = new SortedList<int, Vertex<T>>();

            foreach(Vertex<T> vertex in this.Vertices)
            {
                sortedVertices.Add(vertex.SortOrder, vertex);
            }

            return sortedVertices;
        }
        #endregion

        #region ResetVisitedProperty
        /// <summary>
        /// Setzt die 'isBesucht' Eigenschaft der Knoten des Graphen zurück.
        /// </summary>
        public void ResetVisitedProperty()
        {
            foreach(Vertex<T> vertex in this.Vertices)
            {
                vertex.IsVisited = false;
            }
        }
        #endregion

        #region GetAdjacencyMatrix
        /// <summary>
        /// Gibt die AdjazenzMatrix des Graphens zurück.
        /// </summary>
        /// <returns>Die Adjazenzmatrix.</returns>
        public string GetAdjacencyMatrix()
        {
            // Die maximale Länge einer Node-Repräsentation ermitteln.
            int maxVertexStringLength = this.Vertices.Select(v => v.ToString()).Max(v => v.Length);
            string stringMatrix = string.Empty;

            // Die Titelleiste erstellen.
            stringMatrix +=
                string.Concat(Enumerable.Repeat(" ", maxVertexStringLength)) +
                "||" +
                this.Vertices
                    .Select(v => v.ToString())
                    .Aggregate((a, b) => a.CenterStringWithinLength(maxVertexStringLength) + "|" + b.CenterStringWithinLength(maxVertexStringLength)) +
                Environment.NewLine;

            stringMatrix += string.Concat(Enumerable.Repeat("=", stringMatrix.Length)) + Environment.NewLine;

            // Die einzelnen Zeilen erstellen.
            foreach(Vertex<T> v in this.Vertices)
            {
                string[] boolValues = new string[this.Vertices.Count];

                for (int i = 0; i < boolValues.Length; i++)
                {
                    bool isAdjacent = this.Vertices[i].Equals(v);
                    isAdjacent = isAdjacent || this.Edges.Where(e => e.From.Equals(v) && e.To.Equals(this.Vertices[i])).Any();
                    if (!this.IsDirected)
                    {
                        isAdjacent = isAdjacent || this.Edges.Where(e => e.To.Equals(v) && e.From.Equals(this.Vertices[i])).Any();
                    }
                    boolValues[i] = isAdjacent.ToZeroAndOnes().CenterStringWithinLength(maxVertexStringLength);
                }

                stringMatrix +=
                    v.ToString().CenterStringWithinLength(maxVertexStringLength) +
                    "||" +
                    boolValues.
                        Aggregate((a, b) => a + "|" + b) +
                    Environment.NewLine;
            }

            return stringMatrix;
        }
        #endregion

        #region Copy
        /// <summary>
        /// Erstellt eine Kopie des Graphens.
        /// </summary>
        /// <returns></returns>
        public Graph<T> Copy()
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

            Graph<T> copy = new Graph<T>(vertices, edges, this.IsDirected);

            return copy;
        }
        #endregion
    }
}
