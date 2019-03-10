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

            // Die Anzahl an bereits vorhandenen Ergebnissen des DijkstraAlgorithmus ermitteln
            string baseFileName = fileName.Split('.')[0];
            int resultFileCount = Directory.GetFiles(
                    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    "primResult_" + baseFileName + "[*].txt", SearchOption.TopDirectoryOnly)
                .Length;

            // Den Pfad für die Ergebnisdatei zusammenstellen.
            string resultFilePath = Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "primResult_" + baseFileName + "[" + resultFileCount + "].txt");

            /*string resultFilePathAdjecent = Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "primAdjacent_" + baseFileName + "[" + resultFileCount + "].txt");*/

            File.AppendAllLines(resultFilePath, result.Edges.Select((edge) => edge.ToString()));
            //File.AppendAllText(resultFilePathAdjecent, result.GetAdjacencyMatrix());

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
            //Graph<string> result = graph.Kruskal();
            Graph<string> result = graph.KruskalDisjointSet();

            // Die Anzahl an bereits vorhandenen Ergebnissen des DijkstraAlgorithmus ermitteln
            string baseFileName = fileName.Split('.')[0];
            int resultFileCount = Directory.GetFiles(
                    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    "kurskalResult_" + baseFileName + "[*].txt", SearchOption.TopDirectoryOnly)
                .Length;

            // Den Pfad für die Ergebnisdatei zusammenstellen.
            string resultFilePath = Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "kurskalResult_" + baseFileName + "[" + resultFileCount + "].txt");

            /*string resultFilePathAdjecent = Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "kurskalAdjacent_" + baseFileName + "[" + resultFileCount + "].txt");*/

            File.AppendAllLines(resultFilePath, result.Edges.Select((edge) => edge.ToString()));
            //File.AppendAllText(resultFilePathAdjecent, result.GetAdjacencyMatrix());
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
    }
}
