#pragma once

namespace combi
{
    struct xcombination
    {
        short n;      // всего элементов
        short m;      // сколько выбираем
        short* sset;  // текущая комбинация

        xcombination(short n = 1, short m = 1);
        ~xcombination();

        short getfirst();
        short getnext();
        unsigned long long count();
    };
}
