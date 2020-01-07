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
            //char[] invalid = System.IO.Path.GetInvalidPathChars();

            // Die Daten aus der Datei laden.
            IEnumerable<string> lines = File.ReadLines(path);
            if (lines.Count() < 1)
            {
                throw new InvalidDataException("The given File is empty");
            }

            // Variablen initialisieren
            bool isFlowGraph = false;
            bool isWeighted = false;
            int numberOfVertices = 0;
            Vertex<string> sSource = default(Vertex<string>);
            Vertex<string> tSink = default(Vertex<string>);

            // Die einzelnen Zeilen der Datei auslesen.
            IEnumerator<string> linesEnumerator = lines.GetEnumerator();

            // Die erste Zeile enthält die Anzahl an Knoten im Graphen
            linesEnumerator.MoveNext();

            // Handelt es sich um einen Flussgraphen sind in der ersten Zeile neben der Anzahl auch die Quelle und die Senke angegeben
            string[] lineData = linesEnumerator.Current.Split(' ');
            switch(lineData.Count())
            {
                case 3:
                    isFlowGraph = true;
                    numberOfVertices = Convert.ToInt32(lineData[0]);
                    sSource = new Vertex<string>(lineData[1]);
                    tSink = new Vertex<string>(lineData[2]);
                    break;
                case 1:
                    numberOfVertices = Convert.ToInt32(lineData[0]);
                    break;
                default:
                    throw new ArgumentException("Unkown input Format in line 1");
            }

            // Die Anzahl an Einträgen in der zweiten Zeile bestimmt, ob es sich um einen un/gewichteten oder Flussgraphen handelt.
            linesEnumerator.MoveNext();
            lineData = linesEnumerator.Current.Split(' ');
            
            // Flussgraphen Anforderung überprüfen
            if (isFlowGraph && lineData.Count() != 4)
            {
                throw new ArgumentException("Line 1 indicates a Flow-Graph, but line 2 number of arguments doesn't match the requirements for Flow-Graphs (must be 4)");
            }

            if (lineData.Count() == 3)
            {
                isWeighted = true;
            }

            // Den Enumerator wieder auf die erste Zeile setzen
            linesEnumerator = lines.GetEnumerator();
            linesEnumerator.MoveNext();

            // Den Graphen erstellen
            Graph<string> graph = (isFlowGraph) ? new FlowGraph<string>(sSource, tSink, new List<Vertex<string>>() { sSource, tSink }) : new Graph<string>(isDirected: isDirected);

            // Die Liste mit Kanten, Knoten und Kantengewichten auslesen.            
            int lineNumber = 2;
            while (linesEnumerator.MoveNext())
            {
                string[] edgeData = linesEnumerator.Current.Split(' ');
                if ((isFlowGraph && edgeData.Count() != 4) ||
                     (!isFlowGraph && edgeData.Count() > 3) ||
                     (!isFlowGraph && edgeData.Count() < 2))
                {
                    throw new ArgumentException($"Number of Arguments in line {lineNumber} doesn't macht the requirements");
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
                else if (isFlowGraph)
                {
                    if (!((FlowGraph<string>)graph).AddEdge(edgeData.First(), edgeData.Last(), Convert.ToInt32(edgeData[1]), Convert.ToInt32(edgeData[2])))
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

        public static Graph<string> LoadXmasCodingGraph(string path)
        {
            // Die Daten aus der Datei laden.
            IEnumerable<string> lines = File.ReadLines(path);
            if (lines.Count() < 1)
            {
                throw new InvalidDataException("The given File is empty");
            }

            // Variablen initialisieren
            bool isFlowGraph = false;
            bool isWeighted = true;
            int numberOfVertices = 0;
            Vertex<string> sSource = default(Vertex<string>);
            Vertex<string> tSink = default(Vertex<string>);

            // Die einzelnen Zeilen der Datei auslesen.
            IEnumerator<string> linesEnumerator = lines.GetEnumerator();

            // Die erste Zeile enthält die Anzahl an Knoten im Graphen
            linesEnumerator.MoveNext();
            string[] lineData = linesEnumerator.Current.Split(' ');
            numberOfVertices = Convert.ToInt32(lineData[0]);

            // Den Enumerator wieder auf die erste Zeile setzen
            linesEnumerator = lines.GetEnumerator();
            linesEnumerator.MoveNext();

            Graph<string> graph = new Graph<string>(true);

            // Die Liste mit Kanten, Knoten und Kantengewichten auslesen.            
            int lineNumber = 2;

            while (linesEnumerator.MoveNext())
            {
                string[] edgeData = linesEnumerator.Current.Split(' ');
                if ((isFlowGraph && edgeData.Count() != 4) ||
                     (!isFlowGraph && edgeData.Count() > 3) ||
                     (!isFlowGraph && edgeData.Count() < 2))
                {
                    throw new ArgumentException($"Number of Arguments in line {lineNumber} doesn't macht the requirements");
                }

                // Wenn der ausgehende Knoten nicht vorhanden ist, diesen hinzufügen.
                if (!graph.HasVertexWithValue(edgeData.First()))
                {
                    graph.AddVertex(edgeData.First());
                }

                // Wenn der eingehende Knoten nicht vorhanden ist, diesen hinzufügen.
                if (!graph.HasVertexWithValue(edgeData[1]))
                {
                    graph.AddVertex(edgeData[1]);
                }

                if (!graph.AddEdge(edgeData.First(), edgeData[1], weight: Convert.ToInt32(edgeData.Last())))
                {
                    throw new ArgumentException("Die Kante konnte dem Graphen nicht hinzugefügt werden.");
                }

                lineNumber++;
            }

            return graph;
        }
    }
}
