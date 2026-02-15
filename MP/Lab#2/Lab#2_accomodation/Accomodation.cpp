#include "Accomodation.h"

namespace combi
{
    accomodation::accomodation(short n, short m)
    {
        this->n = n;
        this->m = m;
        this->cgen = new xcombination(n, m);
        this->pgen = new permutation(m);
        this->sset = new short[m];
        this->reset();
    }

    accomodation::~accomodation()
    {
        delete this->cgen;
        delete this->pgen;
        delete[] this->sset;
    }

    void accomodation::reset()
    {
        this->na = 0;
        this->cgen->getfirst(); // Сброс сочетаний
        this->pgen->reset();    // Сброс перестановок
    }

    short accomodation::getfirst()
    {
        short rc = (this->n >= this->m) ? this->m : -1;

        if (rc > 0)
        {
            // Формируем первое размещение
            for (int i = 0; i < this->m; i++)
            {
                // Берём элемент из сочетания (cgen->sset) по индексу из перестановки (pgen->sset)
                this->sset[i] = this->cgen->sset[this->pgen->sset[i]];
            }
        }
        return rc;
    }

    short accomodation::getnext()
    {
        short rc;
        this->na++;

        // 1. Пытаемся получить следующую перестановку для текущего сочетания
        if ((this->pgen->getnext()) >= 0)
        {
            rc = 1;
        }
        // 2. Если перестановки кончились, берем следующее сочетание
        else if ((this->cgen->getnext()) >= 0)
        {
            this->pgen->reset(); // Сбрасываем перестановки в начало
            rc = 1;
        }
        else
        {
            rc = -1; // Всё закончилось
        }

        // Если нашли новое состояние, обновляем массив sset
        if (rc > 0)
        {
            for (int i = 0; i < this->m; i++)
            {
                this->sset[i] = this->cgen->sset[this->pgen->sset[i]];
            }
        }

        return rc;
    }

    short accomodation::ntx(short i)
    {
        return this->sset[i];
    }

    unsigned long long accomodation::count()
    {
        unsigned long long result = 1;
        // Формула A(n, m) = n! / (n-m)!
        // Это эквивалентно произведению m чисел: n * (n-1) * ... * (n-m+1)
        for (unsigned long long i = 0; i < (unsigned long long)this->m; i++)
        {
            result *= (this->n - i);
        }
        return result;
    }
}