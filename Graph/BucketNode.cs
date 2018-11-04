using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphCollection
{
    public class BucketNode : Node
    {
        #region fields
        /// <summary>
        /// Content of bucket 1
        /// </summary>
        public int Content1 { get; set; }

        /// <summary>
        /// Content of bucket 2
        /// </summary>
        public int Content2 { get; set; }

        /// <summary>
        /// Capacity of Bucket1
        /// </summary>
        public int Capacity1 { get; set; }

        /// <summary>
        /// Capacity of Bucket2
        /// </summary>
        public int Capacity2 { get; set; }
        #endregion

        #region BucketNode
        /// <summary>
        /// Creates a new Instance of this class.
        /// </summary>
        /// <param name="caption">The caption of this node.</param>
        /// <param name="content1">content of bucket 1</param>
        /// <param name="content2">content of bucket 2</param>
        public BucketNode(string caption, int capacity1, int capacity2, int content1, int content2)
            : base(caption)
        {
            if (capacity1 < content1)
            {
                throw new ArgumentException("Content of bucket 1 can't be greate than the capacity of bucket 1");
            }

            if (capacity2 < content2)
            {
                throw new ArgumentException("Content of bucket 2 can't be greate than the capacity of bucket 2");
            }

            this.Capacity1 = capacity1;
            this.Capacity2 = capacity2;
            this.Content1 = content1;
            this.Content2 = content2;
        }
        #endregion

        #region Equals
        /// <summary>
        /// Overrides the standard Equals Method for this class. The 'visited' value of this class is not considered for Equality.
        /// </summary>
        /// <param name="obj">the 'other' object to check equality with.s</param>
        /// <returns>True if this and other object are Equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                BucketNode other = (BucketNode)obj;

                return (this.Content1 == other.Content1) && (this.Content2 == other.Content2) && (this.Capacity1 == other.Capacity1) && (this.Capacity2 == other.Capacity2);

                //// If only one of the neighbors lists are null return false
                //if (((this.Neighbors == null) && (other.Neighbors != null)) || ((this.Neighbors != null) && (other.Neighbors == null)))
                //{
                //    return false;
                //}
                //else
                //{
                //    // if both neighbor lists null check caption
                //    if (this.Neighbors == null && other.Neighbors == null)
                //    {
                //        return (this.Content1 == other.Content1) && (this.Content2 == other.Content2) && (this.Capacity1 == other.Capacity1) && (this.Capacity2 == other.Capacity2);
                //    }
                //    else
                //    {
                //        // check caption and neighbors lists for equality
                //        return (this.Content1 == other.Content1) && (this.Content2 == other.Content2) && (this.Capacity1 == other.Capacity1) && (this.Capacity2 == other.Capacity2) && this.Neighbors.SequenceEqual(other.Neighbors);
                //    }
                //}
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
            return Neighbors.Count + ((this.Content1 << 2) ^ this.Content2) + ((this.Capacity1 << 2) ^ this.Capacity2);
        }
        #endregion
    }
}
