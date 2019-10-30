using System;
using Interceptron.Core;
using Interceptron.DispatchProxy.DispatchProxyWrappers;

namespace Interceptron.DispatchProxy.Adapters
{
    public class InterceptronInterceptorAdapter : IInterceptronInterceptor
    {
        public InterceptronInterceptorAdapter(DispatchProxyInterceptor interceptor)
        {
            this.Interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        }

        public DispatchProxyInterceptor Interceptor { get; }

        public object Intercept(IInterceptronInvocation invocation)
        {
            if (invocation == null)
            {
                throw new ArgumentNullException(nameof(invocation));
            }

            if (!(invocation.Invocation is DispatchProxyInvocation dispatchProxyInvocation))
            {
                throw new InvalidCastException($"{invocation} parameter should be of type IInvocation.");
            }

            this.Interceptor.Target = dispatchProxyInvocation.Target;
            return Interceptor.Intercept(dispatchProxyInvocation);
        }
    }
}