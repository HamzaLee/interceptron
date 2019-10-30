using System;
using Interceptron.Core;

namespace Interceptron.Sample.Shared
{
    public class SimpleInterceptronInterceptor : IInterceptronInterceptor
    {
        public object Intercept(IInterceptronInvocation invocation)
        {
            Console.WriteLine($"--- Intercept from Interceptron {invocation.Target.GetType().Name}");
            return invocation.Invoke();
        }
    }
}