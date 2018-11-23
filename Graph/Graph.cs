using GraphCollection.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphCollection
{
    public class Graph<T> : IGraph<T>
    {
        #region ctors
        /// <summary>
        /// Erzeugt eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="vertices">Die Liste mit Vertices, bei der jeder Vertex nur einmal vorkommen darf.</param>
        public Graph(IEnumerable<Vertex<T>> vertices)
        {
            this.Vertices = new List<Vertex<T>>();

            foreach(Vertex<T> vertex in vertices)
            {
                if(!this.AddVertex(vertex))
                {
                    this.Vertices = new List<Vertex<T>>();
                    throw new ArgumentException("Die Liste mit Vertices enthält Knoten mehrfach, dies ist nicht zulässig.");
                }
            }
        }

        /// <summary>
        /// Erzeugt eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="values">Die Liste mit Werten, die in die Knoten geschrieben werden sollen.</param>
        public Graph(IEnumerable<T> values)
            : this(values.Select(v => new Vertex<T>(v)))
        {
        }

        /// <summary>
        /// Erzeugt eine neue Instanz der Klasse.
        /// </summary>
        public Graph()
        {
            this.Vertices = new List<Vertex<T>>();
        }
        #endregion

        #region Vertices
        /// <summary>
        /// Die Liste mit Knoten des Graphen.
        /// </summary>
        public List<Vertex<T>> Vertices { get; set; }
        #endregion

        #region AddVertex
        /// <summary>
        /// Fügt dem Graphen ein Knoten hinzu.
        /// </summary>
        /// <param name="vertex">Der neue Knoten.</param>
        /// <returns>True, wenn der Knoten hinzugefügt werden konnte, false wenn nicht.</returns>
        public bool AddVertex(Vertex<T> vertex)
        {
            if (this.Vertices.Contains(vertex))
            {
                return false;
            }
            else
            {
                this.Vertices.Add(vertex);
                return true;
            }
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
            IEnumerable<Vertex<T>> verticesWithNeighbors = this.Vertices.Where(v => v.HasEdgeTo(vertex));
            foreach(Vertex<T> vertexWithNeighbor in verticesWithNeighbors)
            {
                vertexWithNeighbor.RemoveEdge(vertex);
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

        #region AddEdge
        /// <summary>
        /// Fügt in dem Graphen eine Kante zwischen zwei Knoten hinzu.
        /// </summary>
        /// <param name="valueFrom">Der Wert des Knoten von dem die Kante ausgehen soll.</param>
        /// <param name="valueTo">Der Wert des Knoten in dem die Kante enden soll.</param>
        /// <returns>True, wenn die Kante hinzugefügt werden konnte, false wenn nicht.</returns>
        public bool AddEdge(T valueFrom, T valueTo)
        {
            if (!this.HasVertexWithValue(valueFrom) || !this.HasVertexWithValue(valueTo))
            {
                return false;
            }

            Vertex<T> vertexFrom = this.Vertices.Where(v => v.Value.Equals(valueFrom)).Single();
            Vertex<T> vertexTo = this.Vertices.Where(v => v.Value.Equals(valueTo)).Single();

            return vertexFrom.AddEdge(vertexTo);
        }

        /// <summary>
        /// Fügt in dem Graphen eine Kante zwischen zwei Knoten hinzu.
        /// </summary>
        /// <param name="from">Der Knoten von dem die Kante ausgeht.</param>
        /// <param name="to">Der Knoten in dem die Kante enden soll.</param>
        /// <returns>True, wenn die Kante hinzugefügt werden konnte, false wenn nicht.</returns>
        public bool AddEdge(Vertex<T> from, Vertex<T> to)
        {
            if (!this.HasVertex(from) || !this.HasVertex(to))
            {
                return false;
            }

            this.Vertices.TryGetValue(from, out from);
            this.Vertices.TryGetValue(to, out to);

            return from.AddEdge(to);
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
                    bool isAdjacent = this.Vertices.ElementAt(i).Equals(v);
                    isAdjacent = isAdjacent || v.HasEdgeTo(this.Vertices.ElementAt(i));
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

        #region Load
        /// <summary>
        /// Lädt einen Graphen aus einer Datei.
        /// </summary>
        /// <param name="path">Der Pfad zu der Datei.</param>
        /// <returns>Der Graph.</returns>
        public static Graph<string> Load(string path)
        {
            // Die Daten aus der Datei laden.
            IEnumerable<string> lines = File.ReadLines(path);
            if (lines.Count() < 1)
            {
                throw new InvalidDataException("The given File is empty");
            }

            // Die einzelnen Zeilen der Datei auslesen.
            IEnumerator<string> linesEnumerator = lines.GetEnumerator();

            // Die Liste mit NodeCaptions
            linesEnumerator.MoveNext();
            string[] nodeCaptions = linesEnumerator.Current.Split(';');

            // Die Liste mit Kanten.
            List<string> edges = new List<string>();
            while (linesEnumerator.MoveNext())
            {
                edges.Add(linesEnumerator.Current);
            }

            // Den Graphen erstellen und die Nodes setzen.
            Graph<string> graph = new Graph<string>(nodeCaptions);

            // Die Kanten erstellen.
            foreach (string edge in edges)
            {
                string[] vertices = edge.Split(';');
                if (vertices.Count() != 2 ||
                    !graph.HasVertexWithValue(vertices[0]) ||
                    !graph.HasVertexWithValue(vertices[1]))
                {
                    throw new FormatException("Die Kanten müssen in der Form: 'V1;V2' angegeben werden. Wobei diese Kante von V1 nach V2 geht, sollte es sich um einen gerichteten Graphen handeln. V1 und V2 stellen die Werte der Vertices dar, welche in der ersten Zeile enthalten sein müssen. Es darf immer exakt eine Kante pro Zeile angegeben werden.");
                }

                if (!graph.AddEdge(vertices[0], vertices[1]))
                {
                    throw new ArgumentException("Die Kante konnte dem Graphen nicht hinzugefügt werden.");
                }
            }

            // Den Graphen liefern.
            return graph;
        }
        #endregion
    }
}
