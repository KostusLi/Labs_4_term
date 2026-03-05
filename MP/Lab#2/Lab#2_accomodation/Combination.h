#pragma once

namespace combi
{
    struct xcombination
    {
        short n;
        short m;
        short* sset;
        unsigned long long nc;

        xcombination(short n = 1, short m = 1);
        ~xcombination();

        void reset();
        short getfirst();
        short getnext();
        short ntx(short i);
        unsigned long long count() const;
    };
}