using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class CheckParallelEdges
    {
        private static bool IsHaveParallelEdges(int[,] matrix)
        {
            int n = (int)(Math.Sqrt(matrix.Length));
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] > 1)
                        return true;
                }
            }

            return false;
        }

        private static (int, int) ReadTwoNumbers(StreamReader input)
        {
            int a, b;
            string inputLine = input.ReadLine();
            string[] inputStrings = inputLine.Split();
            a = int.Parse(inputStrings[0]);
            b = int.Parse(inputStrings[1]);

            return (a, b);
        }

        public static void Solve()
        {
            int n, m;
            int[,] matrix;
            using (var input = new StreamReader("input.txt"))
            {
                (n, m) = ReadTwoNumbers(input);
                matrix = new int[n, n];

                int v, u;
                for (int i = 0; i < m; i++)
                {
                    (v, u) = ReadTwoNumbers(input);
                    v--;
                    u--;
                    matrix[v, u]++;
                    matrix[u, v]++;
                }
            }

            bool result = IsHaveParallelEdges(matrix);

            using (var output = new StreamWriter("output.txt"))
            {
                output.WriteLine(result ? "YES" : "NO");
            }
        }
    }
}
