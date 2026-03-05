#include <iostream>
#include <locale.h>
#include <iomanip>
#include "Combination.h"

int main()
{
    setlocale(LC_ALL, "rus");

    char AA[][2] = { "A", "B", "C", "D", "E" };
    int N = sizeof(AA) / sizeof(AA[0]);
    int M = 3;

    std::cout << "--- Генератор сочетаний ---\n";
    std::cout << "Исходное множество: { ";
    for (int i = 0; i < N; i++)
        std::cout << AA[i] << (i < N - 1 ? ", " : " ");
    std::cout << "}\n";
    std::cout << "Генерация сочетаний из " << N << " по " << M << ":\n";

    combi::xcombination c(N, M);
    int n = c.getfirst();

    while (n >= 0)
    {
        std::cout << std::setw(3) << c.nc << ": { ";

        for (int i = 0; i < n; i++)
        {
            std::cout << AA[c.ntx(i)] << (i < n - 1 ? ", " : " ");
        }
        std::cout << "}\n";

        n = c.getnext();
    }

    std::cout << "Всего: " << c.count() << std::endl;

    system("pause");
    return 0;
}