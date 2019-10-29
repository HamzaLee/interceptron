using System;
using Castle.DynamicProxy;
using Interceptron.Core;

namespace Interceptron.DynamicProxy
{
    public class InterceptronInterceptorAdapter : IInterceptronInterceptor
    {
        private readonly IInterceptor interceptor;

        public InterceptronInterceptorAdapter(IInterceptor interceptor)
        {
            this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        }

        public object Intercept(object invocation)
        {
            if (invocation == null)
            {
                throw new ArgumentNullException(nameof(invocation));
            }

            if (!(invocation is IInvocation dynamicProxyInvocation))
            {
                throw new InvalidCastException($"{invocation} parameter should be of type IInvocation.");
            }

            interceptor.Intercept(dynamicProxyInvocation);
            return dynamicProxyInvocation.ReturnValue;
        }
    }
}