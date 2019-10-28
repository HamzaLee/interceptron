using System;
using System.Runtime.Serialization;

namespace Interceptron.Core
{
    [Serializable]
    public class ProxyGeneratorException : Exception
    {
        public ProxyGeneratorException()
        {
        }

        public ProxyGeneratorException(string message) : base(message)
        {
        }

        public ProxyGeneratorException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ProxyGeneratorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
