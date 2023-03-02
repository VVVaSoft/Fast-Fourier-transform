using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
   
namespace FFT
{
    class FFTClassic : FFT
    {
        private int NN, Npow2;
        private int[] RevInd;
        private Complex[] ExpTbl;
        private Complex[] tmp1, tmp2;

        public void Init(int N)
        {
            NN = N;

            NCheck.Check(ref NN, ref Npow2);

            RevInd = RevIndex.Create(NN, Npow2);

            ExpTbl = ExpTable.Create(NN);

            tmp1 = new Complex[NN];
            tmp2 = new Complex[NN];
        }

        public void Transform(Complex[] SX)
        {

            int Nd2 = NN >> 1;

            Complex[] tmp_for_change;
            Complex[] data = tmp1;
            Complex[] dest = tmp2;


            for (int i = 0; i < Nd2; i++)
            {
                int index1 = (i << 1);
                int index2 = (i << 1) + 1;
                Complex d1 = SX[RevInd[index1]];
                Complex d2 = SX[RevInd[index2]];
                data[index1] = d1 + d2;
                data[index2] = d1 - d2;
            }

            for (int len = 4, len_pow = 2; len < NN; len = len << 1, len_pow++)
            {

                int pow_N_m_len = (int)(Npow2 - len_pow);
                int no_block = (int)1 << pow_N_m_len;
                int half_len = len >> 1;

                for (int i = 0; i < no_block; i++)
                {
                    int offset_block = i * len;
                    int offset_half = offset_block + half_len;

                    for (int j = 0; j < half_len; j++)
                    {
                        Complex d1 = data[offset_block + j];
                        Complex d2w = data[offset_half + j] * ExpTbl[j << pow_N_m_len];
                        dest[offset_block + j] = d1 + d2w;
                        dest[offset_half + j] = d1 - d2w;
                    }
                }

                tmp_for_change = data;
                data = dest;
                dest = tmp_for_change;
            }

            for (int i = 0; i < Nd2; i++)
            {
                Complex d1 = data[i];
                Complex d2w = data[Nd2 + i] * ExpTbl[i];
                SX[i] = d1 + d2w;
                SX[Nd2 + i] = d1 - d2w; 
            }
            
        }
    }
}
