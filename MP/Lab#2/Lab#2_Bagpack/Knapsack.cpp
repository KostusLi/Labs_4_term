#include "Knapsack.h"

namespace
{
    int calcv(combi::subset& s, const int v[])
    {
        int rc = 0;
        for (int i = 0; i < s.sn; i++)
        {
            rc += v[s.ntx(i)];
        }
        return rc;
    }

    int calcc(combi::subset& s, const int c[])
    {
        int rc = 0;
        for (int i = 0; i < s.sn; i++)
        {
            rc += c[s.ntx(i)];
        }
        return rc;
    }

    void setm(combi::subset& s, short m[])
    {
        for (int i = 0; i < s.n; i++) m[i] = 0;
        for (int i = 0; i < s.sn; i++)
        {
            m[s.ntx(i)] = 1;
        }
    }
}

int knapsack_s(int V, short n, const int v[], const int c[], short m[])
{
    combi::subset s(n);
    int maxc = -2147483647;
    int cc = 0;

    short ns = s.getfirst();

    while (ns >= 0)
    {
        if (calcv(s, v) <= V)
        {
            cc = calcc(s, c);
            if (cc > maxc)
            {
                maxc = cc;
                setm(s, m);
            }
        }
        ns = s.getnext();
    }

    return maxc;
}