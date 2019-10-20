using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AlgorithmsAndStructures.Heap
{
    class Heap
    {
        public int[] heap { get; set; }
        public int heapSize;

        public Heap(int[] array)
        {
            BuildHeap(array);
        }

        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
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

        public void BuildHeap(int[] array)
        {
            heap = array;
            heapSize = array.Length;

            for (int i = heapSize / 2; i >= 0; --i)
            {
                SiftDown(i);
            }
        }
    }
}
