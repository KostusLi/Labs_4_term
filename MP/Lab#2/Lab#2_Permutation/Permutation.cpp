#include "Permutation.h"
#include <algorithm>

#define NINF ((short)0x8000)

namespace combi
{
    permutation::permutation(short n)
    {
        this->n = n;
        this->sset = new short[n];
        this->dart = new bool[n];
        this->reset();
    }

    permutation::~permutation()
    {
        delete[] this->sset;
        delete[] this->dart;
    }

    void permutation::reset()
    {
        this->getfirst();
    }

    long long permutation::getfirst()
    {
        this->np = 0;
        for (int i = 0; i < this->n; i++)
        {
            this->sset[i] = i;
            this->dart[i] = L;
        }
        return (this->n > 0) ? this->np : -1;
    }

    long long permutation::getnext()
    {
        long long rc = -1;
        short maxm = NINF;
        short idx = -1;

        for (int i = 0; i < this->n; i++)
        {
            bool is_mobile = false;

            if (this->dart[i] == L)
            {
                if (i > 0 && this->sset[i] > this->sset[i - 1])
                    is_mobile = true;
            }
            else
            {
                if (i < (this->n - 1) && this->sset[i] > this->sset[i + 1])
                    is_mobile = true;
            }

            if (is_mobile && this->sset[i] > maxm)
            {
                maxm = this->sset[i];
                idx = i;
            }
        }

        if (idx >= 0)
        {
            int neighbor_offset = (this->dart[idx] == L ? -1 : 1);
            int neighbor_idx = idx + neighbor_offset;

            std::swap(this->sset[idx], this->sset[neighbor_idx]);
            std::swap(this->dart[idx], this->dart[neighbor_idx]);

            for (int i = 0; i < this->n; i++)
            {
                if (this->sset[i] > maxm)
                {
                    this->dart[i] = !this->dart[i];
                }
            }

            rc = ++this->np;
        }

        return rc;
    }

    short permutation::ntx(short i)
    {
        return this->sset[i];
    }

    unsigned long long fact(unsigned long long x)
    {
        return (x == 0) ? 1 : (x * fact(x - 1));
    }

    unsigned long long permutation::count() const
    {
        return fact(this->n);
    }
}