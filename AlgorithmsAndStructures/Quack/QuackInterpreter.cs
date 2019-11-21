using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlgorithmsAndStructures.Quack
{
    class QuackInterpreter
    {
        private Queue<UInt16> queue;
        private Dictionary<string, int> labels;
        private Dictionary<char, UInt16> registers;
        private readonly List<string> commands;

        public QuackInterpreter(List<string> commands)
        {
            queue = new Queue<UInt16>();
            labels = new Dictionary<string, int>();
            registers = new Dictionary<char, UInt16>();
            for (int i = 0; i < 26; ++i)
            {
                registers[(char) ('a' + i)] = 0;
            }
            this.commands = commands;
            for (int i = 0; i < commands.Count; ++i)
            {
                string command = commands[i];
                if (command[0] == ':')
                    labels[command.Substring(1)] = i + 1;
            }

            
        }
        public void Start()
        {
            int i = 0;
            while (i < commands.Count)
            {
                string command = commands[i];
                UInt16 a;
                UInt16 b;
                command = command.Trim();
                if (command == string.Empty)
                    continue;
                switch (command[0])
                {
                    case '+':
                        a = queue.Dequeue();
                        b = queue.Dequeue();
                        queue.Enqueue((UInt16)((a + b)));

                        i++;
                        break;
                    case '-':
                        a = queue.Dequeue();
                        b = queue.Dequeue();
                        queue.Enqueue((UInt16)((a - b)));

                        i++;
                        break;
                    case '*':
                        a = queue.Dequeue();
                        b = queue.Dequeue();
                        queue.Enqueue((UInt16) (a * b));

                        i++;
                        break;
                    case '/':
                        a = queue.Dequeue();
                        b = queue.Dequeue();
                        queue.Enqueue((UInt16) (b == 0 ? 0 : a / b));

                        i++;
                        break;
                    case '%':
                        a = queue.Dequeue();
                        b = queue.Dequeue();
                        queue.Enqueue((UInt16) (b == 0 ? 0 : a % b));

                        i++;
                        break;
                    case '>':
                        registers[command[1]] = queue.Dequeue();

                        i++;
                        break;
                    case '<':
                        queue.Enqueue(registers[command[1]]);

                        i++;
                        break;
                    case 'P':
                        if (command.Length == 1)
                        {
                            Console.WriteLine(queue.Dequeue().ToString());
                        }
                        else
                        {
                            Console.WriteLine(registers[command[1]]);
                        }

                        i++;
                        break;
                    case 'C':
                        if (command.Length == 1)
                        {
                            Console.Write((char) (queue.Dequeue() % 256));
                        }
                        else
                        {
                            Console.Write((char) (registers[command[1]] % 256));
                        }

                        i++;
                        break;
                    case 'J':
                        i = labels[command.Substring(1)];
                        break;
                    case 'Z':
                        if (registers[command[1]] == 0)
                            i = labels[command.Substring(2)];
                        else
                            i++;
                        break;
                    case 'E':
                        if (registers[command[1]] == registers[command[2]])
                            i = labels[command.Substring(3)];
                        else
                            i++;
                        break;
                    case 'G':
                        if (registers[command[1]] > registers[command[2]])
                            i = labels[command.Substring(3)];
                        else
                            i++;
                        break;
                    case 'Q':
                        return;
                    case ':':
                        i++;
                        break;
                    default:
                        if (UInt16.TryParse(command, out a))
                            queue.Enqueue(a);
                        i++;
                        break;
                }
            }
        }
    }

    class SolutionQuack
    {
        public static void Main()
        {
            List<string> commands = new List<string>();
            while (true)
            {
                string inp = Console.ReadLine();
                if (inp == null)
                    break;
                commands.Add(inp);
            }
            QuackInterpreter qi = new QuackInterpreter(commands);
            
            qi.Start();
            
        }
    }
}
