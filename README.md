# Fast-Fourier-transform
Fast Fourier transform and Cooley–Tukey algorithm

# Using

int pow = 22;
int NO = 1 << pow;
FFT fft = new FFTCooleyTukey();// or new FFTClassic();
fft.Init(NO);
// SX <- pionts
fft.Transform(SX);
// SX -> spectrum
