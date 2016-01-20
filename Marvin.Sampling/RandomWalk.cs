using System;
using System.Collections.Generic;
using Toolkit;

namespace Marvin.Sampling
{
    public class RandomWalk<TValues, TProbs> where TProbs: IComparable
    {
        private IList<TValues> values;
        private IList<TProbs> probs;
        private RandomNumberGenerator rng; 

        public RandomWalk(IList<TValues> values, IList<TProbs> probabilities)
        {
            this.values = values;
            this.probs = new OutOfBoundAccessDecorator<TProbs>(probabilities);
            rng = new RandomNumberGenerator(); 
        }

        public IList<TValues> GetSample(int n)
        {
            var sample = new List<TValues>();
            int index = values.Count / 2; 

            for(int i=0; i<n; i++)
            {
                int proposedIndex = rng.RandomTruth() ? index - 1 : index + 1;
                dynamic pCurrent = probs[index];
                dynamic pProposed = probs[proposedIndex];
                dynamic pMove = Math.Min(pProposed / pCurrent, 1.0);
                var shouldMove = pMove >= rng.RandomNumber();
                index = shouldMove ? proposedIndex : index;
                var pickedValue = values[index];
                sample.Add(pickedValue); 
            }

            return sample;   
        }

        
    }
}
