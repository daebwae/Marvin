using System;
using System.Security.Cryptography;
using static System.Math; 

namespace Toolkit
{
    public class RandomNumberGenerator
    {
        private RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private double? cachedBoxMueller;

        // The code for RandomNumber() is from Conrad Albrecht's answer at StackOverflow: 
        // http://stackoverflow.com/questions/2854438/how-to-generate-a-cryptographically-secure-double-between-0-and-1
        public double RandomNumber()
        {
            var bytes = new byte[8];
            rng.GetBytes(bytes);

            var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
            var d = ul / (Double)(1UL << 53);

            return d;
        }

        public bool RandomTruth()
        {
            return RandomNumber() < 0.5; 
        }

        public double RandomBoxMueller(double mean, double variance)
        {
            Func<double, double> distributionTransform = x => x * variance + mean; 

            if(cachedBoxMueller.HasValue)
            {
                var cache = distributionTransform(cachedBoxMueller.Value);
                cachedBoxMueller = null;
                return cache; 
            }
            var u1 = RandomNumber();
            var u2 = RandomNumber();

            var z0 = Sqrt(-2.0 * Log(u1)) * Cos(2 * PI * u2);
            var z1 = Sqrt(-2.0 * Log(u1)) * Sin(2 * PI * u2);

            var transformedResult = distributionTransform(z0);
            cachedBoxMueller = z1; 
            
            return transformedResult; 
        }
    }
}
