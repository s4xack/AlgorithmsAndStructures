using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class HamiltonianPath
    {
        private static bool CheckHamiltonianPath(HashSet<int>[] edgesLists, HashSet<int>[] parents)
        {
            int n = edgesLists.Length;
            HashSet<int> deleted = new HashSet<int>();

            bool IsSource(int v)
            {
                bool result = parents[v].Select((u) => !deleted.Contains(u))
                                        .Aggregate(false, (a, b) => a | b);

                return !result;
            }

            int? FindOnlySource(int v)
            {
                var result = edgesLists[v].Where((u) => IsSource(u)).ToList();
                if (result.Count != 1)
                    return null;
                return result[0];
            }

            int? source = null;
            for (int i = 0; i < n; ++i)
            {
                if (IsSource(i))
                {
                    if (source != null)
                        return false;
                    source = i;
                }
            }
            if (source == null)
                return false;
            
            deleted.Add(source.Value);

            while (deleted.Count < n)
            {
                source = FindOnlySource(source.Value);
                if (source == null)
                    return false;
                deleted.Add(source.Value);
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

            (n, m) = ReadTwoNumbers();

            var edgesLists = new HashSet<int>[n];
            var parents = new HashSet<int>[n];
            for (int i = 0; i < n; i++)
            {
                edgesLists[i] = new HashSet<int>();
                parents[i] = new HashSet<int>();
            }
            int v, u;
            for (int i = 0; i < m; i++)
            {
                (v, u) = ReadTwoNumbers();
                v--;
                u--;
                edgesLists[v].Add(u);
                parents[u].Add(v);
            }

            bool isHamiltonian = CheckHamiltonianPath(edgesLists, parents);
            Console.WriteLine(isHamiltonian ? "YES" : "NO");
        }

        public static void Solve()
        {
            var stackSize = 100000000;
            Thread thread = new Thread(new ThreadStart(Program), stackSize);
            thread.Start();
        }
    }
}
