using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace FFT
{
    interface iFFT
    {
        void Init(int no);
        void Transform(Complex[] XS);
    }
}
