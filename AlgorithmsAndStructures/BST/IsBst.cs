using System.Collections.Generic;
using System.IO;

namespace AlgorithmsAndStructures.BST
{
    class IsBst
    {
        class Node
        {
            public long Value;
            public int LeftChild;
            public int RightChild;

            public Node(long value, int left, int right)
            {
                Value = value;
                LeftChild = left;
                RightChild = right;
            }
        }

        class CheckBstParams
        {
            public long Min;
            public long Max;
            public int Index;

            public CheckBstParams(int index, long min, long max)
            {
                Index = index;
                Min = min;
                Max = max;
            }
        }
        private static bool CheckBst(Node[] tree)
        {
            if (tree.Length == 1)
                return true;
            Stack<CheckBstParams> stack = new Stack<CheckBstParams>();
            stack.Push(new CheckBstParams(1, long.MinValue, long.MaxValue));
            while (stack.Count > 0)
            {
                CheckBstParams x = stack.Pop();
                if (tree[x.Index].Value <= x.Min || tree[x.Index].Value >= x.Max)
                    return false;
                if (tree[x.Index].LeftChild != 0)
                    stack.Push(new CheckBstParams(tree[x.Index].LeftChild, x.Min, tree[x.Index].Value));
                if (tree[x.Index].RightChild != 0)
                    stack.Push(new CheckBstParams(tree[x.Index].RightChild, tree[x.Index].Value, x.Max));
            }

            return true;
        }

        public static void Solve()
        {
            using (var input = new StreamReader("check.in"))
            {
                using (var output = new StreamWriter("check.out"))
                {
                    int n = int.Parse(input.ReadLine());
                    Node[] tree = new Node[n + 1];
                    for (int i = 1; i <= n; ++i)
                    {
                        string[] line = input.ReadLine().Trim().Split();
                        long value = long.Parse(line[0]);
                        int left = int.Parse(line[1]);
                        int right = int.Parse(line[2]);
                        tree[i] = new Node(value, left, right);
                    }

                    string result = CheckBst(tree) ? "YES" : "NO";
                    output.WriteLine(result);
                }
            }
        }
    }
}