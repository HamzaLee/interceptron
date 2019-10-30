using System;
using System.Runtime.Serialization;

namespace Interceptron.DispatchProxy
{
    [Serializable]
    public class InterceptronInterceptorNullException : Exception
    {
        public InterceptronInterceptorNullException()
        {
        }

        public InterceptronInterceptorNullException(string message) : base(message)
        {
        }

        public InterceptronInterceptorNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InterceptronInterceptorNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}