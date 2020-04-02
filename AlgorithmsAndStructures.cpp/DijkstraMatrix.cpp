#include <iostream>
#include <set>
#include <vector>
#include <fstream>

using namespace std;

typedef long long int64;
const int64 inf = 1e12 + 7;

struct Edge
{
	int to;
	int64 weight;
	Edge(int to, int64 weight) : to(to), weight(weight) {}
	Edge() {}
};

typedef vector<vector<Edge>> edgesLists;


int64 Dijkstra(int s, int f, const edgesLists& graph)
{
	int n = graph.size();
	vector<int64> distances(n, inf);
	set<pair<int64, int>> dijkstraQueue;

	distances[s] = 0;
	dijkstraQueue.insert({ 0, s });

	while (!dijkstraQueue.empty())
	{
		pair<int64, int> cur = *dijkstraQueue.begin();
		dijkstraQueue.erase(dijkstraQueue.begin());

		for (auto edge : graph[cur.second])
			if (distances[edge.to] > cur.first + edge.weight)
			{
				dijkstraQueue.erase({ distances[edge.to], edge.to });
				dijkstraQueue.insert({ cur.first + edge.weight, edge.to });
				distances[edge.to] = cur.first + edge.weight;
			}
	}
	return distances[f] == inf ? -1 : distances[f];
}

int solve()
{
	ifstream fin("pathmgep.in");
	ofstream fout("pathmgep.out");
	int n, s, f;
	fin >> n >> s >> f;
	s--;
	f--;

	edgesLists graph(n);
	int64 weight;
	for (int from = 0; from < n; ++from)
	{
		for (int to = 0; to < n; ++to)
		{
			fin >> weight;
			if (weight != -1 && to != from)
				graph[from].emplace_back(to, weight);
		}
	}
	fout << Dijkstra(s, f, graph) << "\n";


	return 0;
}