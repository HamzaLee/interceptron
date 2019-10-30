using System.Reflection;
using Interceptron.Core;

namespace Interceptron.DispatchProxy.Adapters
{
    public class InterceptronInvocationAdapter : IInterceptronInvocation
    {

        public InterceptronInvocationAdapter(DispatchProxyInvocation invocation)
        {
            this.Invocation = invocation;
            this.Target = invocation.Target;
            this.Arguments = invocation.Arguments;
            this.MethodInfo = invocation.MethodInfo;
        }

        public object Target { get; }

        public object[] Arguments { get; }

        public MethodInfo MethodInfo { get; }

        public object Invocation { get; }

        public object Invoke()
        {
            if (!(this.Invocation is DispatchProxyInvocation dynamicProxyInvocation))
            {
                throw new InterceptronInvocationException($"The invocation should be of type {nameof(DispatchProxyInvocation)}", typeof(DispatchProxyInvocation), this.Invocation.GetType());
            }

            return dynamicProxyInvocation.Invoke();
        }
    }
}