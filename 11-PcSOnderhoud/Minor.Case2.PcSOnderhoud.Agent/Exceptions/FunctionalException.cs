using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Case2.PcSOnderhoud.Agent.Exceptions
{
    public class FunctionalException : Exception
    {
        public FunctionalException(){}

        public FunctionalException(FunctionalErrorList errors)
        {
            Errors = errors;
        }
        public FunctionalErrorList Errors { get; set; }
    }
}
