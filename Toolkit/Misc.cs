using System;
using System.Numerics;
using static System.Math; 

namespace Toolkit
{
    public static class Misc
    {
        public static bool EpsilonEquals(double x, double y, double eps) => Abs(x - y) <= eps;
        // Lanczos approximation from Wikipedia Python Code: 
        // https://en.wikipedia.org/w/index.php?title=Lanczos_approximation&oldid=696252067
        // as implemented by Rossettacode.org: http://rosettacode.org/wiki/Gamma_function#C.23
        private static int g = 7;
        private static double[] p = {0.99999999999980993, 676.5203681218851, -1259.1392167224028,
         771.32342877765313, -176.61502916214059, 12.507343278686905,
         -0.13857109526572012, 9.9843695780195716e-6, 1.5056327351493116e-7};
        public static Complex GammaComplex(Complex z)
        {
            // Reflection formula
            if (z.Real < 0.5)
            {
                return Math.PI / (Complex.Sin(Math.PI * z) * GammaComplex(1 - z));
            }
            else
            {
                z -= 1;
                Complex x = p[0];
                for (var i = 1; i < g + 2; i++)
                {
                    x += p[i] / (z + i);
                }
                Complex t = z + g + 0.5;
                return Complex.Sqrt(2 * Math.PI) * (Complex.Pow(t, z + 0.5)) * Complex.Exp(-t) * x;
            }
        }

        public static double Integrate(Func<double,double> functionToIntegrate, double lower, double upper, int steps)
        {
            double stepsize = (upper - lower) / steps;
            return Integrate(functionToIntegrate, lower, upper, stepsize); 
        }


        public static double Integrate(Func<double, double> functionToIntegrate, double lower, double upper, double stepSize)
        {
            
            var result = 0d;
            for (double left = lower;  (left + stepSize) < upper; left += stepSize)
            {
                result += functionToIntegrate(left);
            }
            result *= stepSize;
            return result;
        }

        public static double Gamma(double x)
        {
            return GammaComplex(x).Real; 
        }
        public static double BetaFunction(double alpha, double beta) => (Gamma(alpha) * Gamma(beta)) / Gamma(alpha + beta);

        public static BigInteger Factorial(int x)
        {
            if (x < 0)
                throw new ArgumentException(nameof(x) + " may not be less than 1"); 

            var result = (BigInteger) 1;

            for(int i=x; i > 1; i--)
            {
                result *= i; 
            }

            return result; 
        }

        public static double BinomialCoefficent(int n, int k)
        {
            var n_fac = Factorial(n);
            var k_fac = Factorial(k);
            var n_minus_k_fac = Factorial(n - k);
            var result = n_fac / (k_fac * n_minus_k_fac); 
            return (double) result;
        }
    }
}
