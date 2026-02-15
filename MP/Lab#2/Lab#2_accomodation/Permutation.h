#pragma once

namespace combi
{
    struct permutation
    {
        const static bool L = true;
        const static bool R = false;

        short n;
        short* sset;
        bool* dart;
        unsigned long long np;

        permutation(short n = 1);
        ~permutation();

        void reset();
        long long getfirst();
        long long getnext();
        short ntx(short i);
        unsigned long long count() const;
    };
}