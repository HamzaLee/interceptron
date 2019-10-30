using System;
using System.Reflection;
using Interceptron.Core;

namespace Interceptron.DispatchProxy
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
                throw new ArgumentNullException();
            }

            return dynamicProxyInvocation.Invoke();
        }
    }
}