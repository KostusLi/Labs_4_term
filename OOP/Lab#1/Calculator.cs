using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    public static class Calculator
    {
        public delegate void BoxMessage(string message);

        public static int And(int a, int b) => a & b;
        public static int Or(int a, int b) => a | b;
        public static int Xor(int a, int b) => a ^ b;
        public static int Not(int a) => ~a;
    }
}
