#include "Combi.h"

namespace combi
{
    subset::subset(short n)
    {
        this->n = n;
        this->mask = 0;           // обязательно
        this->sset = new short[n];
        this->reset();
    }

    subset::~subset()             // чтобы не было утечки
    {
        delete[] this->sset;
    }

    void subset::reset()
    {
        this->sn = 0;
        this->mask = 0;
    }

    short subset::getfirst()
    {
        unsigned long long buf = this->mask;   // вместо __int64
        this->sn = 0;

        for (short i = 0; i < n; i++)
        {
            if (buf & 1ULL)
                this->sset[this->sn++] = i;

            buf >>= 1;
        }

        return this->sn;
    }

    short subset::getnext()
    {
        short rc = -1;

        this->sn = 0;

        if (++this->mask < this->count())
            rc = getfirst();

        return rc;
    }

    short subset::ntx(short i)
    {
        return this->sset[i];
    }

    unsigned long long subset::count()
    {
        return (1ULL << this->n);   // ВАЖНО: 1ULL
    }
}
