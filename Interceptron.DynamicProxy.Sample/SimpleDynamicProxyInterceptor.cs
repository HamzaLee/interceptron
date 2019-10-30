using System;
using Castle.DynamicProxy;
using Interceptron.Core;

namespace Interceptron.DynamicProxy.Sample
{
    public class SimpleDynamicProxyInterceptor : IInterceptronInterceptor
    {
        public object Intercept(IInterceptronInvocation invocation)
        {
            Console.WriteLine($"--- Intercept {invocation.Target.GetType().Name}");
            return invocation.Invoke();
        }
    }
}