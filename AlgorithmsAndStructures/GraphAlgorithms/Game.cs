using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class Game
    {
        private static bool CheckFirstWin(int s, HashSet<int>[] edgeslists)
        {
            int n = edgeslists.Length;
            bool?[] winHere = new bool?[n];

            void Dfs(int v)
            {
                foreach (var u in edgeslists[v])
                {
                    if (winHere[u] == null)
                        Dfs(u);
                }

                if (edgeslists[v].Count == 0)
                {
                    winHere[v] = false;
                    return;
                }

                winHere[v] = edgeslists[v].Select((e) => !winHere[e]).Aggregate((a, b) => a | b);
            }

            Dfs(s);

            return winHere[s].Value;
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

        private static (int, int, int) ReadThreeNumbers()
        {
            int a, b, c;
            string inputLine = Console.ReadLine();
            string[] inputStrings = inputLine.Split();
            a = int.Parse(inputStrings[0]);
            b = int.Parse(inputStrings[1]);
            c = int.Parse(inputStrings[2]);

            return (a, b, c);
        }

        private static void Program()
        {
            int n, m, s;

            (n, m, s) = ReadThreeNumbers();
            s--;

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

            bool isHamiltonian = CheckFirstWin(s, edgesLists);
            Console.WriteLine(isHamiltonian ? "First player wins" : "Second player wins");
        }

        public static void Solve()
        {
            var stackSize = 100000000;
            Thread thread = new Thread(new ThreadStart(Program), stackSize);
            thread.Start();
        }
    }
}
