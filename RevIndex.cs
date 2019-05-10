using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFT
{
    class RevIndex
    {
        public static int[] Create(int NN, int Npow2)
        {
            int[] RevInd = new int[NN];

            for (long i = 0; i < NN; i++)
            {
                RevInd[i] = 0;
                for (int j = 0; j < Npow2; j++)
                    if (0 != (i & ((int)1 << (int)j)))
                        RevInd[i] |= (int)1 << (int)(Npow2 - 1 - j);
            }
            return RevInd;
        }
    }
}
