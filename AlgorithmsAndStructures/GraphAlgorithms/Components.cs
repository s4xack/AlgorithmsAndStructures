using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class Components
    {
        private static int[] GetComponents(List<int>[] edgesLists)
        {
            int n = edgesLists.Length;
            int[] components = new int[n];
            int color = 1;
            for (int i = 0; i < n; i++)
            {
                if (components[i] == 0)
                    Bfs(edgesLists, components, i, color++);
            }

            return components;
        }

        private static void Bfs(List<int>[] edgesLists, int[] components, int start, int color)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                int v = queue.Dequeue();
                components[v] = color;

                for (int i = 0; i < edgesLists[v].Count; i++)
                {
                    int u = edgesLists[v][i];
                    if (components[u] == 0) 
                        queue.Enqueue(u);
                }
            }
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
            List<int>[] edgesLists;
            using (var input = new StreamReader("components.in"))
            {
                (n, m) = ReadTwoNumbers(input);
                edgesLists = new List<int>[n];
                for (int i = 0; i < n; i++)
                {
                    edgesLists[i] = new List<int>();
                }

                int v, u;
                for (int i = 0; i < m; i++)
                {
                    (v, u) = ReadTwoNumbers(input);
                    v--;
                    u--;
                    edgesLists[v].Add(u);
                    edgesLists[u].Add(v);
                }
            }

            int[] components = GetComponents(edgesLists);
            int componentCount = components.Max();

            using (var output = new StreamWriter("components.out"))
            {
                output.WriteLine(componentCount);
                output.WriteLine(String.Join(" ", components.Select(e => e.ToString())));
            }
        }
    }
}
