using System;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //Thread.Sleep(timeout: TimeSpan.FromSeconds(30));
            //Trace.WriteLine(message: "Goodbye·World!");

            ArraySegmentTest();
        }

        private static void SpanTest()
        {
            //string str = "Hello World";
            //str.StartsWith()
            //str.AsReadOnlySpan().Slice(start: 7, length: 5);
        }

        private static void ArraySegmentTest()
        {
            string[] arr = { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };

            // Create an ArraySegment that contains the entire array.
            ArraySegment<string> arrSeg = new ArraySegment<string>(arr);
            Console.WriteLine("The entire array:");
            PrintIndexAndValues(arrSeg);

            // Create an ArraySegment that contains the middle five strings.
            arrSeg = new ArraySegment<string>(arr, 1, 5);
            Console.WriteLine("The middle five elements:");
            PrintIndexAndValues(arrSeg);

            // Modify the original array.
            arrSeg[1] = "black";
            arrSeg[3] = "bear";

            // The ArraySegment reflects the change.
            Console.WriteLine("After the change to the original array:");
            PrintIndexAndValues(arrSeg);
        }

        public static void PrintIndexAndValues(ArraySegment<string> arrSeg)
        {
            for (int i = arrSeg.Offset; i < (arrSeg.Offset + arrSeg.Count); i++)
            {
                Console.WriteLine("   [{0}] : {1}", i, arrSeg.Array[i]);
            }
            Console.WriteLine();
        }

        public static void PrintIndexAndValues(String[] myArr)
        {
            for (int i = 0; i < myArr.Length; i++)
            {
                Console.WriteLine("   [{0}] : {1}", i, myArr[i]);
            }
            Console.WriteLine();
        }
    }
}
