#include "stdafx.h"
#include <iostream>
#include <ctime>
#include <locale>
#include "Auxil.h"

#define CYCLE 1000000

long long fib(int n)
{
    if (n <= 1) return n;
    return fib(n - 1) + fib(n - 2);
}

int main()
{
    setlocale(LC_ALL, "rus");

    double av1 = 0, av2 = 0;
    clock_t t1 = 0, t2 = 0;

    auxil::start();

    t1 = clock();

    for (int i = 0; i < CYCLE; i++)
    {
        av1 += auxil::iget(-100, 100);
        av2 += auxil::dget(-100, 100);
    }

    t2 = clock();

    std::cout << "\nЗАДАНИЕ 2";
    std::cout << "\nКоличество циклов:         " << CYCLE;
    std::cout << "\nСреднее значение (int):    " << av1 / CYCLE;
    std::cout << "\nСреднее значение (double): " << av2 / CYCLE;
    std::cout << "\nВремя (у.е.):              " << (t2 - t1);
    std::cout << "\nВремя (сек):               "
        << (double)(t2 - t1) / CLOCKS_PER_SEC;


    std::cout << "\n\nЗАДАНИЕ 3 (Рекурсивный Фибоначчи)\n";

    clock_t t;

    for (int n = 30; n <= 40; n++)
    {
        t = clock();
        fib(n);
        t = clock() - t;

        std::cout << "n = " << n << " | время (у.е.) = " << t << std::endl;
    }

    system("pause");
    return 0;
}