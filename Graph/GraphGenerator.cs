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
        public static Graph<string> LoadFromFile(string path, bool isDirected = false, bool isWeighted = false)
        {
            // Die Daten aus der Datei laden.
            IEnumerable<string> lines = File.ReadLines(path);
            if (lines.Count() < 1)
            {
                throw new InvalidDataException("The given File is empty");
            }

            // Die einzelnen Zeilen der Datei auslesen.
            IEnumerator<string> linesEnumerator = lines.GetEnumerator();

            // Die erste Zeile enthält die Anzahl an Knoten im Graphen
            linesEnumerator.MoveNext();
            string stringSize = linesEnumerator.Current;

            // Den Graphen erstellen
            Graph<string> graph = new Graph<string>(isDirected: isDirected);

            // Die Liste mit Kanten, Knoten und Kantengewichten auslesen.            
            int lineNumber = 2;
            while (linesEnumerator.MoveNext())
            {
                string[] edgeData = linesEnumerator.Current.Split(' ');
                if ((isWeighted && edgeData.Count() != 3) ||
                    (!isWeighted && edgeData.Count() != 2))
                {
                    throw new ArgumentException($"Die Anzahl an Argumenten für die Kante in Zeile {lineNumber} genügt nicht den Anforderungen.");
                }

                // Wenn der ausgehende Knoten nicht vorhanden ist, diesen hinzufügen.
                if (!graph.HasVertexWithValue(edgeData.First()))
                {
                    graph.AddVertex(edgeData.First());
                }

                // Wenn der eingehende Knoten nicht vorhanden ist, diesen hinzufügen.
                if (!graph.HasVertexWithValue(edgeData.Last()))
                {
                    graph.AddVertex(edgeData.Last());
                }

                // Die Kante dem Graphen hinzufügen.
                if (isWeighted)
                {
                    if (!graph.AddEdge(edgeData.First(), edgeData.Last(), weight: Convert.ToInt32(edgeData[1])))
                    {
                        throw new ArgumentException("Die Kante konnte dem Graphen nicht hinzugefügt werden.");
                    }
                }
                else
                {
                    if(!graph.AddEdge(edgeData.First(), edgeData.Last()))
                    {
                        throw new ArgumentException("Die Kante konnte dem Graphen nicht hinzugefügt werden.");
                    }
                }

                lineNumber++;
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
