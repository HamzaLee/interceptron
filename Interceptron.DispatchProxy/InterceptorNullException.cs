using System;
using System.Runtime.Serialization;

namespace Interceptron.DispatchProxy
{
    [Serializable]
    public class InterceptorNullException : Exception
    {
        public InterceptorNullException()
        {
        }

        public InterceptorNullException(string message) : base(message)
        {
        }

        public InterceptorNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InterceptorNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}