using System.Reflection;

namespace Interceptron.Core
{
    public interface IInterceptronInvocation
    {
        object Target { get; }

        object[] Arguments { get; }

        MethodInfo MethodInfo { get; }

        object Invocation { get; }

        object Invoke();
    }
}