using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
[assembly: InternalsVisibleTo("GraphSearch")]

namespace GraphCollection
{
    public class Graph<T> where T : Node
    {
        #region fields
        /// <summary>
        /// Die Liste mit Knoten des Graphen.
        /// </summary>s
        public List<T> Nodes { get; private set; }

        /// <summary>
        /// The root node of this graph.
        /// </summary>
        public T Root { get; private set; }
        #endregion

        #region ctors
        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        /// <param name="root">The root node for this graph.</param>
        public Graph(T root)
        {
            this.Root = root;
            this.Nodes = new List<T>() { root };
        }
        #endregion

        #region AddNode
        /// <summary>
        /// Fügt einen Knoten hinzu.
        /// </summary>
        /// <param name="node">Der Knoten</param>
        public bool AddNode(T node)
        {
            if (!this.Contains(node))
            {
                this.Nodes.Add(node);
                return true;
            }

            return false;            
        }
        #endregion

        #region AddNodes
        /// <summary>
        /// Fügt eine Anzahl von Knoten hinzu.
        /// </summary>
        /// <param name="nodes">Die Enumeration von Knoten.</param>
        public bool AddNodes(IEnumerable<T> nodes)
        {
            foreach(T node in nodes)
            {
                if (this.Nodes.Contains(node))
                {
                    return false;
                }
            }

            nodes.ToList().ForEach(n => this.AddNode(n));
            return true;            
        }
        #endregion

        #region Contains
        /// <summary>
        /// Checks if the graph contains a given node.
        /// </summary>
        /// <param name="node">the node to check if it's part of the graph.</param>
        /// <returns>True if the node is part of the graph, false otherwise.</returns>
        public bool Contains(T node)
        {
            return this.Nodes.Contains(node);
        }
        #endregion

        #region GetNodeInstance
        /// <summary>
        /// Returns the instance of a node within this graph.
        /// </summary>
        /// <param name="node">An equal node for which the graph-instance is needed.</param>
        /// <returns>the node instance.</returns>
        public T GetNodeInstance(T node)
        {
            if (this.Contains(node))
            {
                return this.Nodes.Where(n => n.Equals(node)).Single();
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region ResetVisitedProperty
        /// <summary>
        /// Setzt die 'Besucht' Eigenschaft der Knoten zuürck.
        /// </summary>
        public void ResetVisitedProperty()
        {
            this.Nodes.ForEach(n => n.Visited = false);
        }
        #endregion

        #region GetAdjacencyMatrix
        /// <summary>
        /// Gibt die AdjazenzMatrix eines Graphen zurück.s
        /// </summary>
        /// <returns>Die AdjazenzMatrix als String.</returns>
        public string GetAdjacencyMatrix()
        {
            // Die maximale Länge einr Node-Caption ermitteln.
            int maxNodeStringLengt = this.Nodes.Select(n => n.Caption).Max(n => n.Length);
            string stringMatrix = string.Empty;

            // Die Titelleiste erstellen.
            stringMatrix +=
                string.Concat(Enumerable.Repeat(" ", maxNodeStringLengt)) +
                "||" +
                this.Nodes
                    .Select(n => n.Caption)
                    .Aggregate((a, b) => a.CenterStringWithinLength(maxNodeStringLengt) + "|" + b.CenterStringWithinLength(maxNodeStringLengt)) +
                Environment.NewLine;

            stringMatrix += string.Concat(Enumerable.Repeat("=", stringMatrix.Length)) + Environment.NewLine;

            // Die einzelnen Zeilen erstellen.
            foreach(Node n in this.Nodes)
            {
                string[] boolValues = new string[this.Nodes.Count];

                for (int i = 0; i < boolValues.Length; i++)
                {
                    bool isAdjacent = this.Nodes[i].Caption.Equals(n.Caption);
                    isAdjacent = isAdjacent || n.Neighbors.Where(nb => nb.Caption.Equals(this.Nodes[i].Caption)).Count() == 1;
                    boolValues[i] = isAdjacent.ToZeroAndOnes().CenterStringWithinLength(maxNodeStringLengt);
                }


                stringMatrix +=
                    n.Caption.CenterStringWithinLength(maxNodeStringLengt) +
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
        public static Graph<Node> Load(string path)
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
            while(linesEnumerator.MoveNext())
            {
                edges.Add(linesEnumerator.Current);
            }

            // Den Graphen erstellen und die Nodes setzen.
            Graph<Node> graph = new Graph<Node>(new Node(nodeCaptions[0]));
            for(int i = 1; i < nodeCaptions.Count(); i++)
            {
                if (!graph.AddNode(new Node(nodeCaptions[i])))
                {
                    throw new ArgumentException("Die Bezeichnung der einzelnen Knoten muss einmalig sein.");
                }
            }

            // Die Kanten erstellen.
            foreach(string edge in edges)
            {
                string[] vertices = edge.Split(';');
                if (vertices.Count() != 2 || 
                    graph.Nodes.Where(n => n.Caption.Equals(vertices[0])).Count() != 1 || 
                    graph.Nodes.Where(n => n.Caption.Equals(vertices[1])).Count() != 1)
                {
                    throw new FormatException("Die Kanten müssen in der Form: 'V1;V2' angegeben werden. Die Beispiel Kange geht von V1 nach V2. V1 und V2 stellen hierbei die Caption der Vertices dar. Es darf immer exakt eine Kante pro Zeile angegeben werden.");
                }

                graph.Nodes
                    .Where(n => n.Caption.Equals(vertices[0])).Single()
                    .AddNeighbor(
                        graph.Nodes
                            .Where(n => n.Caption.Equals(vertices[1]))
                        .Single());
            }
          
            // Den Graphen liefern.
            return graph;
        }
        #endregion
    }
}
