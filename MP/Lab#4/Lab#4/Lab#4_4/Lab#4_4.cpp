#include <iostream>
#include <iomanip>
#include <ctime>
#include <cstdlib>
#include <cstring>
#include "LCS.h" 
#include "LCH.h" 

int main()
{
    setlocale(LC_ALL, "rus");
    srand(time(0));

    char z[100] = "";
    char x[] = "ANPAFRE";
    char y[] = "ICBUFR";

    int l = lcsd(x, y, z);

    std::cout << std::endl;
    std::cout << "последовательность X: " << x << std::endl;
    std::cout << "последовательность Y: " << y << std::endl;
    std::cout << "LCS: " << z << std::endl;
    std::cout << "длина LCS: " << l << std::endl;
    std::cout << std::endl;


    std::cout << "=== Сравнительный анализ времени ===" << std::endl;
    std::cout << "--------------------------------------------------------" << std::endl;
    std::cout << "| Длина (k) | Время Рекурсии  | Время Динам. прогр. |" << std::endl;
    std::cout << "--------------------------------------------------------" << std::endl;

    char X_rand[30]; char Y_rand[30]; char Z_rand[30];
    for (int i = 0; i < 25; i++) {
        X_rand[i] = 'A' + rand() % 26;
        Y_rand[i] = 'A' + rand() % 26;
    }
    X_rand[25] = '\0'; Y_rand[25] = '\0';

    clock_t t1, t2, t3, t4;

    for (int k = 1; k <= 15; k++) {
        char temp_X[30], temp_Y[30];
        strncpy_s(temp_X, X_rand, k); temp_X[k] = '\0';
        strncpy_s(temp_Y, Y_rand, k); temp_Y[k] = '\0';

        t3 = clock();
        lcsd(temp_X, temp_Y, Z_rand);
        t4 = clock();

        t1 = clock();
        lcs(k, temp_X, k, temp_Y);
        t2 = clock();

        std::cout << "| " << std::setw(5) << k << "     | "
            << std::setw(11) << (t2 - t1) << " мс | "
            << std::setw(15) << (t4 - t3) << " мс |" << std::endl;
    }
    std::cout << "--------------------------------------------------------" << std::endl;

    system("pause");
    return 0;
}