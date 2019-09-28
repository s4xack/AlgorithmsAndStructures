using System;
using System.Linq;
using System.IO;

namespace AlgorithmsAndStructures.SortingAlgorithms
{
    class InversionsCount<Template> where Template : IComparable<Template>
    {
        public static decimal cnt = 0;
        private static Template[] CountInversions(Template[] needToSortArray)
        {
            if (needToSortArray.Length == 1)
            {
                return needToSortArray;
            }
            int middlePosition = needToSortArray.Length / 2;
            return Merge(CountInversions(needToSortArray.Take(middlePosition).ToArray()), CountInversions(needToSortArray.Skip(middlePosition).ToArray()));
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
                    InversionsCount<int>.cnt += leftArray.Length - leftIterator;
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
            int[] array;
            using (var inputFile = new StreamReader("inversions.in"))
            {
                int n = Int32.Parse(inputFile.ReadLine());
                array = new int[n];
                string[] inputArray = inputFile.ReadLine().Split();
                for (int i = 0; i < n; ++i)
                {
                    array[i] = Int32.Parse(inputArray[i]);
                }
            }
            InversionsCount<int>.cnt = 0;
            InversionsCount<int>.CountInversions(array);
            decimal result = InversionsCount<int>.cnt;

            using (var outputFile = new StreamWriter("inversions.out"))
            {
                outputFile.WriteLine(result);
            }
        }
    }
    
    class StartInversionsCounter
    {
        public static void Solve()
        {
            InversionsCount<int>.Solve();
        }
    }
}
