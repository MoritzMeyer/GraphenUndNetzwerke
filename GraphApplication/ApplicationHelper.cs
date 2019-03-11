using GraphCollection;
using GraphCollection.SearchAlgorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphApplication
{
    /// <summary>
    /// Helfer Klasse für die Konsolen-Anwendung.
    /// </summary>
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

        #region CallStrongConnect
        /// <summary>
        /// Lädt einen Graphen aus der angegebenen Datei und berechnet für diesen die strongly connected components
        /// </summary>
        /// <param name="fileName"></param>
        public static void CallStrongConnect(string fileName)
        {
            Graph<string> graph = ApplicationHelper.LoadGraph(fileName, true);

            List<List<Vertex<string>>> components = StronglyConnectedComponents<string>.Search(graph);

            // Das Ergebnis in eine Datei schreiben.
            string baseFileName = fileName.Split('.')[0];
            ApplicationHelper.WriteResult(
                baseFileName, 
                "strongConnectResult_", 
                components.Select(
                    (list) => "Component " + components.IndexOf(list) + ":" + Environment.NewLine + list.Select((edge) => edge.ToString()).Aggregate((a, b) => a + Environment.NewLine + b)))
;
        }
        #endregion

        #region CallTopSort
        /// <summary>
        /// Lädt einen Graphen aus der angegebenen Datei und führt auf diesem den TopSort-Algorithmus aus.
        /// </summary>
        /// <param name="fileName"></param>
        public static void CallTopSort(string fileName)
        {
            // Den Graphen aus der Datei laden.
            Graph<string> graph = ApplicationHelper.LoadGraph(fileName, true);

            // TopSort ausführen.
            SortedList<int, Vertex<string>> tSort = graph.TopSort();

            // Das Ergebnis in eine Datei schreiben.
            string baseFileName = fileName.Split('.')[0];
            ApplicationHelper.WriteResult(baseFileName, "topSortResult_", tSort.Select((kv) => kv.Value.ToString()))
;
        }
        #endregion

        #region CallAllPairsShortestPath
        /// <summary>
        /// Lädt einen Graphen aus der gegebenen Datei und führt auf diesem den AllPairsShortestPath-Algorithmus aus.
        /// </summary>
        /// <param name="fileName"></param>
        public static void CallAllPairsShortestPath(string fileName)
        {
            Graph<string> graph = ApplicationHelper.LoadGraph(fileName, true);

            string result = graph.AllPairsShortestPath();

            string baseFileName = fileName.Split('.')[0];
            ApplicationHelper.WriteResult(baseFileName, "allPairsShortestPathResult_", new List<string>() { result });
        }
        #endregion

        #region CallDepthFirstSearch
        /// <summary>
        /// Lädt den Graphen aus der Datei und führt auf diesem den Depth-First-Search Algrithmus aus.
        /// </summary>
        /// <param name="fileName">Die Datei, welche den Graphen enthält.</param>
        /// <param name="start">Der Knoten von dem aus gesucht werden soll.</param>
        /// <param name="target">Der gesuchte Knoten.</param>
        public static void CallDepthFirstSearch(string fileName, string start, string target)
        {
            // Den Graphen aus der Datei laden.
            Graph<string> graph = ApplicationHelper.LoadGraph(fileName);

            // DFS ausführen
            graph.DepthFirstSearch(new Vertex<string>(start), new Vertex<string>(target), out List<Edge<string>> dfsPath);

            // Das Ergebnis in eine Datei schreiben.
            string baseFileName = fileName.Split('.')[0];
            ApplicationHelper.WriteResult(baseFileName, "dfsResult_", dfsPath.Select((edge) => edge.ToString()));
        }
        #endregion

        #region LoadGraph
        /// <summary>
        /// Lädt einen Graphen aus der gegebenen Datei.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Der Graph aus der Datei.</returns>
        private static Graph<string> LoadGraph(string fileName, bool isDirected = false)
        {
            // Den Graphen aus der Datei laden.
            string filePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), fileName);
            Graph<string> graph = GraphGenerator.LoadFromFile(filePath, isDirected);
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
