using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class CheckOriented
    {
        private static bool IsOriented(int[,] matrix)
        {
            int n = (int)(Math.Sqrt(matrix.Length));
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] + matrix[j, i] == 1)
                        return false;
                    if (i == j && matrix[i, j] == 1)
                        return false;
                }
            }

            return true;
        }

        private static int[,] ReadMatrix(StreamReader input, int n)
        {
            int[,] matrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                string[] stringElements = input.ReadLine().Split();

                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = int.Parse(stringElements[j]);
                }
            }

            return matrix;
        }

        public static void Solve()
        {
            int n;
            int[,] matrix;

            using (var input = new StreamReader("input.txt"))
            {
                n = int.Parse(input.ReadLine());
                matrix = ReadMatrix(input, n);
            }

            bool result = IsOriented(matrix);

            using (var output = new StreamWriter("output.txt"))
            {
                output.WriteLine(result ? "YES" : "NO");
            }
        }
    }
}
