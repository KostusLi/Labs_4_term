#include "Combination.h"

namespace combi
{
    xcombination::xcombination(short n, short m)
    {
        this->n = n;
        this->m = m;
        this->sset = new short[m];
    }

    xcombination::~xcombination()
    {
        delete[] this->sset;
    }

    // Первое сочетание: 0 1 2 ... m-1
    short xcombination::getfirst()
    {
        for (short i = 0; i < m; i++)
            sset[i] = i;

        return m;
    }

    short xcombination::getnext()
    {
        for (short i = m - 1; i >= 0; i--)
        {
            if (sset[i] < n - m + i)
            {
                sset[i]++;

                for (short j = i + 1; j < m; j++)
                    sset[j] = sset[j - 1] + 1;

                return m;
            }
        }
        return -1;
    }

    unsigned long long xcombination::count()
    {
        unsigned long long num = 1;
        unsigned long long den = 1;

        for (int i = 1; i <= m; i++)
        {
            num *= (n - i + 1);
            den *= i;
        }

        return num / den;
    }
}
