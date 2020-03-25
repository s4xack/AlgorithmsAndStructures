using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class VertexDegree
    {
        private static int[] GetVertexDegrees(int vertexCount, IEnumerable<(int, int)> edgesList)
        {
            int[] vertexDegrees = new int[vertexCount];
            foreach (var edge in edgesList)
            {
                vertexDegrees[edge.Item1]++;
                vertexDegrees[edge.Item2]++;
            }
            return vertexDegrees;
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

        private static (int, int) ReadTwoNumbers()
        {
            int a, b;
            string inputLine = Console.ReadLine();
            string[] inputStrings = inputLine.Split();
            a = int.Parse(inputStrings[0]);
            b = int.Parse(inputStrings[1]);

            return (a, b);
        }

        public static void Solve()
        {
            int n, m;
            List<(int, int)> edgesList = new List<(int, int)>();
            using (var input = new StreamReader("input.txt"))
            {
                (n, m) = ReadTwoNumbers(input);
                int v, u;
                for (int i = 0; i < m; i++)
                {
                    (v, u) = ReadTwoNumbers(input);
                    edgesList.Add((v - 1, u - 1));
                }
            }

            int[] vertexDegrees = GetVertexDegrees(n, edgesList);

            using (var output = new StreamWriter("output.txt"))
            {
                output.WriteLine(string.Join(" ", vertexDegrees.Select(d => d.ToString()).ToArray()));
            }
        }
    }
}
