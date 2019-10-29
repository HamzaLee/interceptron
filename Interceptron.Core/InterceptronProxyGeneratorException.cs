using System;
using System.Runtime.Serialization;

namespace Interceptron.Core
{
    [Serializable]
    public class InterceptronProxyGeneratorException : Exception
    {
        public InterceptronProxyGeneratorException()
        {
        }

        public InterceptronProxyGeneratorException(string message) : base(message)
        {
        }

        public InterceptronProxyGeneratorException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InterceptronProxyGeneratorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
