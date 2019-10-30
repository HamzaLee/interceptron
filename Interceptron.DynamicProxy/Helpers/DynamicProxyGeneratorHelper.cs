using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using Interceptron.Core;
using Interceptron.DynamicProxy.Adapters;

namespace Interceptron.DynamicProxy.Helpers
{
    public static class DynamicProxyGeneratorHelper
    {
        public static IInterceptor[] ToDynamicProxyInterceptors(IEnumerable<IInterceptronInterceptor> interceptors)
        {
            if (interceptors == null)
            {
                throw new ArgumentNullException(nameof(interceptors));
            }

            return interceptors.Select(i => (IInterceptor)new DynamicProxyInterceptorAdapter(i)).ToArray();

        }
    }
}
