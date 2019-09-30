using System;
using System.IO;

namespace AlgorithmsAndStructures.SortingAlgorithms
{
    class KStaticstic
    {
        private static int FindKStatistic(int[] inputArray, int searchPosition)
        {
            int leftPosition = 0, rightPosition = inputArray.Length - 1;
            while (true)
            {
                if (leftPosition + 1 >= rightPosition)
                {
                    if (leftPosition + 1 == rightPosition && inputArray[leftPosition] > inputArray[rightPosition])
                    {
                        Swap(ref inputArray[leftPosition], ref inputArray[rightPosition]);
                    }
                    return inputArray[searchPosition];
                }

                int middle = (leftPosition + rightPosition) / 2;
                Swap(ref inputArray[middle], ref inputArray[leftPosition + 1]);
                if (inputArray[leftPosition] > inputArray[rightPosition])
                    Swap(ref inputArray[leftPosition], ref inputArray[rightPosition]);

                if (inputArray[leftPosition + 1] > inputArray[rightPosition])
                    Swap(ref inputArray[leftPosition + 1], ref inputArray[rightPosition]);

                if (inputArray[leftPosition] > inputArray[leftPosition + 1])
                    Swap(ref inputArray[leftPosition], ref inputArray[leftPosition + 1]);

                int leftPointer = leftPosition + 1;
                int rightPointer = rightPosition;

                int value = inputArray[leftPointer];

                while (true)
                {
                    do
                    {
                        leftPointer++;
                    } while (inputArray[leftPointer] < value);
                    do
                    {
                        rightPointer--;
                    } while (inputArray[rightPointer] > value);
                    if (leftPointer > rightPointer)
                    {
                        break;
                    }

                    Swap(ref inputArray[leftPointer], ref inputArray[rightPointer]);
                }

                inputArray[leftPosition + 1] = inputArray[rightPointer];
                inputArray[rightPointer] = value;

                if (rightPointer >= searchPosition)
                {
                    rightPosition = rightPointer - 1;
                }
                if (rightPointer <= searchPosition)
                {
                    leftPosition = leftPointer;
                }
            }
        }
        public static int[] FillArray(int n, int a, int b, int c, int firstElement, int secondElement)
        {
            int[] filledArray = new int[n];
            filledArray[0] = firstElement;
            if (n > 1)
            {
                filledArray[1] = secondElement;
            }

            for (int i = 2; i < n; i++)
            {
                filledArray[i] = a * filledArray[i - 2] + b * filledArray[i - 1] + c;
            }
            return filledArray;
        }

        public static void Swap<Template>(ref Template firstElement, ref Template secondElement)
        {
            Template swapHelper = firstElement;
            firstElement = secondElement;
            secondElement = swapHelper;
        }

        public static void Solve()
        {
            int n, k;
            int a, b, c;
            int firstElement, secondElement;

            string[] firstLine = Console.ReadLine().Split();
            string[] secondLine = Console.ReadLine().Split();
            n = Int32.Parse(firstLine[0]);
            k = Int32.Parse(firstLine[1]);
            a = Int32.Parse(secondLine[0]);
            b = Int32.Parse(secondLine[1]);
            c = Int32.Parse(secondLine[2]);
            firstElement = Int32.Parse(secondLine[3]);
            secondElement = Int32.Parse(secondLine[4]);

            int[] array = FillArray(n, a, b, c, firstElement, secondElement);
            int result = FindKStatistic(array, k - 1);

            Console.WriteLine(result);
        }
    }
}
