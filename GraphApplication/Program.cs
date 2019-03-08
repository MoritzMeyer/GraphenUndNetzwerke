using System;
using System.IO;
using System.Linq;

namespace GraphApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase));
            //Console.ReadKey();

            // Die Input Parameter prüfen.
            switch (args.Count())
            {
                case 0:
                case 1:
                    throw new ArgumentException("Not enough Arguments given.");
                case 2:
                    string filePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), args[0]);
                    if (!File.Exists(filePath))
                    {
                        throw new ArgumentException("File doesn't exists.");
                    }
                    break;
            }

            // Je nach angabe des zweiten Parameters die entsprechende Funktion aufrufen.
            switch (args[1].ToLower())
            {
                case "dijkstra":
                    if (args.Count() < 3 || args.Count() > 4)
                    {
                        throw new ArgumentException("Not enough Arguments for this function.");
                    }

                    if (args.Count() == 3)
                    {
                        ApplicationHelper.CallDijkstra(args[0], args[2]);
                    }
                    else
                    {
                        ApplicationHelper.CallDijkstra(args[0], args[2], args[3]);
                    }
                    
                    break;
                case "prim":
                    break;
                default:
                    throw new ArgumentException($"No matching function found for '{args[1]}'");
            }

            

            Console.WriteLine(args.Select((arg) => arg.ToString()).Aggregate((a, b) => a + ", " + b));
        }

        
    }
}
