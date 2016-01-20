using static Toolkit.Misc;
using static System.Math;

namespace Distriubtions
{
    public class Beta
    {
        public double alpha { get; private set; }
        public double beta { get; private set; }
        private readonly double normalizationConst; 

        public Beta(double alpha, double beta)
        {
            this.alpha = alpha;
            this.beta = beta;
            normalizationConst = BetaFunction(alpha, beta); 
        }

        public double CalcPdf(double x)
        {
            if(x <0 || x>1)
            {
                return 0.0; 
            }
            var x_alpha = Pow(x, alpha - 1);
            var x_beta = Pow(1 - x, beta - 1);
            var result = (x_alpha * x_beta) / normalizationConst; 
            return result; 
        }

        public double CalcCdf(double k)
        {
            var result = Integrate(CalcPdf, 0.00000000000001d, k, 1000); 
            return result;
        }

        public double Mean => alpha /(alpha + beta);
        public double Median => (alpha - 1/3d ) / (alpha + beta - 2d/3)  ;
        public double Mode => (alpha -1) / (alpha + beta -2);
        public double Variance => alpha * beta / ( Pow(alpha + beta, 2) * (alpha +beta +1));
        public double StandardDeviation => Sqrt(Variance);

        static public Beta FromSample(double sampleMean, double sampleVariance)
        {
            var coefficient = (sampleMean * (1 - sampleMean) / sampleVariance - 1); 
            var alpha = sampleMean * coefficient;
            var beta = (1 - sampleMean) * coefficient;

            return new Beta(alpha, beta); 
        }


        static public Beta FromBinomial(int trials, int successes)
        {
            var alpha = successes + 1;
            var beta = (trials - successes) + 1; 

            return new Beta(alpha, beta);
        }
    }
}
