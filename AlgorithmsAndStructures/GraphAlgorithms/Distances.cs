using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class Distances
    {
        private static int[] GetDistances(List<int>[] edgesLists, int start)
        {
            int n = edgesLists.Length;
            int[] distances = new int[n];

            for (int i = 0; i < n; i++)
            {
                distances[i] = -1;
            }

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);
            distances[start] = 0;
            while (queue.Count > 0)
            {
                int v = queue.Dequeue();

                for (int i = 0; i < edgesLists[v].Count; i++)
                {
                    int u = edgesLists[v][i];
                    if (distances[u] == -1)
                    {
                        queue.Enqueue(u);
                        distances[u] = distances[v] + 1;
                    }
                }
            }

            return distances;
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
            (n, m) = ReadTwoNumbers();
            var edgesLists = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                edgesLists[i] = new List<int>();
            }

            int v, u;
            for (int i = 0; i < m; i++)
            {
                (v, u) = ReadTwoNumbers();
                v--;
                u--;
                edgesLists[v].Add(u);
                edgesLists[u].Add(v);
            }


            int[] distances = GetDistances(edgesLists, 0);

            Console.WriteLine(String.Join(" ", distances.Select(e => e.ToString())));
        }
    }
}
