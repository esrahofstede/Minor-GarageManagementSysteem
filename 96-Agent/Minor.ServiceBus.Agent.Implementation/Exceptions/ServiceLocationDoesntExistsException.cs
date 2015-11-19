using System;
using System.Runtime.Serialization;

namespace Minor.ServiceBus.Agent.Implementation
{
    [Serializable]
    public class ServiceLocationDoesntExistsException : Exception
    {
        public ServiceLocationDoesntExistsException()
        {
        }

        public ServiceLocationDoesntExistsException(string message) : base(message)
        {
        }

        public ServiceLocationDoesntExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServiceLocationDoesntExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}