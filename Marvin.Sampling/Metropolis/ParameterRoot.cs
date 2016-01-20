using System.Collections.Generic;
using System.Linq;

namespace Marvin.Sampling.Metropolis
{
    public class ParameterRoot: List<Parameter>
    {
        public List<double> ProposedVector
        {
            get
            {
                return this.Select(p => p.ProposedValue).ToList(); 
            }
        }

        public List<double> Current
        {
            get
            {
                return this.Select(p => p.Value).ToList(); 
            }
        }

        public List<double> ProposeNewVector()
        {
            // ToList() is to make sure that the Vector is actually calculated
            // I don't want the calculation to occur when the Enumerable is iterated
            return this.Select(p => p.ProposeNewValue().ProposedValue).ToList(); 
        }

        public List<double> UpdateToProposedValue()
        {
            // ToList() is to make sure that the Vector is actually updated
            // I don't want the calculation to occur when the Enumerable is iterated
            return this.Select(p => p.UpdateToProposedValue().Value).ToList(); 
        }
    }
}
