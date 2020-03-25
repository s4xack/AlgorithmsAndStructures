using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AlgorithmsAndStructures.GraphAlgorithms
{
    class TwoChinese
    {
        private static Int64 GetMst(int root, List<Edge>[] graph)
        {
            Int64 result = 0;
            int n = graph.Length;
            int[] minInEdge = Enumerable.Repeat(int.MaxValue, n).ToArray();

            for (int from = 0; from < n; from++)
            {
                foreach (var edge in graph[from])
                {
                    minInEdge[edge.To] = Math.Min(minInEdge[edge.To], edge.Weight);
                }
            }

            for (int i = 0; i < n; i++)
                if (i != root)
                    result += (Int64)(minInEdge[i]);

            List<Edge>[] zeroGraph = new List<Edge>[n];
            for (int from = 0; from < n; from++)
            {
                zeroGraph[from] = new List<Edge>();
                foreach (var edge in graph[from])
                {
                    if (edge.Weight == minInEdge[edge.To])
                    {
                        edge.Weight = 0;
                        zeroGraph[from].Add(edge);
                    }
                }
            }

            if (CheckAvailability(root, zeroGraph))
                return result;

            int[] components = GetCondensation(zeroGraph);
            int componentsCount = components.Max() + 1;
            List<Edge>[] newGraph = new List<Edge>[componentsCount];
            for (int i = 0; i < componentsCount; i++)
            {
                newGraph[i] = new List<Edge>();
            }

            for (int from = 0; from < n; from++)
            {
                foreach (var edge in graph[from]
                .Where(edge => components[from] != components[edge.To]))
                {
                    newGraph[components[from]].Add(new Edge(components[edge.To], edge.Weight - minInEdge[edge.To]));
                }
            }

            result += GetMst(components[root], newGraph);
            return result;
        }

        private static bool CheckAvailability(int from, List<Edge>[] graph)
        {
            int n = graph.Length;
            bool[] visited = new bool[n];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(from);
            visited[from] = true;
            while (queue.Any())
            {
                from = queue.Dequeue();
                foreach (var edge in graph[from]
                .Where(edge => !visited[edge.To]))
                {
                    queue.Enqueue(edge.To);
                    visited[edge.To] = true;
                }
            }

            return visited.All(v => v);
        }

        private static List<int> GetTopSort(List<Edge>[] graph)
        {
            int n = graph.Length;
            bool[] visited = new bool[n];

            List<int> topSort = new List<int>(n);

            void Dfs(int from)
            {
                visited[from] = true;
                foreach (var edge in graph[from]
                .Where(edge => !visited[edge.To]))
                {
                    Dfs(edge.To);
                }

                topSort.Add(from);
            }

            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    Dfs(i);
                }
            }

            topSort.Reverse();
            return topSort;
        }

        private static int[] GetCondensation(List<Edge>[] graph)
        {
            int n = graph.Length;
            List<Edge>[] graphTransposed = GetTransposed(graph);
            List<int> order = GetTopSort(graph);
            bool[] visited = new bool[n];
            int componentCount = 0;
            int[] components = new int[n];

            void Dfs(int from)
            {
                visited[from] = true;
                components[from] = componentCount;
                foreach (var edge in graphTransposed[from]
                .Where(edge => !visited[edge.To]))
                {
                    Dfs(edge.To);
                }
            }

            foreach (var from in order)
            {
                if (!visited[from])
                {
                    componentCount++;
                    Dfs(from);
                }
            }

            return components.Select(c => c - 1).ToArray();
        }

        private static List<Edge>[] GetTransposed(List<Edge>[] graph)
        {
            int n = graph.Length;
            List<Edge>[] graphTransposed = new List<Edge>[n];
            for (int i = 0; i < n; i++)
            {
                graphTransposed[i] = new List<Edge>();
            }

            for (int from = 0; from < n; from++)
            {
                foreach (var edge in graph[from])
                {
                    graphTransposed[edge.To].Add(new Edge(from, edge.Weight));
                }
            }

            return graphTransposed;
        }

        private static void Program()
        {
            int n, m;
            var input = Console.ReadLine().Split().Select(c => int.Parse(c)).ToArray();
            n = input[0];
            m = input[1];

            List<Edge>[] graph = new List<Edge>[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<Edge>();
            }

            for (int i = 0; i < m; i++)
            {
                input = Console.ReadLine().Split().Select(c => int.Parse(c)).ToArray();
                graph[input[0] - 1].Add(new Edge(input[1] - 1, input[2]));
            }

            if (CheckAvailability(0, graph))
            {
                Console.WriteLine("YES");
                Console.WriteLine(GetMst(0, graph));
            }
            else
            {
                Console.WriteLine("NO");
            }
        }

        public static void Solve()
        {
            var stackSize = 100000000;
            Thread thread = new Thread(new ThreadStart(Program), stackSize);
            thread.Start();
        }
    }

    internal class Edge
    {
        public int To { get; private set; }
        public int Weight { get; set; }

        public Edge(int to, int weight)
        {
            To = to;
            Weight = weight;
        }
    }
}