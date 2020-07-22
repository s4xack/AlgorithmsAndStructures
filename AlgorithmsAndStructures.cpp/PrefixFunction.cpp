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
    ifstream fin("prefix.in");
    ofstream fout("prefix.out");
     
    string str;
    fin >> str;
    vector<int> prefix;
    prefixFunc(str, prefix);
 
    for (int pi: prefix)
    {
        fout << pi << " ";
    }
    fout << endl;
}