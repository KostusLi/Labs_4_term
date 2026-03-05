#include <iostream>
#include <iomanip>
#include <locale.h>
#include "Salesman.h"

#define N 5
#define INF 0x7fffffff

int main()
{
    setlocale(LC_ALL, "rus");

    int d[N * N] = {
        INF,   13,  32, 15,  9,
        14,  INF,   25,  27,  65,
        20,  26,  INF,   72,  50,
        31,  42,  39,  INF,   126,
        72,  45,  45,  25,  INF
    };

    int r[N];

    std::cout << "--- Задача коммивояжера ---\n";
    std::cout << "Количество городов: " << N << "\n";
    std::cout << "Матрица расстояний:\n";

    for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < N; j++)
        {
            int val = d[i * N + j];
            if (val == INF)
                std::cout << std::setw(5) << "INF";
            else
                std::cout << std::setw(5) << val;
        }
        std::cout << "\n";
    }

    int s = salesman(N, d, r);

    std::cout << "\n--- Результаты ---\n";

    if (s == INF)
    {
        std::cout << "Решение не найдено (маршрут разорван).\n";
    }
    else
    {
        std::cout << "Оптимальный маршрут: ";
        for (int i = 0; i < N; i++)
        {
            std::cout << r[i]+1 << " --> ";
        }
        std::cout << r[0]+1;

        std::cout << "\nДлина маршрута: " << s << "\n";
    }

    system("pause");
    return 0;
}