#include <iostream>
#include <set>
#include <vector>
#include <fstream>
#include <algorithm>

using namespace std;

typedef long long int64;
const int64 inf = 1e12 + 7;

struct Edge
{
	int from;
	int to;
	int64 weight;
	Edge(int from, int to, int64 weight) : from(from), to(to), weight(weight) {}
	Edge() {}
};

vector<int> FindNegativeCycle(int n, const vector<Edge>& edges)
{
	vector<int64> dist(n);
	vector<int> parents(n, -1);
	int lastChanged;
	for (int i = 0; i < n; ++i)
	{
		lastChanged = -1;
		for (auto edge : edges)
		{
			if (dist[edge.to] > dist[edge.from] + edge.weight)
			{
				dist[edge.to] = max(-inf, dist[edge.from] + edge.weight);
				parents[edge.to] = edge.from;
				lastChanged = edge.to;
			}
		}
	}
	if (lastChanged == -1)
	{
		return vector<int>(0);
	}
	else
	{
		int v = lastChanged;
		for (int i = 0; i < n; ++i)
			v = parents[v];

		vector<int> path;
		int u = v;
		do
		{
			path.push_back(u);
			u = parents[u];
		} while (u != v);
		path.push_back(u);
		reverse(path.begin(), path.end());
		return path;
	}
}


int solve()
{
	ifstream fin("negcycle.in");
	ofstream fout("negcycle.out");
	int n;
	fin >> n;

	int64 weight;
	vector<Edge> edges;

	for (int from = 0; from < n; ++from)
	{
		for (int to = 0; to < n; ++to)
		{
			fin >> weight;
			if (weight != 1e9)
				edges.emplace_back(from, to, weight);
		}
	}

	vector<int> result = FindNegativeCycle(n, edges);

	if (result.empty())
	{
		fout << "NO\n";
	}
	else
	{
		fout << "YES\n";
		fout << result.size() << "\n";
		for (auto v : result)
		{
			fout << v + 1 << " ";
		}
		fout << "\n";
	}

	fout << '\n';

}