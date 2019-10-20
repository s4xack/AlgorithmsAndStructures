using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlgorithmsAndStructures.Heap
{
    class IsHeap
    {
        public static void Solve()
        { 
            int n;
            Int64[] array;
            using (var inputFile = new StreamReader("isheap.in"))
            {
                n = int.Parse(Console.ReadLine());
                array = Console.ReadLine()?.Split(' ').Select(Int64.Parse).ToArray();
            }
            bool isHeap = true;
            
            for (int i = 0; i < n / 2; ++i)
            {
                if (2 * i + 1 < n && array[2 * i + 1] < array[i])
                    isHeap = false;

                if (2 * i + 2 < n && array[2 * i + 2] < array[i])
                    isHeap = false;
            }
            using (var outputFile = new StreamWriter("isheap.out"))
            {
                Console.WriteLine(isHeap ? "YES" : "NO");
            }
        }
    }
}
