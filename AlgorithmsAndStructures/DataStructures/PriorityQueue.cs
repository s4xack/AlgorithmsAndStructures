using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlgorithmsAndStructures.DataStructures
{
    class PriorityQueue
    {
        private const int maxDataSize = 1000000;
        private (Int64, int)[] data { get; set; }
        private int size;

        public PriorityQueue()
        {
            data = new (Int64, int)[maxDataSize];
        }

        public int Size()
        {
            return size;
        }

        public void Print()
        {
            for (int i = 0; i < size; ++i)
            {
                Console.Write(data[i]);
                Console.Write(" ");
            }
            Console.WriteLine();
        }

        private static void Swap<template>(ref template a, ref template b)
        {
            template temp = a;
            a = b;
            b = temp;
        }

        private void SiftDown(int parent)
        {
            while (2 * parent + 1 < size)
            {
                int leftSon = 2 * parent + 1;
                int rightSon = 2 * parent + 2;
                int biggestSon = leftSon;

                if (rightSon < size && data[rightSon].Item1 < data[leftSon].Item1)
                    biggestSon = rightSon;

                if (data[parent].Item1 <= data[biggestSon].Item1)
                    break;

                Swap(ref data[parent], ref data[biggestSon]);
                parent = biggestSon;
            }
        }

        private void SiftUp(int son)
        {
            while (data[son].Item1 < data[(son - 1) / 2].Item1)
            {
                Swap(ref data[son], ref data[(son - 1) / 2]);
                son = (son - 1) / 2;
            }
        }

        public void Add(Int64 newElement, int id)
        {
            data[size] = (newElement, id);
            SiftUp(size++);
        }

        public Int64 ExtractMin()
        {
            Int64 result = data[0].Item1;
            if (size > 1)
                Swap(ref data[0], ref data[size - 1]);
            size--;
            if (size > 0)
                SiftDown(0);
            return result;
        }

        public int FindIndex(int id)
        {
            for (int i = 0; i < size; ++i)
            {
                if (id == data[i].Item2)
                    return i;
            }

            return -1;
        }

        public void DecreaseElement(Int64 newElement, int id)
        {
            int index = FindIndex(id);
            data[index].Item1 = newElement;
            SiftUp(index);
        }
    }

    class SolutionPriorityQueue
    {
        public static void Solve()
        {
            PriorityQueue pq = new PriorityQueue();
            string[] com;
            int i = 0;
            do
            {
                com = Console.ReadLine()?.Split();
                if (com == null)
                    break;
                switch (com[0])
                {
                    case "push":
                        Int64 element = Int64.Parse(com[1]);
                        pq.Add(element, i);
                        break;
                    case "decrease-key":
                        int id = int.Parse(com[1]) - 1;
                        Int64 newElement = Int64.Parse(com[2]);
                        pq.DecreaseElement(newElement, id);
                        break;
                    case "extract-min":
                        if (pq.Size() > 0)
                        {
                            Int64 result = pq.ExtractMin();
                            Console.WriteLine(result.ToString());
                        }
                        else
                        {
                            Console.WriteLine("*");
                        }

                        break;
                }

                i++;
            } while (com != null);

        }
    }
}

