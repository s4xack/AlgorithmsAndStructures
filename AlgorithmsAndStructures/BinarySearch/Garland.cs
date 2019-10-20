using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlgorithmsAndStructures.BinarySearch
{
    class Garland
    {
        private static double answer;

        private static void BinSearch(double[] bulbsHeight, double left, double right, int bulbsCount)
        {
            while (right - left > 0.000005)
            {
                double mid = (right + left) / 2.0;
                if (CheckValid(bulbsHeight, mid, bulbsCount))
                {
                    right = mid;
                }
                else
                {
                    left = mid;
                }
            }
        }

        private static bool CheckValid(double[] bulbsHeight, double secondBulbHeight, int bulbsCount)
        {
            bulbsHeight[1] = secondBulbHeight;

            for (int i = 2; i < bulbsCount; ++i)
            {
                bulbsHeight[i] = 2.0 * bulbsHeight[i - 1] + 2.0 - bulbsHeight[i - 2];
                if (bulbsHeight[i] < 0)
                {
                    return false;
                }
            }

            answer = bulbsHeight[bulbsCount - 1];
            return true;
        }

        public static void Solve()
        {
            string[] input;
            input = Console.ReadLine()?.Split();

            int bulbsCount = int.Parse(input[0]);
            double firstBulbHeight = double.Parse(input[1].Replace(".", ","));
            double[] bulbsHeight = new double[bulbsCount];
            bulbsHeight[0] = firstBulbHeight;

            BinSearch(bulbsHeight, 0.00, firstBulbHeight, bulbsCount);
            Console.WriteLine($"{(int)(answer)}.{(int)(answer * 100 % 100)}");
        }
    }
}
