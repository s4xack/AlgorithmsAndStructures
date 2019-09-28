using System;
using System.IO;

namespace AlgorithmsAndStructures.SimpleTasks
{
    class AplusBB
    {
        public static void Solve()
        {
            long a, b;
            string[] input = File.ReadAllText("aplusbb.in").Split();
            a = Int64.Parse(input[0]);
            b = Int64.Parse(input[1]);
            long result = a + b * b;
            File.WriteAllText("aplusbb.out", Convert.ToString(result));
        }
    }
}
