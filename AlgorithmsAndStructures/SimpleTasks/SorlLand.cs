using System;
using System.IO;
using System.Globalization;

namespace AlgorithmsAndStructures.SimpleTasks
{
    class SorlLand
    {
        public static void Solve()
        {
            string[] input = File.ReadAllLines("sortland.in");
            int n = Int32.Parse(input[0]);
            string[] stringArray = input[1].Split();
            (int, float)[] array = new (int, float)[n];
            var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";
            for (int i = 0; i < n; ++i)
            {
                array[i] = (i + 1, float.Parse(stringArray[i], culture));
            }

            (int, float) temp;
            for (int t = 0; t < n; ++t)
            {
                for (int i = 0; i < n - 1; ++i)
                {
                    if (array[i].Item2 > array[i + 1].Item2)
                    {
                        temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                    }
                }
            }
            string result = "";
            result += Convert.ToString(array[0].Item1) + " ";
            result += Convert.ToString(array[n / 2].Item1) + " ";
            result += Convert.ToString(array[n - 1].Item1);
            File.WriteAllText("sortland.out", result);
        }
    }
}