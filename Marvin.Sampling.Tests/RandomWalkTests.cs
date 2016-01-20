using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;


namespace Marvin.Sampling.Tests
{
    [TestClass()]
    public class RandomWalkTests
    {
        private List<int> values;
        private List<double> probs; 

        [TestInitialize()]
        public void Init()
        {
            values = new List<int>() { 1, 2, 3, 4, 5 };
            probs = new List<double>() { 0.1, 0.1, 0.6, 0.1, 0.1 }; 

        }

        [TestMethod()]
        public void CanInstantiateRandomWalkTest()
        {
            new RandomWalk<int,double>(values, probs); 
        }

     
        [TestMethod()]
        public void GetSampleTest()
        {
            int n = 10000; 
            var samples = GetDefaultSample(n);

            var threesProportion = samples.Count(i => i == 3) / (double)n;
            Assert.IsTrue(threesProportion > 0.55 && threesProportion < 0.65 , "It is likely that the sampling methods is wrong"); 

        }

        private IList<int> GetDefaultSample(int n)
        {
            var walk = new RandomWalk<int, double>(values, probs);
            return walk.GetSample(n); 
        }
    }
}