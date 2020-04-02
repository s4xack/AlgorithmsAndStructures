#include <iostream>
#include <set>
#include <vector>
#include <fstream>
#include <algorithm>

using namespace std;

typedef long long int64;
const int64 inf = 1e12 + 7;



int solve()
{
	ifstream fin("pathsg.in");
	ofstream fout("pathsg.out");
	int n, m;
	fin >> n >> m;

	int from, to;
	int64 weight;
	vector<vector<int64>> matrix(n, vector<int64>(n, inf));
	for (int i = 0; i < n; ++i)
		matrix[i][i] = 0;

	for (int i = 0; i < m; ++i)
	{
		fin >> from >> to >> weight;
		from--;
		to--;
		matrix[from][to] = weight;
	}

	for (int k = 0; k < n; ++k)
		for (int i = 0; i < n; ++i)
			for (int j = 0; j < n; ++j)
				matrix[i][j] = min(matrix[i][j], matrix[i][k] + matrix[k][j]);

	for (auto line : matrix)
	{
		for (auto dist : line)
		{
			fout << dist << ' ';
		}
		fout << '\n';
	}

	fout << '\n';

	return 0;
}
