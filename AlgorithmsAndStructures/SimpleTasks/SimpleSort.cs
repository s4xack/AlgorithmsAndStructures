using System;
using System.IO;

namespace AlgorithmsAndStructures.SimpleTasks
{
    class SimpleSort
    {
        public static void Solve()
        {
            string[] input = File.ReadAllLines("smallsort.in");
            int n = Int32.Parse(input[0]);
            string[] stringArray = input[1].Split();
            int[] array = new int[n];

            for (int i = 0; i < n; ++i)
            {
                array[i] = Int32.Parse(stringArray[i]);
            }

            int temp;
            for (int t = 0; t < n; ++t)
            {
                for (int i = 0; i < n - 1; ++i)
                {
                    if (array[i] > array[i + 1])
                    {
                        temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                    }
                }
            }
            string result = "";
            for (int i = 0; i < n; ++i)
            {
                result += Convert.ToString(array[i]) + " ";
            }
            File.WriteAllText("smallsort.out", result);
        }
    }
}
