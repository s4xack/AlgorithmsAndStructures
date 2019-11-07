using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace AlgorithmsAndStructures.HashTables
{
    class LinkedMapItem
    {
        public string key;
        public string value;
        public LinkedMapItem prev;
        public LinkedMapItem next;

        public LinkedMapItem(string key, string value, LinkedMapItem prev, LinkedMapItem next = null)
        {
            this.key = key;
            this.value = value;
            this.prev = prev;
            this.next = next;
        }

        public LinkedMapItem()
        {
        }
    }
    class MyLinkedMap
    {
        private const int maxHashSize = 20000003;
        private const int hashHelper = 37; 
        List<LinkedMapItem>[] hashTable = new List<LinkedMapItem>[maxHashSize];

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

        public LinkedMapItem Put(LinkedMapItem item)
        {
            int hashCode = Hash(item.key);
            if (hashTable[hashCode] == null)
                hashTable[hashCode] = new List<LinkedMapItem>();
            if(!hashTable[hashCode].Exists(i => i.key == item.key))
            {
                hashTable[hashCode].Add(item);
                if (item.prev != null)
                {
                    item.prev.next = item;
                }
                return item;
            }
            else
            {
                hashTable[hashCode].Find(i => i.key == item.key).value = item.value;
                return item.prev;
            }
        }


        public LinkedMapItem Get(string key)
        {
            int hashCode = Hash(key);
            if (hashTable[hashCode] != null && hashTable[hashCode].Exists(i => i.key == key))
                return hashTable[hashCode].Find(i => i.key == key);
            return null;
        }

        public LinkedMapItem Delete(string key)
        {
            int hashCode = Hash(key);
            if (hashTable[hashCode] == null || !hashTable[hashCode].Exists(i => i.key == key))
                return null;
            LinkedMapItem item = hashTable[hashCode].Find(i => i.key == key);
            if (item.prev != null)
                item.prev.next = item.next;
            if (item.next != null)
                item.next.prev = item.prev;
            item = hashTable[hashCode].Find(i => i.key == key);
            hashTable[hashCode].RemoveAll(i => i.key == key);
            return item;
        }

        public LinkedMapItem Prev(string key)
        {
            return Get(key)?.prev;
        }

        public LinkedMapItem Next(string key)
        {
            return Get(key)?.next;
        }
    }

    class SolutionLinkedMap
    {
        public static void Solve()
        {
            using (var input = new StreamReader("linkedmap.in"))
            {
                using (var output = new StreamWriter("linkedmap.out"))
                {
                    MyLinkedMap map = new MyLinkedMap();
                    LinkedMapItem lastItem = null;
                    LinkedMapItem deleted = null;
                    while (true)
                    {
                        string[] line = input.ReadLine()?.Trim().Split();
                        if (line == null)
                            break;
                        switch (line[0])
                        {
                            case "put":
                                lastItem = map.Put(new LinkedMapItem(line[1], line[2], lastItem));
                                break;
                            case "delete":
                                deleted = map.Delete(line[1]);
                                if (deleted?.key == lastItem?.key)
                                    lastItem = deleted?.prev;
                                break;
                            case "get":
                                output.WriteLine(map.Get(line[1])?.value ?? "none");
                                break;
                            case "prev":
                                output.WriteLine(map.Prev(line[1])?.value ?? "none");
                                break;
                            case "next":
                                output.WriteLine(map.Next(line[1])?.value ?? "none");
                                break;
                        }
                    }
                }
            }
        }
    }
}
