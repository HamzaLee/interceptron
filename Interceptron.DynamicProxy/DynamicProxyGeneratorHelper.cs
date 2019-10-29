using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using Interceptron.Core;

namespace Interceptron.DynamicProxy
{
    public static class DynamicProxyGeneratorHelper
    {
        public static IInterceptor[] ToDynamicProxyInterceptors(IEnumerable<IInterceptronInterceptor> interceptors)
        {
            if (interceptors == null)
            {
                throw new ArgumentNullException(nameof(interceptors));
            }

            return interceptors.Select(i => new DynamicProxyInterceptorAdapter(i)).ToArray();
        }
    }
}
