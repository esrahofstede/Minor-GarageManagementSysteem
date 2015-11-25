using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Minor.Case2.PcSOnderhoud.Agent.Exceptions
{
    [Serializable]
    public class TechnicalException : Exception
    {
        public TechnicalException()
        {
        }

        public TechnicalException(string message) : base(message)
        {
        }

        public TechnicalException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TechnicalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}