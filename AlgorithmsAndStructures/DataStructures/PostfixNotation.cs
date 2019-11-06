using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlgorithmsAndStructures.DataStructures
{
    class PostfixNotation
    {
        class Stack
        {
            private int[] data;
            private int size;

            public Stack()
            {
                data = new int[1000000];
                size = 0;
            }

            public int Size()
            {
                return size;
            }

            public void Push(int element)
            {
                data[size++] = element;
            }

            public int Top()
            {
                return data[size - 1];
            }

            public int Pop()
            {
                return data[--size];
            }
        }

        public static void Solve()
        {
            string[] sequence = File.ReadAllText("postfix.in").Split();
            Stack stack = new Stack();

            for (int i = 0; i < sequence.Length; ++i)
            {
                string element = sequence[i];
                if (element == "")
                    continue;
                int a, b;
                switch (element)
                {
                    case "+":
                        b = stack.Pop();
                        a = stack.Pop();
                        stack.Push(a + b);
                        break;
                    case "-":
                        b = stack.Pop();
                        a = stack.Pop();
                        stack.Push(a - b);
                        break;
                    case "*": 
                        b = stack.Pop();
                        a = stack.Pop();
                        stack.Push(a * b);
                        break;
                    default:
                        stack.Push(int.Parse(element));
                        break;
                }
            }

            int result = stack.Top();
            File.WriteAllText("postfix.out", result.ToString());
        }
    }
}
