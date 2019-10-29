using System;
using Castle.DynamicProxy;
using Interceptron.Core;

namespace Interceptron.DynamicProxy
{
    public class DynamicProxyInterceptorAdapter : IInterceptor
    {
        public DynamicProxyInterceptorAdapter(IInterceptronInterceptor interceptor)
        {
            Interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        }

        public IInterceptronInterceptor Interceptor { get; }

        public void Intercept(IInvocation invocation)
        {
            Interceptor.Intercept(invocation);
        }
    }
}