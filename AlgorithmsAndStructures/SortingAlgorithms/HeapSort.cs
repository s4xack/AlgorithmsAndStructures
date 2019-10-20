using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlgorithmsAndStructures.SortingAlgorithms
{
    class Heap
    {
        public Int64[] heap { get; set; }
        public int heapSize;

        public Heap(Int64[] array)
        {
            BuildHeap(array);
        }

        public static void Swap(ref Int64 a, ref Int64 b)
        {
            Int64 temp = a;
            a = b;
            b = temp;
        }

        public void SiftDown(int parent)
        {
            while (2 * parent + 1 < heapSize)
            {
                int leftSon = 2 * parent + 1;
                int rightSon = 2 * parent + 2;
                int biggestSon = leftSon;

                if (rightSon < heapSize && heap[rightSon] > heap[leftSon])
                    biggestSon = rightSon;

                if (heap[parent] >= heap[biggestSon])
                    break;

                Swap(ref heap[parent], ref heap[biggestSon]);
                parent = biggestSon;
            }
        }

        public void SiftUp(int son)
        {
            while (heap[son] > heap[(son - 1) / 2])
            {
                Swap(ref heap[son], ref heap[(son - 1) / 2]);
                son = (son - 1) / 2;
            }
        }

        public void BuildHeap(Int64[] array)
        {
            heapSize = array.Length;
            heap = new Int64[heapSize];
            array.CopyTo(heap, 0);

            for (int i = heapSize / 2; i >= 0; --i)
            {
                SiftDown(i);
            }
        }
    }
    class HeapSort
    {
        private static Int64[] Sort(Int64[] array)
        {
            Heap heap = new Heap(array);
            for (int i = 0; i < array.Length - 1; ++i)
            {
                Heap.Swap(ref heap.heap[0], ref heap.heap[array.Length - 1 - i]);
                heap.heapSize--;
                heap.SiftDown(0);
            }

            return heap.heap;
        }

        public static void Solve()
        {
            int n;
            Int64[] array;
            using (var inputFile = new StreamReader("sort.in"))
            {
                n = int.Parse(inputFile.ReadLine());
                string[] line = inputFile.ReadLine().Split(' ');
                array = new Int64[n];
                for (int i = 0; i < n; ++i)
                {
                    array[i] = Int64.Parse(line[i]);
                }
            }
            array = Sort(array);
            using (var outputFile = new StreamWriter("sort.out"))
            {
                for (int i = 0; i < n; ++i)
                {
                    outputFile.Write(array[i]);
                    outputFile.Write(" ");
                }
                outputFile.WriteLine();
            }
        }
    }
}
