using System;
using Interceptron.Core;
using Interceptron.DispatchProxy.Adapters;
using Interceptron.DispatchProxy.DispatchProxyWrappers;

namespace Interceptron.DispatchProxy
{
    public static class DispatchProxyExtensions
    {
        public static IInterceptronInterceptor ToInterceptronInterceptor(this DispatchProxyInterceptor interceptor)
        {
            if (interceptor == null)
            {
                throw new ArgumentNullException(nameof(interceptor));
            }

            return new InterceptronInterceptorAdapter(interceptor);
        }
    }
}
