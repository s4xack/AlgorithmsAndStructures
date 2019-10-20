using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsAndStructures.BinarySearch
{
    class BinarySearch
    {
        private static int BinSearch(int[] array, int searchValue, bool leftSearch)
        {
            const int notFound = -1;

            if (array[0] > searchValue || array[array.Length - 1] < searchValue)
            {
                return notFound;
            }

            int leftPointer = -1;
            int rightPointer = array.Length;

            while (rightPointer - leftPointer > 1)
            {
                int midPointer = (rightPointer + leftPointer) / 2;
                if (leftSearch ? array[midPointer] < searchValue : array[midPointer] <= searchValue)
                {
                    leftPointer = midPointer;
                }
                else
                {
                    rightPointer = midPointer;
                }
            }

            if (leftSearch ? array[rightPointer] == searchValue : array[rightPointer - 1] == searchValue)
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
