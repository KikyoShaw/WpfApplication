using System;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Thread.Sleep(timeout: TimeSpan.FromSeconds(30));
            Trace.WriteLine(message: "Goodbye·World!");
        }
    }
}
