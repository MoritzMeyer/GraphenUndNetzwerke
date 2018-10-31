using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphCollection
{
    public class Graph
    {
        #region fields
        /// <summary>
        /// Die Liste mit Knoten des Graphen.
        /// </summary>
        public List<Node> Nodes { get; private set; }
        #endregion

        #region AddNode
        /// <summary>
        /// Fügt einen Knoten hinzu.
        /// </summary>
        /// <param name="node">Der Knoten</param>
        public bool AddNode(Node node)
        {
            if (!this.Nodes.Contains(node))
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
        public bool AddNodes(IEnumerable<Node> nodes)
        {
            foreach(Node node in nodes)
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

        #region ResetVisitedProperty
        /// <summary>
        /// Setzt die 'Besucht' Eigenschaft der Knoten zuürck.
        /// </summary>
        public void ResetVisitedProperty()
        {
            this.Nodes.ForEach(n => n.Visited = false);
        }
        #endregion
    }
}
