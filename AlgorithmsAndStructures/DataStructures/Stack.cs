using System;
using System.IO;

namespace AlgorithmsAndStructures.DataStructures
{
    class Stack
    {
        private Int64[] data;
        private int size;

        public Stack()
        {
            data = new Int64[1000000];
            size = 0;
        }

        public int Size()
        {
            return size;
        }

        public void Add(Int64 element)
        {
            data[size++] = element;
        }

        public Int64 Top()
        {
            return data[size - 1];
        }

        public Int64 Pop()
        {
            return data[--size];
        }
    }

    class SolutionStack
    {
        public static void Solve()
        {
            int commandCount;
            using (var inputFile = new StreamReader("stack.in"))
            {
                commandCount = Int32.Parse(inputFile.ReadLine());

                Stack stack = new Stack();
                using (var outputFile = new StreamWriter("stack.out"))
                {
                    for (int i = 0; i < commandCount; ++i)
                    {
                        string[] commands = inputFile.ReadLine().Split();
                        if (commands[0] == "+")
                        {
                            Int64 newElement = Int64.Parse(commands[1]);
                            stack.Add(newElement);
                        }
                        else
                        {
                            outputFile.WriteLine(stack.Pop());
                        }
                    }
                }
            }
        }
    }
}
