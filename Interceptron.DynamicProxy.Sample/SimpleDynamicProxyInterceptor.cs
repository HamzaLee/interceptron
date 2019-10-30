using System;
using Castle.DynamicProxy;

namespace Interceptron.DynamicProxy.Sample
{
    public class SimpleDynamicProxyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"+++ Intercept from DynamicProxy {invocation.TargetType.Name}");
            invocation.Proceed();
        }
    }
}
