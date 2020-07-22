#include <iostream>
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
 
int main()
{
    int n;
    cin >> n;
    string str;
    cin >> str;
    str += "$";
    vector<int> prefix;
    prefixFunc(str, prefix);
    vector<vector<int>> akmp(str.size(), vector<int>(n));
    for (int i = 0; i < str.size(); ++i)
    {
        for (char chr = 0; chr < n; ++chr)
        {
            if (i > 0 && chr + 'a' != str[i])
                akmp[i][chr] = akmp[prefix[i - 1]][chr];
            else if (chr + 'a' == str[i])
                akmp[i][chr] = i + 1;
            else
                akmp[i][chr] = i;
        }
    }
 
    for (auto row : akmp)
    {
        for (int a : row)
        {
            cout << a << " ";
        }
        cout << endl;
    }
}