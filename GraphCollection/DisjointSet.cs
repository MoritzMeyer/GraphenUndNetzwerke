using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollection
{
    /// <summary>
    /// A Union-Find/Disjoint-Set data structure.
    /// </summary>
    public class DisjointSet<T>
    {
        #region fields
        /// <summary>
        /// The parent of each element in the universe.
        /// </summary>
        private int[] Parent;

        /// <summary>
        /// The rank of each element in the universe.
        /// </summary>
        private int[] Rank;

        /// <summary>
        /// The size of each set.
        /// </summary>
        private int[] SizeOfSet;

        /// <summary>
        /// The number of disjoint sets.
        /// </summary>
        public int SetCount { get; private set; }
        #endregion

        #region Count
        /// <summary>
        /// The number of elements in the universe.
        /// </summary>
        public int Count { get; private set; }
        #endregion

        #region Elements
        /// <summary>
        /// The actual Elements in this universe.
        /// </summary>
        public List<Vertex<T>> Elements { get; private set; }
        #endregion

        #region ctor
        /// <summary>
        /// Initializes a new Disjoint-Set data structure, with the specified amount of elements in the universe.
        /// </summary>
        /// <param name='count'>
        /// The number of elements in the universe.
        /// </param>
        public DisjointSet(IEnumerable<Vertex<T>> elements)
        {
            this.Count = elements.Count();
            this.SetCount = elements.Count();
            this.Parent = new int[this.Count];
            this.Rank = new int[this.Count];
            this.SizeOfSet = new int[this.Count];
            this.Elements = elements.ToList();

            for (int i = 0; i < this.Count; i++)
            {
                this.Parent[i] = i;
                this.Rank[i] = 0;
                this.SizeOfSet[i] = 1;
            }
        }
        #endregion

        #region Find
        /// <summary>
        /// Find the parent of the specified element.
        /// </summary>
        /// <param name='i'>
        /// The specified element.
        /// </param>
        /// <remarks>
        /// All elements with the same parent are in the same set.
        /// </remarks>
        public int Find(Vertex<T> v)
        {
            int i = this.Elements.IndexOf(v);
            if (this.Parent[i] == i)
            {
                return i;
            }
            else
            {
                // Recursively find the real parent of i, and then cache it for later lookups.
                this.Parent[i] = this.Find(this.Elements[this.Parent[i]]);
                return this.Parent[i];
            }
        }
        #endregion

        #region Union
        /// <summary>
        /// Unite the sets that the specified elements belong to.
        /// </summary>
        /// <param name='i'>
        /// The first element.
        /// </param>
        /// <param name='j'>
        /// The second element.
        /// </param>
        public void Union(Vertex<T> u, Vertex<T> v)
        {

            // Find the representatives (or the root nodes) for the set that includes i
            int irep = this.Find(u),
                // And do the same for the set that includes j
                jrep = this.Find(v),
                // Get the rank of i's tree
                irank = this.Rank[irep],
                // Get the rank of j's tree
                jrank = this.Rank[jrep];

            // Elements are in the same set, no need to unite anything.
            if (irep == jrep)
                return;

            this.SetCount--;

            // If i's rank is less than j's rank
            if (irank < jrank)
            {
                // Then move i under j
                this.Parent[irep] = jrep;
                this.SizeOfSet[jrep] += this.SizeOfSet[irep];

            } // Else if j's rank is less than i's rank
            else if (jrank < irank)
            {

                // Then move j under i
                this.Parent[jrep] = irep;
                this.SizeOfSet[irep] += this.SizeOfSet[jrep];

            } // Else if their ranks are the same
            else
            {

                // Then move i under j (doesn't matter which one goes where)
                this.Parent[irep] = jrep;
                this.SizeOfSet[jrep] += this.SizeOfSet[irep];

                // And increment the the result tree's rank by 1
                this.Rank[jrep]++;
            }
        }
        #endregion

        #region SetSize
        /// <summary>
        /// Return the element count of the set that the specified elements belong to.
        /// </summary>
        /// <param name='i'>
        /// The element.
        /// </param>
        public int SetSize(int i)
        {
            return this.SizeOfSet[this.Find(this.Elements[i])];
        }
        #endregion
    }
}
