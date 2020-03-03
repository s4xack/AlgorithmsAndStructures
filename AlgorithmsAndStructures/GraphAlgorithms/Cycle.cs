using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class Cycle
    {
        private static List<int> GetCycle(HashSet<int>[] edgesLists)
        {
            int n = edgesLists.Length;
            int[] used = new int[n];
            int[] parent = new int[n];

            List<int> cycle = new List<int>();

            (int, int)? result = null;
            for (int i = 0; i < n; i++)
            {
                if (used[i] == 0)
                {
                    result = Dfs(i, edgesLists, used, parent) ?? result;
                    if (result != null)
                    {
                        break;
                    }
                }
            }

            if (result == null)
                return null;

            var (u, v) = result.Value;
            while (v != u)
            {
                cycle.Add(v);
                v = parent[v];
            }
            cycle.Add(u);
            cycle.Reverse();

            return cycle;
        }

        private static (int, int)? Dfs(int v, HashSet<int>[] edgesLists, int[] used, int[] parent)
        {
            used[v] = 1;

            foreach (var u in edgesLists[v])
            {
                if (used[u] == 0)
                {
                    parent[u] = v;
                    var result = Dfs(u, edgesLists, used, parent);
                    if (result != null)
                        return result;
                }
                else if (used[u] == 1)
                {
                    return (u, v);
                }
            }

            used[v] = 2;
            return null;
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

            (n, m) = ReadTwoNumbers();

            var edgesLists = new HashSet<int>[n];
            for (int i = 0; i < n; i++)
            {
                edgesLists[i] = new HashSet<int>();
            }
            int v, u;
            for (int i = 0; i < m; i++)
            {
                (v, u) = ReadTwoNumbers();
                v--;
                u--;
                edgesLists[v].Add(u);
            }

            List<int> cycle = GetCycle(edgesLists);
            if (cycle == null)
            {
                Console.WriteLine("NO");
            }
            else
            {
                Console.WriteLine("YES");
                Console.WriteLine(String.Join(" ", cycle.Select(e => (e + 1).ToString()).ToArray()));
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
