using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AlgorithmsAndStructures.HashTables
{
    class MySet
    {
        List<Int64>[] hashTable = new List<Int64>[1000001];

        private static int Hash(Int64 element)
        {
            int hashCode = Math.Abs((int)(element % 1000001));
            return hashCode;
        }

        public void Insert(Int64 element)
        {
            int hashCode = Hash(element);
            if (hashTable[hashCode] == null)
                hashTable[hashCode] = new List<Int64>();
            if (!hashTable[hashCode].Contains(element))
                hashTable[hashCode].Add(element);
        }

        public bool Exists(Int64 element)
        {
            int hashCode = Hash(element);
            return hashTable[hashCode]?.Contains(element) ?? false;
        }

        public void Delete(Int64 element)
        {
            int hashCode = Hash(element);
            hashTable[hashCode]?.Remove(element);
        }
    }

    class SolutionMySet
    {
        public static void Solve()
        {
            using (var input = new StreamReader("set.in"))
            {
                using (var output = new StreamWriter("set.out"))
                {
                    MySet set = new MySet();
                    while (true)
                    {
                        string inp = input.ReadLine()?.Trim();
                        if (inp == null)
                            break;
                        inp = Regex.Replace(inp, @"\s+", " ");
                        string[] line = inp.Split();
                        Int64 element = Int64.Parse(line[1]);
                        switch (line[0])
                        {
                            case "insert":
                                set.Insert(element);
                                break;
                            case "exists":
                                output.WriteLine(set.Exists(element) ? "true" : "false");
                                break;
                            case "delete":
                                set.Delete(element);
                                break;
                        }
                    }
                }
            }
        }
    }
}
