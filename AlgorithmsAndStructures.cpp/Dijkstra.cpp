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


vector<int64> Dijkstra(int s, const edgesLists& graph)
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
	return distances;
}

int solve()
{
	ifstream fin("pathbgep.in");
	ofstream fout("pathbgep.out");
	int n, m;
	fin >> n >> m;

	edgesLists graph(n);
	int from, to;
	int64 weight;
	for (int i = 0; i < m; ++i)
	{
		fin >> from >> to >> weight;
		from--;
		to--;
		graph[from].emplace_back(to, weight);
		graph[to].emplace_back(from, weight);
	}
	for (auto dist : Dijkstra(0, graph))
	{
		fout << dist << ' ';
	}
	fout << '\n';

	return 0;
}