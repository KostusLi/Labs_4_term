#include <iostream>
#include <iomanip>
#include <locale.h>
#include <cstdlib>
#include <ctime>
#include "Knapsack.h"

int main()
{
    setlocale(LC_ALL, "rus");
    srand((unsigned)time(NULL));

    const int V = 300;
    const int N = 20;

    int v[N];
    int c[N];
    short m[N];

    std::cout << "--- Данные ---\n";
    std::cout << "N | Вес | Стоимость\n";

    for (int i = 0; i < N; i++)
    {
        v[i] = 10 + rand() % (300 - 10 + 1);
        c[i] = 5 + rand() % (55 - 5 + 1);

        std::cout << std::setw(2) << i + 1 << " | "
            << std::setw(3) << v[i] << " | "
            << std::setw(3) << c[i] << "\n";
    }
    std::cout << "\n";

    std::cout << "Решение задачи (Вместимость = " << V << ")...\n";

    clock_t t1=0, t2=0;
    
    t1 = clock();
    int max_cost = knapsack_s(V, N, v, c, m);
    t2 = clock();

    std::cout << "Время (тики процессора): " << (t2 - t1) << std::endl;
    std::cout << "Максимальная стоимость: " << max_cost << "\n";

    int total_weight = 0;
    std::cout << "Выбранные предметы: { ";
    for (int i = 0; i < N; i++)
    {
        if (m[i] == 1)
        {
            total_weight += v[i];
            std::cout << (i + 1) << " ";
        }
    }
    std::cout << "}\n";
    std::cout << "Итоговый вес: " << total_weight << "\n";

    system("pause");
    return 0;
}