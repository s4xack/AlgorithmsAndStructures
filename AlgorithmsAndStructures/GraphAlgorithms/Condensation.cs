using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class Condensation
    {
        private static (int, int[]) GetCondensation(List<int>[] edgesLists, List<int>[] edgesListsT)
        {
            int n = edgesLists.Length;
            bool[] used = new bool[n];
            int[] condensation = new int[n];
            List<int> order = GetTopSort(edgesLists);

            int componentNumber = 1;
            void Dfs(int v)
            {
                used[v] = true;
                condensation[v] = componentNumber;
                foreach (var u in edgesListsT[v])
                {
                    if (!used[u])
                        Dfs(u);
                }
            }

            foreach (var i in order)
            {
                if(!used[i])
                {
                    Dfs(i);
                    componentNumber++;
                }
            }

            return (componentNumber - 1, condensation);
        }

        private static List<int> GetTopSort(List<int>[] edgesLists)
        {
            int n = edgesLists.Length;
            bool[] used = new bool[n];
            List<int> topSort = new List<int>();

            void Dfs(int v)
            {
                used[v] = true;
                foreach (var u in edgesLists[v])
                {
                    if (!used[u])
                        Dfs(u);
                }
                topSort.Add(v);
            }

            for (int i = 0; i < n; i++)
            {
                if (!used[i])
                    Dfs(i);
            }

            topSort.Reverse();
            return topSort;
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

            var edgesLists = new List<int>[n];
            var edgesListsT = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                edgesLists[i] = new List<int>();
                edgesListsT[i] = new List<int>();
            }
            int v, u;
            for (int i = 0; i < m; i++)
            {
                (v, u) = ReadTwoNumbers();
                v--;
                u--;
                edgesLists[v].Add(u);
                edgesListsT[u].Add(v);
            }

            var (k, condensation) = GetCondensation(edgesLists, edgesListsT);
            Console.WriteLine(k);
            Console.WriteLine(String.Join(" ", condensation.Select(e => e.ToString()).ToArray()));
        }

        public static void Solve()
        {
            var stackSize = 100000000;
            Thread thread = new Thread(new ThreadStart(Program), stackSize);
            thread.Start();
        }
    }
}
