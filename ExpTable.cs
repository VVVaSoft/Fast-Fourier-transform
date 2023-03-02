using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
  
namespace FFT
{
    class ExpTable
    {
        public static Complex[] Create(int NN) 
        {
            int NNd2 = NN >> 1;
            Complex[] ret = new Complex[NNd2];

            for (long i = 0; i < NNd2; i++)
                ret[i] = Complex.Exp(-2.0d * Complex.ImaginaryOne * 3.14159265358979323846d * (double)(i) / (double)NN);  //Math.PI has only 5 digit from point in VS 2010

            // return e^(-2*pi*i/N)
            // return ret[i * nn];//nn=N/len, N - глобальная N , len - нужная N , i - строка

            return ret;
        }
    }
}
