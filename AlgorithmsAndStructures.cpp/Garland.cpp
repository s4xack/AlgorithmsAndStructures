#include <iostream>
#include <fstream>
#include <vector>

using namespace std;

double answer;
int bulbsCount;
vector<double> bulbsHeight;

bool checkValid(double secondBulbHeight)
{
	bulbsHeight[1] = secondBulbHeight;
	for (int i = 2; i < bulbsCount; ++i)
	{
		bulbsHeight[i] = 2.0 * bulbsHeight[i - 1] + 2.0 - bulbsHeight[i - 2];
		if (bulbsHeight[i] < 0)
		{
			return false;
		}
	}
	answer = bulbsHeight[bulbsCount - 1];
	return true;
}

void binSearch(double left, double right)
{
	while (right - left > 0.000005)
	{
		double mid = (right + left) / 2.0;
		if (checkValid(mid))
		{
			right = mid;
		}
		else
		{
			left = mid;
		}
	}
}

int solve()
{
	ifstream fin("garland.in");
	ofstream fout("garland.out");

	fin >> bulbsCount;
	bulbsHeight.resize(bulbsCount);

	double firstBulbHeight;
	fin >> firstBulbHeight;
	bulbsHeight[0] = firstBulbHeight;

	binSearch(0.0, firstBulbHeight);
	fout << fixed;
	fout.precision(2);
	fout << answer << '\n';
	cerr << answer << '\n';
	return 0;
}