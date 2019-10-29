using System;
using System.Runtime.Serialization;

namespace Interceptron.Core
{
    [Serializable]
    public class InterceptronGeneratorNotFoundException : Exception
    {

        public InterceptronGeneratorNotFoundException()
        {
        }

        public InterceptronGeneratorNotFoundException(string message) : base(message)
        {
        }

        public InterceptronGeneratorNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InterceptronGeneratorNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
