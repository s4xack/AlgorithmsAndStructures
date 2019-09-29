using System;
using System.IO;
using System.Linq;

namespace AlgorithmsAndStructures.SortingAlgorithms
{
    class AntiQuickSort
    {
        private static void Swap(ref int firstElement, ref int secondElement)
        {
            int swapHelper = firstElement;
            firstElement = secondElement;
            secondElement = swapHelper;
        }

        public static void Solve()
        {
            int n = int.Parse(Console.ReadLine());

            int[] antiArray = Enumerable.Range(1, n).ToArray();

            for (int i = 2; i < n; i++)
            {
                Swap(ref antiArray[i], ref antiArray[i / 2]);
            }

            Console.WriteLine(String.Join(" ", antiArray));
        }
    }
}
