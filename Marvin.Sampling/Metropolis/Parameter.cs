using Toolkit;

namespace Marvin.Sampling.Metropolis
{
    public class Parameter
    {
        private double value;
        private double mean;
        private double variance;

        private double proposedValue;
        private RandomNumberGenerator rng;

        public double ProposedValue
        {
            get { return proposedValue;  }
            private set { proposedValue = value; }
        }

        public double Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public double Mean
        {
            get
            {
                return mean;
            }

            set
            {
                mean = value;
            }
        }

        public double Variance
        {
            get
            {
                return variance;
            }

            set
            {
                variance = value;
            }
        }

        public Parameter(double value, double variance, RandomNumberGenerator rng = null)
        {
            this.value = value;
            this.variance = variance;
            this.rng = rng ?? new RandomNumberGenerator(); 
        }

        public Parameter ProposeNewValue()
        {
            ProposedValue = rng.RandomBoxMueller(Value, Variance);

            return this; 
        }

        public Parameter UpdateToProposedValue()
        {
            Value = ProposedValue;
            return this; 
        }
    }
}
