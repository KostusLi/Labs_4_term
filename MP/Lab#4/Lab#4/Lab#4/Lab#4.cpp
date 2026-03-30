#include <algorithm>
#include <iostream>
#include <ctime>
#include <iomanip>
#include <cstdlib>
#include "Levenshtein.h"

int main()
{
    setlocale(LC_ALL, "rus");
    srand(time(0));

    clock_t t1 = 0, t2 = 0, t3, t4;

    int len_S1 = 300;
    int len_S2 = 200;
    char x[301];
    char y[201];

    for (int i = 0; i < len_S1; i++) x[i] = 'a' + rand() % 26;
    x[len_S1] = '\0';
    for (int i = 0; i < len_S2; i++) y[i] = 'a' + rand() % 26;
    y[len_S2] = '\0';

    std::cout << "Сгенерированы случайные строки:" << std::endl;
    std::cout << "S1 (300 симв): " << std::string(x).substr(0, 40) << "..." << std::endl;
    std::cout << "S2 (200 симв): " << std::string(y).substr(0, 40) << "..." << std::endl;
    std::cout << std::endl;

    double K_values[] = { 1.0 / 25.0, 1.0 / 20.0, 1.0 / 15.0, 1.0 / 10.0, 1.0 / 5.0, 1.0 / 2.0, 1.0 };
    int K_size = 7;

    std::cout << "-------------------------------------------------------------------------" << std::endl;
    std::cout << "|   k   | Длины (L1/L2) | Время Рекурсии      | Время Динам. прогр. |" << std::endl;
    std::cout << "-------------------------------------------------------------------------" << std::endl;

    for (int i = 0; i < K_size; i++)
    {
        double k = K_values[i];

        int curr_lx = (int)(len_S1 * k);
        int curr_ly = (int)(len_S2 * k);

        t3 = clock();
        levenshtein(curr_lx, x, curr_ly, y);
        t4 = clock();

        std::cout << "| 1/" << std::left << std::setw(3) << (int)(1.0 / k)
            << " | " << std::right << std::setw(6) << curr_lx << "/" << std::left << std::setw(6) << curr_ly << "| ";

        if (curr_lx <= 15) {
            t1 = clock();
            levenshtein_r(curr_lx, x, curr_ly, y);
            t2 = clock();
            std::cout << std::right << std::setw(15) << (t2 - t1) << " мс | ";
        }
        else {
            std::cout << std::right << std::setw(18) << "Слишком долго | ";
        }

        std::cout << std::setw(15) << (t4 - t3) << " мс |" << std::endl;
    }
    std::cout << "-------------------------------------------------------------------------" << std::endl;

    system("pause");
    return 0;
}