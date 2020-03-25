using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class Mst
    {
        class Dsu
        {
            private int[] _parent;
            private int[] _rank;

            public Dsu(int count)
            {
                _parent = new int[count];
                for (int i = 0; i < count; i++)
                {
                    _parent[i] = i;
                }

                _rank = new int[count];
                for (int i = 0; i < count; i++)
                {
                    _rank[i] = 0;
                }
            }

            public int Get(int v)
            {
                if (_parent[v] == v)
                    return v;
                return _parent[v] = Get(_parent[v]);
            }

            private void Swap(ref int a, ref int b)
            {
                int temp = a;
                a = b;
                b = temp;
            }

            public bool Unite(int v, int u)
            {
                v = Get(v);
                u = Get(u);
                if (u != v)
                {
                    if (_rank[v] < _rank[u])
                        Swap(ref v, ref u);
                    _parent[u] = v;
                    if (_rank[v] != _rank[u])
                        _rank[v]++;

                    return true;
                }

                return false;
            }
        }

        class Edge
        {
            public int From { get; private set; }
            public int To { get; private set; }
            public int Weight { get; private set; }

            public Edge(int from, int to, int weight)
            {
                From = from;
                To = to;
                Weight = weight;
            }
        }

        private static void Program()
        {
            int n, m;
            var input = Console.ReadLine().Split().Select(c => int.Parse(c)).ToArray();
            n = input[0];
            m = input[1];
            
            Dsu dsu = new Dsu(n);

            List <Edge> edgeList = new List<Edge>(m);

            for (int i = 0; i < m; i++)
            {
                input = Console.ReadLine().Split().Select(c => int.Parse(c)).ToArray();
                edgeList.Add(new Edge(input[0] - 1, input[1] - 1, input[2]));
            }

            Int64 result = edgeList
                .OrderBy(e => e.Weight)
                .Select(e => (Int64)(dsu.Unite(e.From, e.To) ? e.Weight : 0))
                .Sum();

            Console.WriteLine(result);
        }

        public static void Solve()
        {
            var stackSize = 100000000;
            Thread thread = new Thread(new ThreadStart(Program), stackSize);
            thread.Start();
        }
    }
}
