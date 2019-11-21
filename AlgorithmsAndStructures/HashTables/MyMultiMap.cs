﻿using System;
using System.Collections.Generic;
using System.IO;
 using System.Text.RegularExpressions;


 namespace AlgorithmsAndStructures.HashTables
{
     
    class LinkedSetItem
    {
        public readonly string Value;
        public LinkedSetItem Prev;
        public LinkedSetItem Next;
 
        public LinkedSetItem(string value, LinkedSetItem prev, LinkedSetItem next = null)
        {
            Value = value;
            Prev = prev;
            Next = next;
        }
    }
    class LinkedSet
    {
        private const int MaxHashSize = 107;
        private const int HashHelper = 37;
 
        public readonly string Key;
        private List<LinkedSetItem>[] Elements;
        public LinkedSetItem Begin;
        private LinkedSetItem _last;
        public int Size;
 
        public LinkedSet(string key)
        {
            this.Key = key;
            this.Elements = new List<LinkedSetItem>[MaxHashSize];
            Begin = null;
            _last = null;
            Size = 0;
        }
 
        public static int Hash(string value)
        {
            Int64 hashCode = 0;
            Int64 helper = 1;
            foreach (var ch in value.ToLower())
            {
                hashCode += (Int64)(ch - 'a') * helper % MaxHashSize;
                hashCode %= MaxHashSize;
                helper *= HashHelper;
                helper %= MaxHashSize;
            }
            return Math.Abs((int)hashCode);
        }
 
        public void DeleteAll()
        {
            Elements = new List<LinkedSetItem>[MaxHashSize];
            _last = null;
            Begin = null;
            Size = 0;
        }
 
        public void Push(string value)
        {
            int hashCode = Hash(value);
            // Console.WriteLine(value);
            LinkedSetItem item = new LinkedSetItem(value, _last);
            if (Elements[hashCode] == null)
                Elements[hashCode] = new List<LinkedSetItem>();
            if (!Elements[hashCode].Exists(i => i.Value == item.Value))
            {
                Elements[hashCode].Add(item);
                Size++;
                if (_last != null)
                {
                    _last.Next = item;
                }
 
                _last = item;
 
                if (Begin == null)
                {
                    Begin = item;
                }
            }
             
        }
 
        public void Delete(string value)
        {
            try
            {
                int hashCode = Hash(value);
                if (Elements[hashCode].Exists(i => i.Value == value))
                {
                    LinkedSetItem item = Elements[hashCode].Find(i => i.Value == value);
                    Size--;
                    Elements[hashCode].RemoveAll(i => i.Value == value);
                    if (item.Prev != null)
                    {
                        item.Prev.Next = item.Next;
                    }

                    if (item.Next != null)
                    {
                        item.Next.Prev = item.Prev;
                    }

                    if (item.Value == _last?.Value)
                    {
                        _last = item.Prev;
                    }

                    if (item.Value == Begin?.Value)
                    {
                        Begin = item.Next;
                    }
                }
            }
            catch
            {

            }
        }
    }
     
    class MyMultiMap
    {
        private const int MaxHashSize = 107;
        private const int HashHelper = 37;
        readonly List<LinkedSet>[] _hashTable = new List<LinkedSet>[MaxHashSize];

        private static int Hash(string value)
        {
            Int64 hashCode = 0;
            Int64 helper = 1;
            foreach (var ch in value.ToLower())
            {
                hashCode += (Int64)(ch - 'a') * helper % MaxHashSize;
                hashCode %= MaxHashSize;
                helper *= HashHelper;
                helper %= MaxHashSize;
            }
            return Math.Abs((int)hashCode);
        }
 
        public void Put((string, string) item)
        {
            int hashCode = Hash(item.Item1);
            if (_hashTable[hashCode] == null)
                _hashTable[hashCode] = new List<LinkedSet>();
            if (!_hashTable[hashCode].Exists(i => i.Key == item.Item1))
            {
                _hashTable[hashCode].Add(new LinkedSet(item.Item1));
            }
            _hashTable[hashCode].Find(i => i.Key == item.Item1).Push(item.Item2);
        }
 
        public void Delete((string, string) item)
        {
            int hashCode = Hash(item.Item1);
            if (_hashTable[hashCode] == null)
                return;
            if (!_hashTable[hashCode].Exists(i => i.Key == item.Item1))
                return;
            _hashTable[hashCode].Find(i => i.Key == item.Item1).Delete(item.Item2);
        }
 
        public void DeleteAll(string key)
        {
            int hashCode = Hash(key);
            if (_hashTable[hashCode] == null)
                return;
            if (!_hashTable[hashCode].Exists(i => i.Key == key))
                return;
            _hashTable[hashCode].Find(i => i.Key == key).DeleteAll();
        }
 
        public LinkedSet Get(string key)
        {
            int hashCode = Hash(key);
            if (_hashTable[hashCode] == null)
                return null;
            if (!_hashTable[hashCode].Exists(i => i.Key == key))
                return null;
            LinkedSet res = _hashTable[hashCode].Find(i => i.Key == key);
            return res;
        }
    }
    class SolutionMultiMap
    {
        public static void Solve()
        {
            using (var input = new StreamReader("multimap.in"))
            {
                using (var output = new StreamWriter("multimap.out"))
                {
                    MyMultiMap map = new MyMultiMap();
                    while (true)
                    {
                        string inp = input.ReadLine()?.Trim();
                        if (null == inp)
                            break;
                        string[] line = Regex.Replace(inp, @"\s+", " ").Split();

                        switch (line[0])
                        {

                            case "put":
                                map.Put((line[1], line[2]));
                                break;
                            case "delete":
                                map.Delete((line[1], line[2]));
                                break;
                            case "deleteall":
                                map.DeleteAll(line[1]);
                                break;
                            case "get":
                                LinkedSet res = map.Get(line[1]);
                                if (res == null)
                                {
                                    output.WriteLine('0');
                                }
                                else
                                {
                                    output.Write(res.Size);
                                    output.Write(" ");
                                    LinkedSetItem el = res.Begin;
                                    while (el != null)
                                    {
                                        output.Write(el.Value);
                                        output.Write(" ");
                                        el = el.Next;
                                    }

                                    output.WriteLine();
                                }

                                break;
                        }
                    }
                }
            }
        }
    }
}