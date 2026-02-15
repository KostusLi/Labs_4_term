#include <iostream>
#include <locale.h>
#include <iomanip>
#include "Permutation.h"

int main()
{
    setlocale(LC_ALL, "rus");

    char AA[][2] = { "A", "B", "C", "D" };
    int n_elements = sizeof(AA) / sizeof(AA[0]);

    std::cout << "\n--- Генератор перестановок ---\n";
    std::cout << "Исходное множество: { ";
    for (int i = 0; i < n_elements; i++)
        std::cout << AA[i] << (i < n_elements - 1 ? ", " : " ");
    std::cout << "}\n\n";

    combi::permutation p(n_elements);

    std::cout << "Генерация перестановок:\n";

    long long n = p.getfirst();

    while (n >= 0)
    {
        std::cout << std::setw(4) << p.np << ": { ";
        for (int i = 0; i < p.n; i++)
        {
            std::cout << AA[p.ntx(i)] << (i < p.n - 1 ? ", " : " ");
        }
        std::cout << "}\n";

        n = p.getnext();
    }

    std::cout << "\nВсего: " << p.count() << std::endl;

    system("pause");
    return 0;
}