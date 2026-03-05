#include <iostream>
#include <iomanip>
#include <locale.h>
#include "Accomodation.h"

int main()
{
    setlocale(LC_ALL, "rus");

    char AA[][2] = { "A", "B", "C", "D" };
    int N = sizeof(AA) / sizeof(AA[0]);
    int M = 3;

    std::cout << "--- Генератор размещений ---\n";
    std::cout << "Исходное множество: { ";
    for (int i = 0; i < N; i++)
        std::cout << AA[i] << (i < N - 1 ? ", " : " ");
    std::cout << "}\n\n";

    combi::accomodation s(N, M);
    int res = s.getfirst();

    while (res >= 0)
    {
        std::cout << std::setw(3) << s.na << ": { ";
        for (int i = 0; i < M; i++)
        {
            std::cout << AA[s.ntx(i)] << (i < M - 1 ? ", " : " ");
        }
        std::cout << "}\n";

        res = s.getnext();
    }

    std::cout << "\nВсего: " << s.count() << std::endl;

    system("pause");
    return 0;
}