using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCollection
{
    public class Node
    {
        #region fields
        public string Caption { get; set; }
        public List<Node> Neighbours { get; private set; }
        public bool Visited { get; set; }
        #endregion

        #region AddNeighbour
        /// <summary>
        /// Fügt dem Knoten einen Nachbarn hinzu.
        /// </summary>
        /// <param name="node">Der hinzuzufügende Nachbar.</param>
        /// <returns>Wahr, wenn der Knoten hinzugefügt wurde, falsch wenn nicht.</returns>
        public bool AddNeighbour(Node node)
        {
            if (!this.Neighbours.Contains(node))
            {
                this.Neighbours.Add(node);
                return true;
            }

            return false;            
        }
        #endregion
    }
}
