using System;
using Castle.DynamicProxy;
using Interceptron.Core;

namespace Interceptron.DynamicProxy
{
    public class InterceptronInterceptorAdapter : IInterceptronInterceptor
    {
        public InterceptronInterceptorAdapter(IInterceptor interceptor)
        {
            this.Interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        }

        private IInterceptor Interceptor { get; }

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

            this.Interceptor.Intercept(dynamicProxyInvocation);
            return dynamicProxyInvocation.ReturnValue;
        }
    }
}