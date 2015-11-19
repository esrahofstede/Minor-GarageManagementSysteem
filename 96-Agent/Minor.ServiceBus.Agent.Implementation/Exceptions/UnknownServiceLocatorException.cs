using System;
using System.Runtime.Serialization;

namespace Minor.ServiceBus.Agent.Implementation
{
    [Serializable]
    public class UnknownServiceLocatorException : Exception
    {
        public UnknownServiceLocatorException()
        {
        }

        public UnknownServiceLocatorException(string message) : base(message)
        {
        }

        public UnknownServiceLocatorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownServiceLocatorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}