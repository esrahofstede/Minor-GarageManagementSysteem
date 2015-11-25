using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Minor.Case2.PcSOnderhoud.Agent.Exceptions
{
    [Serializable]
    public class TechnicalException : Exception
    {
        private IDictionary data;
        private Exception innerException;
        private string message;

        public TechnicalException()
        {
        }

        public TechnicalException(string message) : base(message)
        {
        }

        public TechnicalException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public TechnicalException(IDictionary data, string message, Exception innerException)
        {
            this.data = data;
            this.message = message;
            this.innerException = innerException;
        }

        protected TechnicalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}