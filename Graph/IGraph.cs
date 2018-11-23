using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollection
{
    public interface IGraph<T>
    {
        bool AddVertex(Vertex<T> vertex);
        Vertex<T> GetVertex(Vertex<T> vertex);
        bool RemoveVertex(Vertex<T> vertex);
        bool HasVertex(Vertex<T> vertex);
        bool HasVertexWithValue(T value);
        bool AddEdge(T valueFrom, T valueTo);
        bool AddEdge(Vertex<T> from, Vertex<T> to);
        SortedList<int, Vertex<T>> VerticesInSortOrder();
        void ResetVisitedProperty();
        string GetAdjacencyMatrix();
    }
}
