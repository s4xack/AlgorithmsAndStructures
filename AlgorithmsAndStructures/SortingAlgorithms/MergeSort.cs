using System;
using System.Linq;
using System.IO;

namespace AlgorithmsAndStructures.SortingAlgorithms
{
    class MergeSort<Template> where Template: IComparable<Template>
    {
        private static Template[] MergeSorting (Template[] needToSortArray)
        {
            if (needToSortArray.Length == 1)
            {
                return needToSortArray;
            }
            int middlePosition = needToSortArray.Length / 2;
            return Merge(MergeSorting(needToSortArray.Take(middlePosition).ToArray()), MergeSorting(needToSortArray.Skip(middlePosition).ToArray()));
        }

        private static Template[] Merge(Template[] leftArray, Template[] rightArray)
        {
            int leftIterator = 0;
            int rightIterator = 0;
            Template[] mergedArray = new Template[leftArray.Length + rightArray.Length];
            int mergedIterator = 0;

            while (leftIterator < leftArray.Length && rightIterator < rightArray.Length)
            {
                if (leftArray[leftIterator].CompareTo(rightArray[rightIterator]) <= 0)
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
        public static void Solve()
        {
            int n = Int32.Parse(Console.ReadLine());
            int[] array = new int[n];
            string[] inputArray = Console.ReadLine().Split();
            for (int i = 0; i < n; ++i)
            {
                array[i] = Int32.Parse(inputArray[i]);
            }
            array = MergeSort<int>.MergeSorting(array);
            Console.WriteLine(String.Join(" ", array));
        }
    }
    class StartMergeSort
    {
        public static void Solve()
        {
            MergeSort<int>.Solve();
        }
    }
}
