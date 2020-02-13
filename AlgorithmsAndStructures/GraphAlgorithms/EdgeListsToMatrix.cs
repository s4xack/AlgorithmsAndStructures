using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class EdgeListsToMatrix
    {
        private static bool[,] GetMatrix(List<int>[] edgeLists)
        {
            int n = edgeLists.Length;
            bool[,] matrix = new bool[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < edgeLists[i].Count; j++)
                {
                    matrix[i, edgeLists[i][j]] = true;
                }
            }

            return matrix;
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

        private static void WriteMatrix(StreamWriter output, bool[,] matrix)
        {
            int n = (int)(Math.Sqrt(matrix.Length));
            int e;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    e = matrix[i, j] ? 1 : 0;
                    output.Write($"{e} ");
                }
                output.Write("\n");
            }
        }

        public static void Solve()
        {
            int n, m;
            List<int>[] edgeLists;
            using (var input = new StreamReader("input.txt"))
            {
                (n, m) = ReadTwoNumbers(input);
                edgeLists = new List<int>[n];
                for (int i = 0; i < n; i++)
                {
                    edgeLists[i] = new List<int>();
                }

                int v, u;
                for (int i = 0; i < m; i++)
                {
                    (v, u) = ReadTwoNumbers(input);
                    v--;
                    u--;
                    edgeLists[v].Add(u);
                }
            }

            bool[,] matrix = GetMatrix(edgeLists);

            using (var output = new StreamWriter("output.txt"))
            {
                WriteMatrix(output, matrix);
            }
        }
    }
}
