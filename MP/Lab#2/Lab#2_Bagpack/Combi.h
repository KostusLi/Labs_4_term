#pragma once

namespace combi
{
    struct subset
    {
        short n;
        short sn;
        short* sset;
        unsigned long long mask;

        subset(short n = 1);
        ~subset();

        void reset();
        short getfirst();
        short getnext();
        short ntx(short i);
        unsigned long long count();
    };
}