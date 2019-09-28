using System;
using System.IO;

namespace AlgorithmsAndStructures.SimpleTasks
{
    class AplusB
    {
        public static void Solve()
        {
            int a, b;
            string[] input = File.ReadAllText("aplusb.in").Split();
            a = Int32.Parse(input[0]);
            b = Int32.Parse(input[1]);
            File.WriteAllText("aplusb.out", Convert.ToString(a + b));
        }
    }
}
