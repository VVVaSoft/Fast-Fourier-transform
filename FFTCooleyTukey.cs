using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace FFT
{
    class FFTCooleyTukey : FFT
    {
        private int NN, Npow2;
        private int[] RevInd;
        private Complex[] ExpTbl, ExpTblRev;
        private Complex[] tmp1, tmp2;

        public void Init(int N)
        {
            NN = N;

            NCheck.Check(ref NN, ref Npow2);

            RevInd = RevIndex.Create(NN, Npow2);

            ExpTbl = ExpTable.Create(NN);

            int Nd2 = NN >> 1;
            ExpTblRev = new Complex[Nd2];
            for (int i = 0; i < Nd2; i++)
                ExpTblRev[i] = ExpTbl[RevInd[i] >> 1];

            tmp1 = new Complex[NN];
            tmp2 = new Complex[NN];
        }

        public void Transform(Complex[] SX)
        {
            int Nd2 = NN >> 1;
            Complex[] tmp;
            Complex[] data = tmp1;
            Complex[] dest = tmp2;

            for (int i = 0; i < NN; i++)
                data[i] = SX[RevInd[i]];

            for (int len_block_pow = 1; len_block_pow < Npow2; len_block_pow++)
            {
                int len_block = (int)1 << len_block_pow;
                int len_block_half = (int)1 << (len_block_pow - 1);
                int no_block = (int)1 << (int)(Npow2 - len_block_pow);

                for (int i = 0; i < no_block; i++)
                {
                    int offset_block = i * len_block;
                    int offset_block_half = offset_block + len_block_half;
                    Complex w = ExpTblRev[i];
                    for (int j = 0; j < len_block_half; j++)
                    {
                        Complex d1 = data[offset_block + j];
                        Complex d2 = data[offset_block_half + j];

                        dest[offset_block + j] = d1 + d2;
                        dest[offset_block_half + j] = (d1 - d2) * w;
                    }
                }
                tmp = data;
                data = dest;
                dest = tmp;
            }

            for (int j = 0; j < Nd2; j++)
            {
                Complex d1 = data[j];
                Complex d2 = data[Nd2 + j];

                SX[ j] = d1 + d2;
                SX[Nd2 + j] = d1 - d2;
            }
        
        }
    }
}
