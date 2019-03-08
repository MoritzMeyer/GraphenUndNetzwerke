using System;
using System.Linq;

namespace GraphApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args.Select((arg) => arg.ToString()).Aggregate((a, b) => a + ", " + b));
        }
    }
}
