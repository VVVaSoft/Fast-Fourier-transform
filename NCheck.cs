using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFT
{
    class NCheck
    {
        public static void Check(ref int N, ref int Npow2)
        {
            if (N < 4)
                throw new Exception("N < 4");

            Npow2 = -1;

            for (int i = 0; i < 31; i++)
            {
                if (N == ((int)1 << i))
                {
                    Npow2 = i;
                    break;
                }
            }

            if (Npow2 == -1)
            {
                N = -1;
                throw new Exception("N != 2^x");
            }

        }
    }
}
