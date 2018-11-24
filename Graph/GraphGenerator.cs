using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphCollection
{
    public static class GraphGenerator
    {
        #region LoadFromFile
        /// <summary>
        /// Lädt einen Graphen aus einer Datei
        /// </summary>
        /// <param name="path">Der Pfad zur Datei.</param>
        /// <returns>Den Graphen.</returns>
        public static Graph<string> LoadFromFile(string path, bool isDirected = false)
        {
            // Die Daten aus der Datei laden.
            IEnumerable<string> lines = File.ReadLines(path);
            if (lines.Count() < 1)
            {
                throw new InvalidDataException("The given File is empty");
            }

            // Die einzelnen Zeilen der Datei auslesen.
            IEnumerator<string> linesEnumerator = lines.GetEnumerator();

            // Die Liste mit NodeCaptions
            linesEnumerator.MoveNext();
            string[] nodeCaptions = linesEnumerator.Current.Split(';');

            // Die Liste mit Kanten.
            List<string> stringEdges = new List<string>();
            while (linesEnumerator.MoveNext())
            {
                stringEdges.Add(linesEnumerator.Current);
            }

            // Den Graphen erstellen und die Nodes setzen.
            Graph<string> graph = new Graph<string>(nodeCaptions, isDirected);

            // Die Kanten erstellen.
            foreach (string stringEdge in stringEdges)
            {
                string[] vertices = stringEdge.Split(';');
                if (vertices.Count() != 2 ||
                    !graph.HasVertexWithValue(vertices[0]) ||
                    !graph.HasVertexWithValue(vertices[1]))
                {
                    throw new FormatException("Die Kanten müssen in der Form: 'V1;V2' angegeben werden. Wobei diese Kante von V1 nach V2 geht, sollte es sich um einen gerichteten Graphen handeln. V1 und V2 stellen die Werte der Vertices dar, welche in der ersten Zeile enthalten sein müssen. Es darf immer exakt eine Kante pro Zeile angegeben werden.");
                }

                if (!graph.AddEdge(vertices[0], vertices[1]))
                {
                    throw new ArgumentException("Die Kante konnte dem Graphen nicht hinzugefügt werden.");
                }
            }

            // Den Graphen liefern.
            return graph;

        }
        #endregion

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
            Graph<TwoBuckets> graph = new Graph<TwoBuckets>(true);
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
