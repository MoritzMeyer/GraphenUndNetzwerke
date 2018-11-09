using System;
using System.Collections.Generic;
using System.Linq;

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
            Nodes.ForEach(n =>
            {
                string[] boolValues = new string[this.Nodes.Count];

                for (int i = 0; i < boolValues.Length; i++)
                {
                    boolValues[i] = (this.Nodes[i].Equals(n) || n.Neighbors.Contains(this.Nodes[i])).ToZeroAndOnes().CenterStringWithinLength(maxNodeStringLengt);
                }


                stringMatrix +=
                    n.Caption.CenterStringWithinLength(maxNodeStringLengt) +
                    "||" +
                    boolValues.
                        Aggregate((a, b) => a + "|" + b) +
                    Environment.NewLine;                       
                        
            });

            return stringMatrix;
        }
        #endregion
    }
}
