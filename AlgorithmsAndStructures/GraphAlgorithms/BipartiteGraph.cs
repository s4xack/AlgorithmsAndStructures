using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class BipartiteGraph
    {
        private static bool CheckBipartite(HashSet<int>[] edgesLists)
        {
            int n = edgesLists.Length;
            int[] color = new int[n];

            bool Dfs(int v, int c)
            {
                color[v] = c;
                foreach (var u in edgesLists[v])
                {
                    if (color[u] == 0)
                    {
                        if (!Dfs(u, -c)) return false;
                    }
                    else if (color[u] == c)
                    {
                        return false;
                    }
                }

                return true;
            }

            for (int i = 0; i < n; i++)
            {
                if (color[i] == 0)
                {
                    if (!Dfs(i, 1)) return false;
                }
            }

            return true;
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

        private static void Program()
        {
            int n, m;
            HashSet<int>[] edgesLists;

            using (StreamReader input = new StreamReader("bipartite.in")) {
                (n, m) = ReadTwoNumbers(input);

                edgesLists = new HashSet<int>[n];
                for (int i = 0; i < n; i++)
                {
                    edgesLists[i] = new HashSet<int>(); 
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

            bool isBipartite = CheckBipartite(edgesLists);

            using (StreamWriter output = new StreamWriter("bipartite.out"))
            {
                output.WriteLine(isBipartite ? "YES" : "NO");
            }
        }

        public static void Solve()
        {
            var stackSize = 100000000;
            Thread thread = new Thread(new ThreadStart(Program), stackSize);
            thread.Start();
        }
    }
}
