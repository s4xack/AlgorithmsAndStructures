using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace AlgorithmsAndStructures.HashTables
{
    class MyMap
    {
        private const int maxHashSize = 20000003;
        private const int hashHelper = 37;
        List<(string, string)>[] hashTable = new List<(string, string)>[maxHashSize];

        public static int Hash(string value)
        {
            Int64 hashCode = 0;
            Int64 helper = 1;
            foreach (var ch in value.ToLower())
            {
                hashCode += (Int64)(ch - 'a') * helper % maxHashSize;
                hashCode %= maxHashSize;
                helper *= hashHelper;
                helper %= maxHashSize;
            }
            return (int)hashCode;
        }

        public void Put((string, string) value)
        {
            int hashCode = Hash(value.Item1);
            if (hashTable[hashCode] == null)
                hashTable[hashCode] = new List<(string, string)>();
            hashTable[hashCode].RemoveAll(v => v.Item1 == value.Item1);
            hashTable[hashCode].Add(value);
        }

        public string Get(string key)
        {
            int hashCode = Hash(key);
            if (hashTable[hashCode] != null && hashTable[hashCode].Exists(v => v.Item1 == key))
                return hashTable[hashCode].Find(v => v.Item1 == key).Item2;
            return "none";
        }

        public void Delete(string key)
        {
            int hashCode = Hash(key);
            if (hashTable[hashCode] != null)
            {
                hashTable[hashCode].RemoveAll(v => v.Item1 == key);
            }
        }
    }

    class SolutionMap
    {
        public static void Solve()
        {
            using (var input = new StreamReader("map.in"))
            {
                using (var output = new StreamWriter("map.out"))
                {
                    MyMap map = new MyMap();
                    while(true)
                    { 
                        string[] line = input.ReadLine()?.Trim().Split();
                        if (line == null)
                            break;
                        switch (line[0])
                        {
                            case "put":
                                map.Put((line[1], line[2]));
                                break;
                            case "delete":
                                map.Delete(line[1]);
                                break;
                            case "get":
                                output.WriteLine(map.Get(line[1]));
                                break;
                        }
                    }
                }
            }
        }
    }
}
