#include <iostream>
#include <vector>
#include <algorithm>
#include <fstream>

using namespace std;

typedef long long int64;

struct Edge
{
	int64 to;
	mutable int64 weight;

	Edge(int64 to, int64 weight) : to(to), weight(weight) {}
	Edge() {}

	friend istream& operator>> (istream& is, Edge& edge)
	{
		is >> edge.to >> edge.weight;
		edge.to--;
		return is;
	}
};

typedef vector<vector<pair<int64, int64>>> edgesLists;


const int64 inf = 228228228228228;

int64 getMst(int64 root, const edgesLists& graph);
bool checkAvailability(int64 from, const edgesLists& graph);
void countVertex(int64 from, const edgesLists& graph, vector<bool>& visited);
void getCondensation(const edgesLists& graph, vector<int64>& components);
edgesLists getTransposed(const edgesLists& graph);
void markComponents(int64 from, const edgesLists& graph, vector<int64>& components, int64& component);
void getTopSort(const edgesLists& graph, vector<int64>& topSort);
void sortByTimeOut(int64 from, const edgesLists& graph, vector<bool>& visited, vector<int64>& topSort);

int64 getMst(int64 root, const edgesLists& graph)
{
	int64 result = 0;
	int64 n = graph.size();
	vector<int64> minInEdge = vector<int64>(n, inf);

	for (int64 from = 0; from < n; ++from)
	{
		for (auto edge : graph[from])
		{
			minInEdge[edge.first] = min(minInEdge[edge.first], edge.second);
		}
	}

	for (int64 i = 0; i < n; ++i)
		if (i != root)
			result += minInEdge[i];

	edgesLists zeroGraph = edgesLists(n);
	for (int64 from = 0; from < n; ++from)
	{
		for (auto edge : graph[from])
		{
			if (edge.second == minInEdge[edge.first])
			{
				edge.second = 0;
				zeroGraph[from].push_back(edge);
			}
		}
	}

	if (checkAvailability(root, zeroGraph))
		return result;

	vector<int64> components = vector<int64>(n);
	getCondensation(zeroGraph, components);
	int64 componentsCount = 0;
	for (auto component : components)
	{
		componentsCount = max(componentsCount, component);
	}
	componentsCount++;

	edgesLists newGraph = edgesLists(componentsCount);
	for (int64 from = 0; from < n; ++from)
	{
		for (auto edge : graph[from])
		{
			if (components[from] != components[edge.first])
				newGraph[components[from]].emplace_back(components[edge.first], edge.second - minInEdge[edge.first]);
		}
	}

	return result + getMst(components[root], newGraph);
}

bool checkAvailability(int64 from, const edgesLists& graph)
{
	int64 n = graph.size();
	vector<bool> visited = vector<bool>(n);
	countVertex(from, graph, visited);
	for (auto v : visited)
		if (!v)
			return false;
	return true;
}

void countVertex(int64 from, const edgesLists& graph, vector<bool>& visited)
{
	visited[from] = true;
	for (auto edge : graph[from])
		if (!visited[edge.first])
			countVertex(edge.first, graph, visited);
}

void getCondensation(const edgesLists& graph, vector<int64>& components)
{
	int64 n = graph.size();
	edgesLists graphTransposed = edgesLists(n);
	for (int64 from = 0; from < n; ++from)
		for (auto edge : graph[from])
			graphTransposed[edge.first].emplace_back(from, edge.second);
	vector<int64> order;
	getTopSort(graph, order);
	int64 componentsCount = 0;

	for (auto from : order)
	{
		if (components[from] == 0)
			markComponents(from, graphTransposed, components, ++componentsCount);
	}

	for (auto& component : components)
	{
		component--;
	}
}

edgesLists getTransposed(const edgesLists& graph)
{
	int64 n = graph.size();
	edgesLists graphTransposed = edgesLists(n);

	for (int64 from = 0; from < n; ++from)
		for (auto edge : graph[from])
			graphTransposed[edge.first].emplace_back(from, edge.second);

	return graphTransposed;
}

void getTopSort(const edgesLists& graph, vector<int64>& topSort)
{
	int64 n = graph.size();
	vector<bool> visited = vector<bool>(n);

	for (int64 from = 0; from < n; ++from)
	{
		if (!visited[from])
			sortByTimeOut(from, graph, visited, topSort);
	}

	reverse(topSort.begin(), topSort.end());
}

void sortByTimeOut(int64 from, const edgesLists& graph, vector<bool>& visited, vector<int64>& topSort)
{
	visited[from] = true;
	for (auto& edge : graph[from])
		if (!visited[edge.first])
			sortByTimeOut(edge.first, graph, visited, topSort);
	topSort.push_back(from);
}

void markComponents(int64 from, const edgesLists& graph, vector<int64>& components, int64& component)
{
	components[from] = component;
	for (auto& edge : graph[from])
		if (components[edge.first] == 0)
			markComponents(edge.first, graph, components, component);
}



int main()
{
	ifstream fin("chinese.in");
	ofstream fout("chinese.out");
	int64 n, m;
	fin >> n;
	fin >> m;

	edgesLists graph = edgesLists(n);
	int64 from;
	pair<int64, int64> edge;
	for (int64 i = 0; i < m; ++i)
	{
		fin >> from >> edge.first >> edge.second;
		graph[from - 1].emplace_back(edge.first - 1, edge.second);
	}

	if (checkAvailability(0, graph))
	{
		fout << "YES\n";
		fout << getMst(0, graph) << "\n";
	}
	else
	{
		fout << "NO\n";
	}
}