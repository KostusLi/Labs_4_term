#pragma once
#include "Combination.h"
#include "Permutation.h"

namespace combi
{
    struct accomodation
    {
        short n;
        short m;
        short* sset;  // Текущее размещение
        unsigned long long na; // Счётчик

        // Вложенные генераторы
        xcombination* cgen;
        permutation* pgen;

        accomodation(short n, short m);
        ~accomodation();

        void reset();
        short getfirst();
        short getnext();
        short ntx(short i);
        unsigned long long count();
    };
}