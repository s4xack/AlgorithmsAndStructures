using System;
using System.Collections.Generic;
using System.IO;

namespace AlgorithmsAndStructures.BST
{
    class BstHeight
    {
        private static int CountHeight((int, int)[] tree)
        {
            int maxHeight = 0;
            Stack<(int, int)> stack = new Stack<(int, int)>();
            stack.Push((1, 0));
            while (stack.Count > 0)
            {
                var (i, height) = stack.Pop();
                var (left, right) = tree[i];
                maxHeight = Math.Max(maxHeight, height + 1);
                if (left != 0)
                    stack.Push((left, height + 1));
                if (right != 0)
                    stack.Push((right, height + 1));
            }

            return maxHeight;
        }

        public static void Solve()
        {
            int n;
            (int, int)[] tree;
            using (var input = new StreamReader("height.in"))
            {
                n = int.Parse(input.ReadLine());
                tree = new (int, int)[n + 1];
                for (int i = 1; i <= n; ++i)
                {
                    string[] line = input.ReadLine().Trim().Split();
                    tree[i] = (int.Parse(line[1]), int.Parse(line[2]));
                    // Console.WriteLine($"{tree[i].Item1}, {tree[i].Item2}");
                }
            }

            int height = 0;
            if (n > 0)
                height = CountHeight(tree);
            using (var output = new StreamWriter("height.out"))
            {
                output.WriteLine(height);
                // Console.WriteLine(height);
            }
        }
    }
}
