using System;
using Interceptron.Core;

namespace Interceptron.DispatchProxy.Sample
{
    internal class SimpleDispatchProxyInterceptor : IInterceptronInterceptor
    {
        public object Intercept(IInterceptronInvocation invocation)
        {
            Console.WriteLine($"--- Intercept {invocation.Target.GetType().Name}");
            return invocation.Invoke();
        }
    }
}