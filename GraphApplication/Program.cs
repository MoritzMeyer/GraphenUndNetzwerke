using System;
using System.IO;
using System.Linq;

namespace GraphApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // remove parameter '-'
            foreach (string arg in args)
            {
                if (arg[0].Equals('-'))
                {
                    args[0] = arg.Remove(0, 1);
                }
            }

            // Die Input Parameter prüfen.
            switch (args.Count())
            {
                case 0:
                    Program.WriteUsageInfo();
                    break;
                case 1:
                    if (args[0].ToLower().Equals("help") || args[0].ToLower().Equals("h"))
                    {
                        Program.WriteUsageInfo();
                    }
                    else if (args[0].ToLower().Equals("info"))
                    {
                        WriteInfo();
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Unknown Paramter");
                        Console.WriteLine("");
                        Program.WriteUsageInfo();
                    }
                    break;
                    //throw new ArgumentException("Not enough Arguments given.");
                case 2:
                    string filePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), args[0]);
                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine("File with given filename could not be found in application folder.");
                        //throw new ArgumentException("File doesn't exists.");
                    }
                    break;
            }

            // Je nach angabe des zweiten Parameters die entsprechende Funktion aufrufen.
            switch (args[1].ToLower())
            {
                case "dijkstra":
                    if (args.Count() != 4)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Number of Arguments doesn't match the function requirements");
                        Program.WriteUsageInfo();
                        //throw new ArgumentException("Not enough Arguments for this function.");
                    }

                    ApplicationHelper.CallDijkstra(args[0], args[2], args[3]);
                    break;
                case "prim":
                    if (args.Count() > 3)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Number of Arguments doesn't match the function requirements");
                        Program.WriteUsageInfo();
                        //throw new ArgumentException("To much parameters for this function");
                    }

                    if (args.Count() == 2)
                    {
                        ApplicationHelper.CallPrim(args[0]);
                    }
                    else
                    {
                        ApplicationHelper.CallPrim(args[0], args[2]);
                    }

                    break;
                case "kruskal":
                    if (args.Count() > 2)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Number of Arguments doesn't match the function requirements");
                        Program.WriteUsageInfo();
                        //throw new ArgumentException("To much parameters for this function");
                    }

                    ApplicationHelper.CallKruskal(args[0]);
                    break;
                case "ford-fulkerson":
                    if (args.Count() > 2)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Number of Arguments doesn't match the function requirements");
                        Program.WriteUsageInfo();
                    }

                    ApplicationHelper.CallFordFulkerson(args[0]);
                    break;
                case "strong-connect":
                    if (args.Count() > 2)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Number of Arguments doesn't match the function requirements");
                        Program.WriteUsageInfo();
                    }

                    ApplicationHelper.CallStrongConnect(args[0]);
                    break;
                case "tsort":
                    if (args.Count() > 2)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Number of Arguments doesn't match the function requirements");
                        Program.WriteUsageInfo();
                    }

                    ApplicationHelper.CallTopSort(args[0]);
                    break;
                case "allpairs-shortestpath":
                    if (args.Count() > 2)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Number of Arguments doesn't match the function requirements");
                        Program.WriteUsageInfo();
                    }

                    ApplicationHelper.CallAllPairsShortestPath(args[0]);
                    break;
                case "dfs":
                case "depth-first-search":
                    if (args.Count() != 4)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Number of Arguments doesn't match the function requirements");
                        Program.WriteUsageInfo();
                    }

                    ApplicationHelper.CallDepthFirstSearch(args[0], args[2], args[3]);
                    break;
                default:
                    Console.WriteLine("");
                    Console.WriteLine($"No matching funciton found for '{args[1]}'");
                    break;
                    //throw new ArgumentException($"No matching function found for '{args[1]}'");
            }            

            //Console.WriteLine(args.Select((arg) => arg.ToString()).Aggregate((a, b) => a + ", " + b));
        }

        private static void WriteUsageInfo()
        {
            Console.WriteLine("");
            Console.WriteLine("Usage: graphsuitemm [filename] [functionname] [function-parameter]");
            Console.WriteLine("Usage: graphsuitemm [options]");
            Console.WriteLine("");
            Console.WriteLine("filename");
            Console.WriteLine("\t The filename of the file which contains graph data. This must reside in the applications folder.");
            Console.WriteLine("");
            Console.WriteLine("functionname");
            Console.WriteLine("\t Name of the function to execute on the given graph. Possible functions are:");
            Console.WriteLine("\t  - dijkstra \t fuction-parameter:  [start vertex] [target vertex]");
            Console.WriteLine("\t  - prim \t function-parameter: [start vertex (optional)]");
            Console.WriteLine("\t  - kruskal \t (no parameters)");
            Console.WriteLine("\t  - ford-fulkerson \t (no parameters)");
            Console.WriteLine("\t  - strong-connect \t (no parameters)");
            Console.WriteLine("\t  - tsort \t (no parameters)");
            Console.WriteLine("");
            Console.WriteLine("options:");
            Console.WriteLine(" -h|-help \t Display this dialog.");
            Console.WriteLine(" -info    \t Display graphsuite information");

            Environment.Exit(0);
        }

        private static void WriteInfo()
        {
            Console.WriteLine("");
            Console.WriteLine("Graphsuite developed in the context of the lecture 'Graphen und Netwerke' at Hochschule Fulda University of Applied Science by Moritz Meyer.");
            Environment.Exit(0);
        }
    }
}
