using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AlgorithmsAndStructures.SortingAlgorithms
{
    class SortForRunners
    {
        private static List<string> MergeSorting(List<string> needToSortArray)
        {
            if (needToSortArray.Count() == 1)
            {
                return needToSortArray;
            }
            int middlePosition = needToSortArray.Count() / 2;
            return Merge(MergeSorting(needToSortArray.Take(middlePosition).ToList()), MergeSorting(needToSortArray.Skip(middlePosition).ToList()));
        }

        private static List<string> Merge(List<string> leftArray, List<string> rightArray)
        {
            int leftIterator = 0;
            int rightIterator = 0;
            int leftLenght = leftArray.Count();
            int rightLength = rightArray.Count();
            List<string> mergedArray = new List<string>(leftLenght + rightLength);

            while (leftIterator < leftLenght && rightIterator < rightLength)
            {
                if (String.CompareOrdinal(leftArray[leftIterator], rightArray[rightIterator]) <= 0)
                {
                    mergedArray.Add(leftArray[leftIterator++]);
                }
                else
                {
                    mergedArray.Add(rightArray[rightIterator++]);
                }
            }

            while (leftIterator < leftLenght)
            {
                mergedArray.Add(leftArray[leftIterator++]);
            }

            while (rightIterator < rightLength)
            {
                mergedArray.Add(rightArray[rightIterator++]);
            }

            return mergedArray;

        }
        public static void Solve()
        {
            Dictionary<string, List<string>> names = new Dictionary<string, List<string>>();

            using (var inputFile = new StreamReader("race.in"))
            {
                int n = Int32.Parse(inputFile.ReadLine());
                
                string name, country;
                for (int i = 0; i < n; ++i)
                {
                    string[] inp = inputFile.ReadLine().Split();
                    country = inp[0];
                    name = inp[1];
                    if (!names.ContainsKey(country))
                    {
                        names[country] = new List<string>() { name };
                    }
                    else
                    {
                        names[country].Add(name);
                    }
                }
            }
            List<string> array = MergeSorting(names.Keys.ToList());
            string lastCountry = string.Empty; 
            using (var outputFile = new StreamWriter("race.out"))
            {
                foreach (string i in array)
                {
                    outputFile.WriteLine($"=== {i} ===");
                    foreach(string j in names[i])
                    {
                        outputFile.WriteLine(j);
                    }
                }
            }
        }
    }
}
