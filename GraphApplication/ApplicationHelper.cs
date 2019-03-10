using GraphCollection;
using GraphCollection.SearchAlgorithms;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphApplication
{
    public static class ApplicationHelper
    {
        #region CallDijkstra
        /// <summary>
        /// Lädt einen Graphen aus einer Datei und führt auf diesem den Dijkstra-Algorithmus mit den gegebenen Parametern aus.
        /// </summary>
        /// <param name="fileName">Der Dateiname in dem der zu prüfende Graph liegt.</param>
        /// <param name="vStart">Der Knoten von dem aus die kürzesten Pfade berechnet werden sollen.</param>
        /// <param name="vEnd">Der Knoten zu dem der kürzeste Pfad berechnet werden soll.</param>
        public static void CallDijkstra(string fileName, string vStart, string vEnd = null)
        {
            // Den Graphen aus der Datei laden.
            Graph<string> graph = ApplicationHelper.LoadGraph(fileName);

            // Den Start- und Endknoten der Dijkstra-Suche definieren.
            Vertex<string> start = new Vertex<string>(vStart);
            Vertex<string> end = (vEnd != null) ? new Vertex<string>(vEnd) : null;

            // Dijkstra ausführen
            Graph<string> result = (end != null) ? graph.Dijkstra(start, end) : graph.Dijkstra(start);

            // Den Ergebnis - Pfad zusammenstellen
            Stack<Vertex<string>> dijkstraPath = new Stack<Vertex<string>>();
            Vertex<string> actual = graph.GetVertex(new Vertex<string>("996"));
            while (!actual.Equals(start))
            {
                dijkstraPath.Push(actual);
                actual = actual.DijkstraAncestor;
            }

            // Den letzten Knoten (start) noch pushen
            dijkstraPath.Push(start);

            // Das Ergebnis in eine Datei schreiben.
            string baseFileName = fileName.Split('.')[0];
            ApplicationHelper.WriteResult(baseFileName, "dijkstraResult_", dijkstraPath.Select(v => v.Value));
        }
        #endregion

        #region CallPrim
        /// <summary>
        /// Lädt einen Graphen aus der gegebenen Datei und führt auf diesem den Prim-Algorithmus aus.
        /// </summary>
        /// <param name="fileName">Der Name der Datei, in dem der zu prüfende Graph enthalten ist.</param>
        /// <param name="vStart">Optional kann ein Knoten angegeben werden, von dem aus der Minimale Spannbaum aufgezogen werden soll.</param>
        public static void CallPrim(string fileName, string vStart = null)
        {
            // Den Graphen aus der Datei holen.
            Graph<string> graph = ApplicationHelper.LoadGraph(fileName);

            // Falls vorhanden den Start-Vertex setzen.
            Vertex<string> start = (vStart != null) ? new Vertex<string>(vStart) : null;

            // Prim ausführen.
            Graph<string> result = (start != null) ? graph.Prim(start) : graph.Prim();

            // Das Ergebnis in eine Datei schreiben.
            string baseFileName = fileName.Split('.')[0];
            ApplicationHelper.WriteResult(baseFileName, "primResult_", result.Edges.Select((edge) => edge.ToString()));
        }
        #endregion

        #region CallKruskal
        /// <summary>
        /// Lädt einen Graphen aus der angegebenen Datei und führt auf diesem den Kruskal-Algorithmus aus.
        /// </summary>
        /// <param name="fileName"></param>
        public static void CallKruskal(string fileName)
        {
            // Den Graphen aus der Datei laden.
            Graph<string> graph = ApplicationHelper.LoadGraph(fileName);

            // Kruskal ausführen.
            Graph<string> result = graph.KruskalDisjointSet();

            // Das Ergebnis in eine Datei schreiben.
            string baseFileName = fileName.Split('.')[0];
            ApplicationHelper.WriteResult(baseFileName, "kruskalResult_", result.Edges.Select((edge) => edge.ToString()));
        }
        #endregion

        #region CallFordFulkerson
        /// <summary>
        /// Lädt den Graphen aus der gegebenen Datei und führt auf diesem den FordFulkerson Algorithmus aus.
        /// <paramref name="fileName">Der Name der Datei welche den Graphen enthält.</paramref>
        /// </summary>
        public static void CallFordFulkerson(string fileName)
        {
            // Den Graphen aus der Datei laden.
            FlowGraph<string> graph = (FlowGraph<string>)ApplicationHelper.LoadGraph(fileName);

            // Ford-Fulkerson ausführen.
            FlowGraph<string> result = graph.FordFulkerson();

            // Das Ergebnis in eine Datei schreiben.
            string baseFileName = fileName.Split('.')[0];
            ApplicationHelper.WriteResult(baseFileName, "fordfulkersonResult_", result.Edges.Select((edge) => edge.ToString()));
        }
        #endregion

        #region LoadGraph
        /// <summary>
        /// Lädt einen Graphen aus der gegebenen Datei.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static Graph<string> LoadGraph(string fileName)
        {
            // Den Graphen aus der Datei laden.
            string filePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), fileName);
            Graph<string> graph = GraphGenerator.LoadFromFile(filePath);
            return graph;
        }
        #endregion

        #region WriteResult
        /// <summary>
        /// Schreibt ein IEnumerable (Ergebnis) in eine angegebenen Datei.
        /// </summary>
        /// <param name="fileName">Der Name der Ursprungsdatei.</param>
        /// <param name="filePrefix">Der Name der zu schreibenden Datei.</param>
        /// <param name="content">Der Inhalt.</param>
        private static void WriteResult(string baseFileName, string filePrefix, IEnumerable<string> content)
        {
            // Die Anzahl an bereits vorhandenen Ergebnissen des prefixes ermitteln
            int resultFileCount = Directory.GetFiles(
                    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    filePrefix + baseFileName + "[*].txt", SearchOption.TopDirectoryOnly)
                .Length;

            // Den Pfad für die Ergebnisdatei zusammenstellen.
            string resultFilePath = Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                filePrefix + baseFileName + "[" + resultFileCount + "].txt");
        
            File.AppendAllLines(resultFilePath, content);
        }
        #endregion
    }
}
