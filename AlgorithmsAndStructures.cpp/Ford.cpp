#include <iostream>
#include <set>
#include <vector>
#include <fstream>
#include <algorithm>

using namespace std;

typedef long long int64;
const int64 inf = 8e18;

struct Edge
{
	int from;
	int to;
	int64 weight;
	Edge(int from, int to, int64 weight) : from(from), to(to), weight(weight) {}
	Edge() {}
};

typedef vector<vector<Edge>> edgesLists;

void Dfs(int from, const vector<vector<int>>& graph, vector<bool>& used)
{
	used[from] = true;
	for (auto to : graph[from])
	{
		if (!used[to])
			Dfs(to, graph, used);
	}
}

vector<int64> Ford(int v, const vector<Edge>& edges, const vector<vector<int>>& graph)
{
	int n = graph.size();
	vector<int64> dist(n, inf);
	vector<int> parents(n, -1);
	int lastChanged;
	dist[v] = 0;
	for (int i = 0; i < n; ++i)
	{
		lastChanged = -1;
		for (auto edge : edges)
		{
			if (dist[edge.from] < inf)
			{
				if (dist[edge.to] > dist[edge.from] + edge.weight)
				{
					dist[edge.to] = max(-inf, dist[edge.from] + edge.weight);
					parents[edge.to] = edge.from;
					lastChanged = edge.to;
				}
			}
		}
	}

	if (lastChanged != -1)
	{
		for (int i = 0; i < n; ++i)
			lastChanged = parents[lastChanged];

		vector<bool> used(n);
		Dfs(lastChanged, graph, used);

		for (int i = 0; i < n; ++i)
		{
			if (used[i])
				dist[i] = -inf;
		}
	}
	
	return dist;
}


int solve()
{
	ifstream fin("path.in");
	ofstream fout("path.out");
	int n, m, s;
	fin >> n >> m >> s;
	s--;

	int64 from, to, weight;
	vector<Edge> edges;
	vector<vector<int>> graph(n);

	for (int i = 0; i < m; ++i)
	{
		fin >> from >> to >> weight;
		edges.emplace_back(from - 1, to - 1, weight);
		graph[from - 1].push_back(to - 1);
	}

	for (auto dist : Ford(s, edges, graph))
	{
		switch (dist)
		{
		case inf:
			fout << "*";
			break;
		case -inf:
			fout << "-";
			break;
		default:
			fout << dist;
		}
		fout << "\n";
	}

	fout << '\n';

	return 0;
}