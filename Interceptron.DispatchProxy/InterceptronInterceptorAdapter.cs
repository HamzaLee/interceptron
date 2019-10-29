using System;
using Interceptron.Core;

namespace Interceptron.DispatchProxy
{
    public class InterceptronInterceptorAdapter : IInterceptronInterceptor
    {
        public InterceptronInterceptorAdapter(DispatchProxyInterceptor interceptor)
        {
            this.Interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        }

        public DispatchProxyInterceptor Interceptor { get; }

        public object Intercept(object invocation)
        {
            if (invocation == null)
            {
                throw new ArgumentNullException(nameof(invocation));
            }

            if (!(invocation is DispatchProxyInvocation dispatchProxyInvocation))
            {
                throw new InvalidCastException($"{invocation} parameter should be of type IInvocation.");
            }

            this.Interceptor.Target = dispatchProxyInvocation.Target;
            return Interceptor.Intercept(dispatchProxyInvocation);
        }
    }
}