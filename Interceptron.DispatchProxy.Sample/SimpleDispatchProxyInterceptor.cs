using System;
using Interceptron.Core;

namespace Interceptron.DispatchProxy.Sample
{
    internal class SimpleDispatchProxyInterceptor : IInterceptronInterceptor
    {
        public object Intercept(object invocation)
        {
            var dynamicProxyInvocation = (DispatchProxyInvocation)invocation;
            Console.WriteLine($"--- Intercept {dynamicProxyInvocation.Target.GetType().Name}");
            return dynamicProxyInvocation.Invoke();
        }
    }
}