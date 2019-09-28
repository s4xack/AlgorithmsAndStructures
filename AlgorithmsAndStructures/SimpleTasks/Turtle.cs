using System;
using System.IO;

namespace AlgorithmsAndStructures.SimpleTasks
{
    class Turtle
    {
        static void Main(string[] args)
        {
            int h, w;
            string[] input = File.ReadAllLines("turtle.in");
            h = Int32.Parse(input[0].Split()[0]);
            w = Int32.Parse(input[0].Split()[1]);
            int[,] cost = new int[h + 2, w + 2];
            string[] line;
            for (int i = 1; i <= h; ++i)
            {
                line = input[i].Split();
                for (int j = 1; j <= w; ++j)
                {
                    cost[i, j] = Int32.Parse(line[j - 1]);
                }
            }
            int[,] dp = new int[h + 2, w + 2];
            for (int i = h; i > 0; --i)
            {
                for (int j = 1; j <= w; ++j)
                {
                    dp[i, j] = Math.Max(dp[i, j - 1], dp[i + 1, j]) + cost[i, j];
                }
            }

            string result = Convert.ToString(dp[1, w]);
            File.WriteAllText("turtle.out", result);
        }
    }
}
