using System;
using System.Runtime.Serialization;


namespace Interceptron.Core
{
    [Serializable]
    public class InterceptronInvocationException : Exception
    {
        public InterceptronInvocationException()
        {
        }

        public InterceptronInvocationException(string message) : base(message)
        {
        }

        public InterceptronInvocationException(string message, Exception inner) : base(message, inner)
        {
        }

        public InterceptronInvocationException(string message, Type targetType, Type actualType) : base(message)
        {
            this.TargetType = targetType;
            this.ActualType = actualType;
        }

        protected InterceptronInvocationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public Type ActualType { get; set; }

        public Type TargetType { get; set; }
    }
}
