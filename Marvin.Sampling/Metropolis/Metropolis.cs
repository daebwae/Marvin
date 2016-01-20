using DataIO;
using System;
using System.Collections.Generic;
using Toolkit;
using static System.Math; 

namespace Marvin.Sampling.Metropolis
{
    public class Metropolis
    {
        private Func<List<double>, double> probability;
        private ParameterRoot parameters;
        private RandomNumberGenerator rng;

        public Metropolis(Func<List<double>, double> probability, ParameterRoot parameters, RandomNumberGenerator rng)
        {
            this.probability = probability;
            this.parameters = parameters;
            this.rng = rng; 
        }


        public Data Sample(int n)
        {
            var data = new Data();

            var currentVector = parameters.Current; 
            var currentProbability = probability(currentVector); 

            for(int i=0; i<n; i++)
            {
                var proposedVector = parameters.ProposeNewVector();
                var proposedProbability = probability(proposedVector);

                var pMove = Min(1.0, proposedProbability / currentProbability);
                var shouldMove = rng.RandomNumber() < pMove; 

                if(shouldMove)
                {
                    currentVector = parameters.UpdateToProposedValue();
                    currentProbability = probability(currentVector); 
                }

                data.Add(currentVector); 
            }

            return data; 
        }


        
    }
}
