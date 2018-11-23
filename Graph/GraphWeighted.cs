using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollection
{
    public class GraphWeighted<T> : Graph<T>, IGraph<T>
    {
        public List<Edge<T>> Edges { get; set; }

    }
}
