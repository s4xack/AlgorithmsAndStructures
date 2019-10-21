using System;
using System.IO;

namespace AlgorithmsAndStructures.DataStructures
{
    class Queue
    {
        const int maxDataSize = 1000000;
        private Int64[] data;
        private int begin, end;

        public Queue()
        { 
            data = new Int64[maxDataSize];
            begin = 0;
            end = 0;
        }

        public int Size()
        {
            if (begin < end)
            {
                return end - begin;
            }
            else
            {
                return maxDataSize - begin + end;
            }
        }

        public void Add(Int64 element)
        {
            data[end] = element;
            end = (end + 1) % maxDataSize;
        }

        public Int64 Front()
        {
            return data[begin];
        }

        public Int64 Pop()
        {
            Int64 deleted = data[begin];
            begin = (begin + 1) % maxDataSize;
            return deleted;
        }
    }

    class SolutionQueue
    {
        public static void Solve()
        {
            int commandCount;
            using (var inputFile = new StreamReader("queue.in"))
            {
                commandCount = Int32.Parse(inputFile.ReadLine());

                Queue queue = new Queue();
                using (var outputFile = new StreamWriter("queue.out"))
                {
                    for (int i = 0; i < commandCount; ++i)
                    {
                        string[] commands = inputFile.ReadLine().Split();
                        if (commands[0] == "+")
                        {
                            Int64 newElement = Int64.Parse(commands[1]);
                            queue.Add(newElement);
                        }
                        else
                        {
                            outputFile.WriteLine(queue.Pop());
                        }
                    }
                }
            }
        }
    }
}
