using System.Reflection;
using Castle.DynamicProxy;
using Interceptron.Core;

namespace Interceptron.DynamicProxy.Adapters
{
    public class InterceptronInvocationAdapter : IInterceptronInvocation
    {

        public InterceptronInvocationAdapter(IInvocation invocation)
        {
            this.Invocation = invocation;
            this.Target = invocation.InvocationTarget;
            this.Arguments = invocation.Arguments;
            this.MethodInfo = invocation.Method;
        }

        public object Target { get; }

        public object[] Arguments { get; }

        public MethodInfo MethodInfo { get; }

        public object Invocation { get; }

        public object Invoke()
        {
            if (!(this.Invocation is IInvocation dynamicProxyInvocation))
            {
                throw new InterceptronInvocationException($"The invocation should be of type {nameof(IInvocation)}", typeof(IInvocation), this.Invocation.GetType());
            }

            dynamicProxyInvocation.Proceed();
            return dynamicProxyInvocation.ReturnValue;
        }
    }
}