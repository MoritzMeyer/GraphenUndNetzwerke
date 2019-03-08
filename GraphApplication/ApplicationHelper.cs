using GraphCollection;
using GraphCollection.SearchAlgorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GraphApplication
{
    public static class ApplicationHelper
    {
        /// <summary>
        /// Lädt einen Graphen aus einer Datei und führt auf diesem den Dijkstra-Algorithmus mit den gegebenen Parametern aus.
        /// </summary>
        /// <param name="args"></param>
        public static void CallDijkstra(string fileName, string valueStart, string valueEnd = null)
        {
            // Den Graphen aus der Datei laden.
            string filePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), fileName);
            Graph<string> graph = GraphGenerator.LoadFromFile(filePath);

            // Den Start- und Endknoten der Dijkstra-Suche definieren.
            Vertex<string> start = new Vertex<string>(valueStart);
            Vertex<string> end = (valueEnd != null) ? new Vertex<string>(valueEnd) : null;

            // Dijkstra ausführen
            Graph<string> result = null;
            if (end != null)
            {
                result = graph.Dijkstra(start, end);
            }
            else
            {
                result = graph.Dijkstra(start);
            }

            // Den Pfad zusammenstellen
            Stack<Vertex<string>> dijkstraPath = new Stack<Vertex<string>>();
            Vertex<string> actual = graph.GetVertex(new Vertex<string>("996"));
            while (!actual.Equals(start))
            {
                dijkstraPath.Push(actual);
                actual = actual.DijkstraAncestor;
            }

            // Den letzten Knoten (start) noch pushen
            dijkstraPath.Push(start);

            // Die Anzahl an bereits vorhandenen Ergebnissen des DijkstraAlgorithmus ermitteln
            string baseFileName = fileName.Split('.')[0];
            int resultFileCount = Directory.GetFiles(
                    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), 
                    "dijkstraResult_" + baseFileName + "[*].txt", SearchOption.TopDirectoryOnly)
                .Length;

            // Den Pfad für die Ergebnisdatei zusammenstellen.
            string resultFilePath = Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), 
                "dijkstraResult_" + baseFileName + "[" + resultFileCount + "].txt");

            File.AppendAllLines(resultFilePath, dijkstraPath.Select(v => v.Value));
        }
    }
}
