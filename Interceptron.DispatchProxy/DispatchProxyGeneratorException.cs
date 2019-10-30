using System;
using System.Runtime.Serialization;

namespace Interceptron.DispatchProxy
{
    [Serializable]
    internal class DispatchProxyGeneratorException : Exception
    {
        public DispatchProxyGeneratorException()
        {
        }

        public DispatchProxyGeneratorException(string message) : base(message)
        {
        }

        public DispatchProxyGeneratorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DispatchProxyGeneratorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}