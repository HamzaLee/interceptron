using System;
using Castle.DynamicProxy;
using Interceptron.Core;

namespace Interceptron.DynamicProxy.Sample
{
    public class SimpleDynamicProxyInterceptor : IInterceptronInterceptor
    {
        public object Intercept(object invocation)
        {
            var dynamicProxyInvocation = (IInvocation)invocation;
            Console.WriteLine($"--- Intercept {dynamicProxyInvocation.TargetType.Name}");
            dynamicProxyInvocation.Proceed();
            return dynamicProxyInvocation.ReturnValue;
        }
    }
}