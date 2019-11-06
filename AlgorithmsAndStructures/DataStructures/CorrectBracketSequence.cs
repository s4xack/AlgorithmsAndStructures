using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlgorithmsAndStructures.DataStructures
{
    class CorrectBracketSequence
    {
        class Stack
        {
            private char[] data;
            private int size;

            public Stack()
            {
                data = new char[1000000];
                size = 0;
            }

            public int Size()
            {
                return size;
            }

            public bool IsEmpty()
            {
                return Size() == 0;
            }

            public void Push(char element)
            {
                data[size++] = element;
            }

            public char Top()
            {
                return data[size - 1];
            }

            public char Pop()
            {
                return data[--size];
            }
        }

        private static bool CheckSequence(string sequence)
        {
            Stack stack = new Stack();
            for (int i = 0; i < sequence.Length; ++i)
            {
                if (sequence[i] == '(' || sequence[i] == '[')
                {
                    stack.Push(sequence[i]);
                }
                else if (!stack.IsEmpty())
                {
                    if (sequence[i] == ')' && stack.Top() == '(')
                    {
                        stack.Pop();
                    }
                    else if(sequence[i] == ']' && stack.Top() == '[')
                    {
                        stack.Pop();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            if (stack.IsEmpty())
            {
                return true;
            }

            return false;
        }

        public static void Solve()
        {
            string[] sequences = File.ReadAllLines("brackets.in");
            string answer = string.Empty;
            for (int i = 0; i < sequences.Length; ++i)
            {
                string sequence = sequences[i];
                if (CheckSequence(sequence))
                {
                    answer += "YES\n";
                }
                else
                {
                    answer += "NO\n";
                }
            }
            File.WriteAllText("brackets.out", answer);
        }
    }
}
