#pragma once

namespace combi
{
    struct subset   // генератор множества всех подмножеств
    {
        short n;      // количество элементов исходного множества < 64
        short sn;     // количество элементов текущего подмножества
        short* sset;  // массив индексов текущего подмножества
        unsigned long long mask;   // битовая маска

        subset(short n = 1);
        ~subset();                // добавили деструктор

        void reset();             // ОБЯЗАТЕЛЬНО объявить
        short getfirst();
        short getnext();
        short ntx(short i);
        unsigned long long count();
    };
}
