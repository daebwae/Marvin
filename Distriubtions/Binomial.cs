using System;
using static Toolkit.Misc;
using static System.Math; 

namespace Marvin.Distributions
{
    public class Binomial
    {
        public int n { get;  private set; }
        public double p { get; private set; }

        public Binomial(int n, double p)
        {
            this.n = n;
            this.p = p; 
        }

        public double CalcPmf(int k)
        {
            var coefficent = BinomialCoefficent(n, k);
            var p_occurence = Pow(p, k);
            var p_not_occurence = Pow(1 - p, n - k);
            var result = coefficent * p_occurence * p_not_occurence; 
            return result;
        }

        public double CalcCdf(int k)
        {
            var result = 0d; 
            for(int i=0; i <=k; i++)
            {
                var current = CalcPmf(i);
                result += current; 
            }

            return result; 
        }

        public double Mean => n * p;
        public double Median => Round(n * p);
        public double Mode => Floor((n + 1) * p);
        public double Variance => n * p * (1 - p); 
        public double StandardDeviation => Sqrt(Variance);

    }
}
