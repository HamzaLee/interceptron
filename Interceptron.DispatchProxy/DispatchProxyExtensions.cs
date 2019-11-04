using System;
using System.Reflection;
using Interceptron.Core;
using Interceptron.DispatchProxy.Adapters;

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

        public static IInterceptronInterceptor ToInterceptronInterceptor(this System.Reflection.DispatchProxy interceptor, Func<System.Reflection.DispatchProxy, Action<object>> targetSetter)
        {
            if (interceptor == null)
            {
                throw new ArgumentNullException(nameof(interceptor));
            }

            return new NativeInterceptronInterceptorAdapter(interceptor, targetSetter);
        }
    }
   
}
