using System;
using Castle.DynamicProxy;
using Interceptron.Core;
using Interceptron.DynamicProxy.Adapters;

namespace Interceptron.DynamicProxy
{
    public static class DynamicProxyExtensions
    {
        public static IInterceptronInterceptor ToInterceptronInterceptor(this IInterceptor interceptor)
        {
            if (interceptor == null)
            {
                throw new ArgumentNullException(nameof(interceptor));
            }

            return new InterceptronInterceptorAdapter(interceptor);
        }
    }
}
