#include <iostream>
#include <set>
#include <vector>
#include <fstream>

using namespace std;

typedef long long int64;
const int64 inf = 9223372036854775806;

struct Edge
{
	int from;
	int to;
	int64 weight;
	Edge(int from, int to, int64 weight) : from(from), to(to), weight(weight) {}
	Edge() {}
};

typedef vector<vector<Edge>> edgesLists;

vector<int64> Ford(int v, int n, const vector<Edge>& edges)
{
	vector<int64> dist(n, inf);
	dist[v] = 0;
	for (int i = 0; i < n - 1; ++i)
	{
		for (auto edge : edges)
		{
			if (dist[edge.from] < inf)
			{
				int64 newDist;
				if (dist[edge.from] > 0 || dist[edge.from] + inf > -edge.weight)
					newDist = dist[edge.from] + edge.weight;
				else
					newDist = -inf;
				if (dist[edge.to] > newDist || newDist == -inf)
				{
					dist[edge.to] = newDist;
				}
			}
		}
	}
	set<int> neg;
	for (int i = 0; i < n * 20; ++i)
	{
		for (auto edge : edges)
		{
			if (dist[edge.from] < inf)
			{
				int64 newDist;
				if (dist[edge.from] > 0 || dist[edge.from] + inf > -edge.weight)
					newDist = dist[edge.from] + edge.weight;
				else
					newDist = -inf;
				if (dist[edge.to] > newDist || newDist == -inf)
				{
					dist[edge.to] = newDist;
					neg.insert(edge.to);
				}
			}
		}
	}
	for (auto v : neg)
	{
		dist[v] = -inf;
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

	for (int i = 0; i < m; ++i)
	{
		fin >> from >> to >> weight;
		edges.emplace_back(from - 1, to - 1, weight);
	}

	for (auto dist : Ford(s, n, edges))
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

}