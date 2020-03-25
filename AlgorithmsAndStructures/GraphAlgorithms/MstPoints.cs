using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class MstPoints
    {
        private static double Prim(List<(int, int)> points)
        {
            const int inf = int.MaxValue;

            int n = points.Count;
            int[,] matrix = CountDistances(points);
            bool[] visited = new bool[n];

            int[] distances = Enumerable.Repeat(inf, n).ToArray();
            distances[0] = 0;

            for (int i = 0; i < n; i++)
            {
                int from = -1;
                for (int j = 0; j < n; j++)
                    if (!visited[j] && (from == -1 || distances[j] < distances[from]))
                        from = j;
                visited[from] = true;
                for (int to = 0; to < n; to++)
                    if (!visited[to] && matrix[from, to] < distances[to] && from != to)
                        distances[to] = matrix[from, to];
            }

            double result = 0;

            foreach (var distance in distances)
            {
                result += Math.Sqrt(distance);
            }

            return result;
        }

        private static int DistanceBetween((int, int) from, (int, int) to)
        {
            return (from.Item1 - to.Item1) * (from.Item1 - to.Item1) + (from.Item2 - to.Item2) * (from.Item2 - to.Item2);
        }

        private static int[,] CountDistances(List<(int, int)> points)
        {
            int n = points.Count;
            int[,] distances = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    distances[i, j] = DistanceBetween(points[i], points[j]);
                }
            }

            return distances;
        }

        private static void Program()
        {
            int n;
            List <(int, int)> points = new List<(int, int)>();

            n = int.Parse(Console.ReadLine());
            int x, y;
            string[] line;
            for (int i = 0; i < n; i++)
            {
                line = Console.ReadLine().Split();
                x = int.Parse(line[0]);
                y = int.Parse(line[1]);
                points.Add((x, y));
            }

            double result = Prim(points);
            Console.WriteLine(result.ToString("0.0000000000", System.Globalization.CultureInfo.InvariantCulture));
        }

        public static void Solve()
        {
            var stackSize = 100000000;
            Thread thread = new Thread(new ThreadStart(Program), stackSize);
            thread.Start();
        }
    }
}
