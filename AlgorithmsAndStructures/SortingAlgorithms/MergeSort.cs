using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AlgorithmsAndStructures.SortingAlgorithms
{
    class MergeSort
    {
        private static int[] MergeSorting (int[] needToSortArray)
        {
            if (needToSortArray.Length == 1)
            {
                return needToSortArray;
            }
            int middlePosition = needToSortArray.Length / 2;
            return Merge(MergeSorting(needToSortArray.Take(middlePosition).ToArray()), MergeSorting(needToSortArray.Skip(middlePosition).ToArray()));
        }

        private static int[] Merge(int[] leftArray, int[] rightArray)
        {
            int leftIterator = 0;
            int rightIterator = 0;
            int[] mergedArray = new int[leftArray.Length + rightArray.Length];
            int mergedIterator = 0;

            while (leftIterator < leftArray.Length && rightIterator < rightArray.Length)
            {
                if (leftArray[leftIterator] <= rightArray[rightIterator])
                {
                    mergedArray[mergedIterator] = leftArray[leftIterator++];
                }
                else
                {
                    mergedArray[mergedIterator] = rightArray[rightIterator++]; 
                }
                mergedIterator++;
            }

            while (leftIterator < leftArray.Length)
            {
                mergedArray[mergedIterator++] = leftArray[leftIterator++];
            }

            while (rightIterator < rightArray.Length)
            {
                mergedArray[mergedIterator++] = rightArray[rightIterator++];
            }

            return mergedArray;

        }
        public static void Main()
        {
            int n = Int32.Parse(Console.ReadLine());
            int[] array = new int[n];
            string[] inputArray = Console.ReadLine().Split();
            for (int i = 0; i < n; ++i)
            {
                array[i] = Int32.Parse(inputArray[i]);
            }
            array = MergeSorting(array);
            for (int i = 0; i < n; ++i)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }
    }
}
