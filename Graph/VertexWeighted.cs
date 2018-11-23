using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollection
{
    public class VertexWeighted<T> : Vertex<T>
    {
        /// <summary>
        /// Speichert 
        /// </summary>
        public List<int> Weights { get; set; }

        public VertexWeighted(T value) 
            : base(value)
        {
            this.Weights = new List<int>();
        }

        public VertexWeighted(T value, List<Vertex<T>> neighbors)
        {

        }
    }
}
