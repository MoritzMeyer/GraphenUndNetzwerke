using System.Collections.Generic;
using System.Linq;

namespace GraphCollection
{
    public static class GraphGenerator
    {
        #region BucketGraph
        /// <summary>
        /// Erstellt einen neuen 'BucketGraph'
        /// </summary>
        /// <param name="sizeBucket1">Die Kapazität des ersten Eimers.</param>
        /// <param name="sizeBucket2">Die Kapazität des zweiten Eimers.</param>
        /// <param name="startValue">Ein möglicher Startzustand des Graphens.</param>
        /// <returns>Den BucketGraph.</returns>
        public static Graph<TwoBuckets> BucketGraph(int sizeBucket1, int sizeBucket2, TwoBuckets startValue = null)
        {
            if (startValue == null)
            {
                startValue = new TwoBuckets(sizeBucket1, sizeBucket2);
            }

            // Initialize Graph
            Graph<TwoBuckets> graph = new Graph<TwoBuckets>();
            graph.AddVertex(startValue);

            // Generate first NeighborNodes.
            Queue<TwoBuckets> noteProcessedValues = new Queue<TwoBuckets>(GetNeighborVertices(startValue));
            
            // Die Nachbarknoten dem Graphenhinzufügen und die Kanten zum Startknoten erstellen
            foreach(TwoBuckets twoBuckets in noteProcessedValues.Distinct())
            {
                graph.AddVertex(twoBuckets);
                graph.AddEdge(startValue, twoBuckets);
            }            

            while (noteProcessedValues.Count > 0)
            {
                TwoBuckets actualValue = noteProcessedValues.Dequeue();

                // Erstelle die Liste mit möglichen Nachbarknoten zu dem aktuellen Wert.
                IEnumerable<TwoBuckets> neighborVertices = GetNeighborVertices(actualValue);

                foreach(TwoBuckets neighborValue in neighborVertices)
                {
                    // Wenn der neue Nachbar noch nicht in dem Graphen vorhanden ist, ihn diesem und der Warteschlange hinzufügen.
                    if (!graph.HasVertexWithValue(neighborValue))
                    {
                        noteProcessedValues.Enqueue(neighborValue);
                        graph.AddVertex(neighborValue);
                    }

                    // Die neue Kanten in den Graphen einfügen.
                    graph.AddEdge(actualValue, neighborValue);
                }
            }

            return graph;
        }
        #endregion

        #region GethPossibleNeighborNodes
        /// <summary>
        /// Generates a list with all possible neighbor vertices for one bucket node.
        /// </summary>
        /// <param name="root">the bucket node for which the neighbor vertices should be generated.</param>
        /// <returns>A list with possible neighbor vertices.</returns>
        public static IEnumerable<TwoBuckets> GetNeighborVertices(TwoBuckets root)
        {
            // List with possibleChildNodes.
            List<TwoBuckets> neighborNodes = new List<TwoBuckets>();
            neighborNodes.Add(root.FillB1());
            neighborNodes.Add(root.FillB2());
            neighborNodes.Add(root.EmptyB1());
            neighborNodes.Add(root.EmptyB2());
            neighborNodes.Add(root.FillB1FromB2());
            neighborNodes.Add(root.FillB2FromB1());

            return neighborNodes.Where(v => v != null);
        }
        #endregion
    }
}
