using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Tests")]
[assembly: InternalsVisibleTo("GraphSearch")]

namespace GraphCollection
{
    public class Node
    {
        #region fields
        /// <summary>
        /// The caption for this node.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// The neighbors of this node.
        /// </summary>
        public List<Node> Neighbors { get; private set; }

        /// <summary>
        /// A property which indicates, if this node was visited during a graph search.
        /// </summary>
        public bool Visited { get; set; }

        /// <summary>
        /// A property which indicates the search order of the nodes.
        /// </summary>
        public int? Number { get; set; }
        #endregion

        #region ctor
        /// <summary>
        /// Creates a new Instance of this class
        /// </summary>
        /// <param name="caption"></param>
        public Node(string caption)
        {
            this.Caption = caption;
            this.Neighbors = new List<Node>();
            this.Number = null;
        }
        #endregion

        #region AddNeighbor
        /// <summary>
        /// Fügt dem Knoten einen Nachbarn hinzu.
        /// </summary>
        /// <param name="node">Der hinzuzufügende Nachbar.</param>
        /// <returns>Wahr, wenn der Knoten hinzugefügt wurde, falsch wenn nicht.</returns>
        public bool AddNeighbor(Node node)
        {
            if (!this.Neighbors.Contains(node))
            {
                this.Neighbors.Add(node);
                return true;
            }

            return false;            
        }
        #endregion

        #region HasChild
        /// <summary>
        /// Checks if the given node is a child of this node instance.
        /// </summary>
        /// <param name="child">The node to check if it is a child.</param>
        /// <returns>True when the given Node is a child of this, otherwise false.</returns>
        public bool HasChild(Node child)
        {
            if (this.Neighbors.Contains(child))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region EqualsWithNeighbors
        /// <summary>
        /// Calculates Equality of two node instances while concidering the 'Neighbors' properties.
        /// </summary>
        /// <param name="obj">the 'other' object to check equality with.s</param>
        /// <returns>True if this and other object are Equal, false otherwise.</returns>
        public bool EqualsWithNeighbors(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Node other = (Node)obj;

                // If only one of the neighbors lists are null return false
                if (((this.Neighbors == null) && (other.Neighbors != null)) || ((this.Neighbors != null) && (other.Neighbors == null))) 
                {
                    return false;
                }
                else
                {
                    // if both neighbor lists null check caption
                    if (this.Neighbors == null && other.Neighbors == null)
                    {
                        return this.Caption.Equals(other.Caption);
                    }
                    else
                    {
                        // check caption and neighbors lists for equality
                        return this.Caption.Equals(other.Caption) && this.Neighbors.SequenceEqual(other.Neighbors);
                    }
                }               
            }
        }
        #endregion

        #region Equals
        /// <summary>
        /// Überschreibt die Equals-Methode für diese Klasse.
        /// </summary>
        /// <param name="obj">Das Objekt mit dem verglichen werden soll.</param>
        /// <returns>True, wenn es sich um die selben Nodes handelt, false sonst.</returns>
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Node other = (Node)obj;
                return this.Caption.Equals(other.Caption);
            }
        }
        #endregion

        #region GetHashCode
        /// <summary>
        /// Generates the HashCode for this class.
        /// </summary>
        /// <returns>The HashCode</returns>
        public override int GetHashCode()
        {
            return (this.Caption.Length << 2) ^ Neighbors.Count;
        }
        #endregion

        #region ToString
        /// <summary>
        /// Returns  a string Representation of this class.
        /// </summary>
        /// <returns>The String.</returns>
        public override string ToString()
        {
            return "(" + this.Caption + ")";
        }
        #endregion
    }
}
