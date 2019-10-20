using System;
using System.Linq;

namespace AlgorithmsAndStructures.SortingAlgorithms
{
    class Sort<Template> where Template: IComparable<Template>
    {
        private static Template[] MergeSort (Template[] needToSortArray)
        {
            if (needToSortArray.Length == 1)
            {
                return needToSortArray;
            }
            int middlePosition = needToSortArray.Length / 2;
            return Merge(MergeSort(needToSortArray.Take(middlePosition).ToArray()), MergeSort(needToSortArray.Skip(middlePosition).ToArray()));
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
            array = Sort<int>.MergeSort(array);
            Console.WriteLine(String.Join(" ", array));
        }
    }
    class StartMergeSort
    {
        public static void Solve()
        {
            Sort<int>.Solve();
        }
    }
}
