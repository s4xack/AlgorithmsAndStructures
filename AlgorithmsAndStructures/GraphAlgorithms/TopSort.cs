using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class TopSort
    {
        private static List<int> GetTopSort(HashSet<int>[] edgesLists)
        {
            int n = edgesLists.Length;
            int[] used = new int[n];

            List<int> topSort = new List<int>();

            for (int i = 0; i < n; i++)
            {
                if (used[i] == 0)
                {
                    if (!Dfs(i, edgesLists, used, topSort))
                        return null;
                }
            }

            topSort.Reverse();
            return topSort;
        }

        private static bool Dfs(int v, HashSet<int>[] edgesLists, int[] used, List<int> topSort)
        {
            used[v] = 1;
            foreach (var u in edgesLists[v])
            {
                if (used[u] == 0)
                {
                    if (!Dfs(u, edgesLists, used, topSort)) return false;
                }
                else if (used[u] == 1)
                {
                    return false;
                }
            }
            used[v] = 2;
            topSort.Add(v);
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

            List<int> topSort = GetTopSort(edgesLists);
            if (topSort == null)
            {
                Console.WriteLine(-1);
            }
            else
            {
                Console.WriteLine(String.Join(" ", topSort.Select(e => (e + 1).ToString()).ToArray()));
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
