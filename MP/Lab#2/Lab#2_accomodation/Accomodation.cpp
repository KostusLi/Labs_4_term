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
        this->cgen->reset();
        this->pgen->reset();
        this->cgen->getfirst();
    }

    short accomodation::getfirst()
    {
        short rc = (this->n >= this->m) ? this->m : -1;

        if (rc > 0)
        {
            for (int i = 0; i < this->m; i++)
            {
                this->sset[i] = this->cgen->sset[this->pgen->sset[i]];
            }
        }
        return rc;
    }

    short accomodation::getnext()
    {
        short rc;
        this->na++;

        if ((this->pgen->getnext()) >= 0)
        {
            rc = this->getfirst();
        }
        else if ((this->cgen->getnext()) >= 0)
        {
            this->pgen->reset();
            rc = this->getfirst();
        }
        else
        {
            rc = -1;
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
        for (unsigned long long i = 0; i < (unsigned long long)this->m; i++)
        {
            result *= (this->n - i);
        }
        return result;
    }
}