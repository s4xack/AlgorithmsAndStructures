#include <iostream>
#include <fstream>

using namespace std;

int main()
{
	ifstream fin;
	fin.open("isheap.in");

	ofstream fout;
	fout.open("isheap.out");
	
	long long n;
	fin >> n;

	long long array[n];
	for (int i = 0; i < n; ++i)
	{
		fin >> array[i];
	}

	bool isHeap = true;
	for (int i = 0; i < n / 2; ++i)
	{
		if (2 * i + 1 < n && array[2 * i + 1] < array[i])
			isHeap = false;
		if (2 * i + 2 < n && array[2 * i + 2] < array[i])
			isHeap = false;
	}

	if (isHeap)
	{
		fout << "YES\n";
	}
	else
	{
		fout << "NO\n";
	}
	
	return 0;
}