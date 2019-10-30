using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace Interceptron.DispatchProxy.Sample
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
