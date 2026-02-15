#include <iostream>
#include <locale.h>
#include "Combination.h"

int main()
{
    setlocale(LC_ALL, "rus");

    char AA[][2] = { "A", "B", "C", "D" };
    int n = sizeof(AA) / sizeof(AA[0]);
    int m = 2;

    std::cout << "\n- Генератор сочетаний -\n";
    std::cout << "Исходное множество: { ";

    for (int i = 0; i < n; i++)
        std::cout << AA[i] << (i < n - 1 ? ", " : " ");

    std::cout << "}\n";
    std::cout << "Выбираем m = " << m << "\n";

    combi::xcombination c1(n, m);

    int size = c1.getfirst();

    while (size >= 0)
    {
        std::cout << "{ ";

        for (int i = 0; i < size; i++)
            std::cout << AA[c1.sset[i]] << (i < size - 1 ? ", " : " ");

        std::cout << "}\n";

        size = c1.getnext();
    }

    std::cout << "Всего сочетаний: " << c1.count() << std::endl;

    system("pause");
    return 0;
}
