using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsAndStructures.BinarySearch
{
    class BinarySearch
    {
        private static int BinSearch(int[] array, int searchElement, bool leftSearch)
        {
            const int notFound = -1;

            if (array[0] > searchElement || array[array.Length - 1] < searchElement)
            {
                return notFound;
            }

            int leftPointer = -1;
            int rightPointer = array.Length;

            while (rightPointer - leftPointer > 1)
            {
                int midPointer = (rightPointer + leftPointer) / 2;
                if (leftSearch ? array[midPointer] < searchElement : array[midPointer] <= searchElement)
                {
                    leftPointer = midPointer;
                }
                else
                {
                    rightPointer = midPointer;
                }
            }

            if (leftSearch ? array[rightPointer] == searchElement : array[rightPointer - 1] == searchElement)
            {
                return leftSearch ? rightPointer + 1 : rightPointer;
            }

            return notFound;
        }

        public static void Solve()
        {
            var n = int.Parse(Console.ReadLine() ?? "");
            var array = Console.ReadLine()?.Split(' ').Select(int.Parse).ToArray();
            
            var m = int.Parse(Console.ReadLine() ?? "");
            var requests = Console.ReadLine()?.Split(' ').Select(int.Parse).ToArray();

            for (int i = 0; i < m; ++i)
            {
                Console.WriteLine($"{BinSearch(array, requests[i], true)} {BinSearch(array, requests[i], false)}");
            }
        }
    }
}
