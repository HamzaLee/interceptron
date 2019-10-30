using System;
using System.Collections.Generic;
using System.Text;

namespace Interceptron.DispatchProxy.Sample
{
    public class SimpleDispatchProxyInterceptor : DispatchProxyInterceptor
    {
        public override object Intercept(DispatchProxyInvocation dispatchProxyInvocation)
        {
            Console.WriteLine($"+++ Intercept from DispatchProxy {dispatchProxyInvocation.Target.GetType().Name}");
            return dispatchProxyInvocation.Invoke();
        }
    }
}
