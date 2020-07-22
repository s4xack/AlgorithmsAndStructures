#include <fstream>
#include <string>
#include <vector>
 
using namespace std;
 
void prefixFunc(string str, vector<int>& prefix)
{
    prefix = vector<int>(str.size());
 
    for (int i = 1; i < str.size(); ++i)
    {
        int currentPrefix = prefix[i - 1];
 
        while(currentPrefix > 0 && str[i] != str[currentPrefix])
            currentPrefix = prefix[currentPrefix - 1];
 
        if(str[i] == str[currentPrefix])
            currentPrefix++;
 
        prefix[i] = currentPrefix;
    }
}
 
int solve()
{
    ifstream fin("search2.in");
    ofstream fout("search2.out");
     
    string searchString;
    string str;
    fin >> searchString;
    fin >> str;
    vector<int> prefix;
    prefixFunc(searchString + "$" + str, prefix);
    vector<int> result;
    for (int i = searchString.size() + 1; i < prefix.size(); ++i)
    {
        if (prefix[i] == searchString.size())
        {
            result.push_back(i - 2 * searchString.size() + 1);
        }
    }
    fout << result.size() << endl;
    for (int position: result)
    {
        fout << position << " ";
    }
    fout << endl;
}