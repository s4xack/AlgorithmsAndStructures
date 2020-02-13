using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class Labyrinth
    {
        static List<char> FindWay(string[] map, int n, int m, int startI, int startJ, int endI, int endJ)
        {
            char[,] pathLetter = new char[n, m];
            (int, int)[,] pre = new (int, int)[n, m];
            int[,] distances = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    distances[i, j] = -1;
                    pre[i, j] = (-1, -1);
                }
            }

            Queue<(int, int)> queue = new Queue<(int, int)>();
            distances[startI, startJ] = 0;
            pathLetter[startI, startJ] = 'S';
            queue.Enqueue((startI, startJ));
            int curI, curJ;
            while (queue.Count > 0)
            {
                (curI, curJ) = queue.Dequeue();
                if (CanGo(map, n, m, curI - 1, curJ) && distances[curI - 1, curJ] == -1)
                {
                    distances[curI - 1, curJ] = distances[curI, curJ] + 1;
                    pre[curI - 1, curJ] = (curI, curJ);
                    pathLetter[curI - 1, curJ] = 'U';

                    queue.Enqueue((curI - 1, curJ));
                }

                if (CanGo(map, n, m, curI + 1, curJ) && distances[curI + 1, curJ] == -1)
                {
                    distances[curI + 1, curJ] = distances[curI, curJ] + 1;
                    pre[curI + 1, curJ] = (curI, curJ);
                    pathLetter[curI + 1, curJ] = 'D';

                    queue.Enqueue((curI + 1, curJ));
                }

                if (CanGo(map, n, m, curI, curJ - 1) && distances[curI, curJ - 1] == -1)
                {
                    distances[curI, curJ - 1] = distances[curI, curJ] + 1;
                    pre[curI, curJ - 1] = (curI, curJ);
                    pathLetter[curI, curJ - 1] = 'L';

                    queue.Enqueue((curI, curJ - 1));
                }

                if (CanGo(map, n, m, curI, curJ + 1) && distances[curI, curJ + 1] == -1)
                {
                    distances[curI, curJ + 1] = distances[curI, curJ] + 1;
                    pre[curI, curJ + 1] = (curI, curJ);
                    pathLetter[curI, curJ + 1] = 'R';

                    queue.Enqueue((curI, curJ + 1));
                }
            }

            List<char> path = new List<char>();
            curI = endI;
            curJ = endJ;
            while (pathLetter[curI, curJ] != 'S' && pathLetter[curI, curJ] != 0)
            {
                path.Add(pathLetter[curI, curJ]);
                (curI, curJ) = pre[curI, curJ];
            }

            path.Reverse();

            return path;
        }

        static bool IsValidCoordinates(int i, int j, int n, int m)
        {
            return i >= 0 && i < n && j >= 0 && j < m;
        }

        static bool CanGo(string[] map,int n, int m, int i, int j)
        {
            return IsValidCoordinates(i, j, n, m) && map[i][j] != '#';
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
            string[] map;
            using (var input = new StreamReader("input.txt"))
            {
                (n, m) = ReadTwoNumbers(input);
                map = new string[n];
                for (int i = 0; i < n; i++)
                {
                    map[i] = input.ReadLine();
                }
            }

            int startI = -1, startJ = -1;
            int endI = -1, endJ = -1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; ++j)
                {
                    if (map[i][j] == 'S')
                    {
                        startI = i;
                        startJ = j;
                    }
                    if (map[i][j] == 'T')
                    {
                        endI = i;
                        endJ = j;
                    }
                }
            }

            List<char> path = FindWay(map, n, m, startI, startJ, endI, endJ);

            using (var output = new StreamWriter("output.txt"))
            {
                if (path.Count == 0)
                {
                    output.WriteLine(-1);
                    return;
                }
                output.WriteLine(path.Count);
                foreach (var c in path)
                {
                    output.Write(c);
                }
                output.WriteLine();
            }
        }
    }
}
