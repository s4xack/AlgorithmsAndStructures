using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmsAndStructures.SortingAlgorithms
{
    class RadixSort
    {
        private static string[] Sort(string[] arrayToSort, int digitsCount, int phasesCount)
        {
            string[] array = new string[arrayToSort.Length];
            arrayToSort.CopyTo(array, 0);
            string[] nextPhaseArray = new string[arrayToSort.Length];

            for (int phase = 0; phase < phasesCount; phase++)
            {
                int j = digitsCount - phase - 1;
                int[] digits = new int[26];
                int digit;
                for (int i = 0; i < array.Length; ++i)
                {
                    digit = (int) (array[i][j] - 'a');
                    digits[digit]++;
                }

                int preCount = 0;
                int temp;
                for (int i = 0; i < 26; ++i)
                {
                    temp = digits[i];
                    digits[i] = preCount;
                    preCount += temp;
                }

                for (int i = 0; i < array.Length; ++i)
                {
                    digit = (int) (array[i][j] - 'a');
                    nextPhaseArray[digits[digit]] = array[i];
                    digits[digit]++;
                }

                nextPhaseArray.CopyTo(array, 0);
            }

            return array;
        }

        public static void Solve()
        {
            int n, m, k;
            string[] input = Console.ReadLine().Split();
            n = int.Parse(input[0]);
            m = int.Parse(input[1]);
            k = int.Parse(input[2]);
            string[] array = new string[n];
            for (int i = 0; i < n; ++i)
            {
                array[i] = Console.ReadLine();
            }

            array = Sort(array, m, k);
            for (int i = 0; i < n; ++i)
            {
                Console.WriteLine(array[i]);
            }
        }
    }
}
