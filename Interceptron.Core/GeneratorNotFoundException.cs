using System;
using System.Runtime.Serialization;

namespace Interceptron.Core
{
    [Serializable]
    public class GeneratorNotFoundException : Exception
    {

        public GeneratorNotFoundException()
        {
        }

        public GeneratorNotFoundException(string message) : base(message)
        {
        }

        public GeneratorNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected GeneratorNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
