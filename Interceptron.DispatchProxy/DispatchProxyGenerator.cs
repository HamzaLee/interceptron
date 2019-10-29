using System;
using System.Linq;
using Interceptron.Core;

namespace Interceptron.DispatchProxy
{
    public class DispatchProxyGenerator : IInterceptronProxyGenerator
    {
        public TService CreateClassProxy<TService>(TService implementationInstance, IInterceptronInterceptor[] interceptors) where TService : class
        {
            throw new NotSupportedException("DispatchProxy does not support the creation of class proxies.");
        }

        public TService CreateClassProxy<TService>(TService implementationInstance, InterceptronProxyGenerationOptions proxyGenerationOptions, IInterceptronInterceptor[] interceptors) where TService : class
        {
            throw new NotSupportedException("DispatchProxy does not support the creation of class proxies.");
        }

        public TService CreateInterfaceProxy<TService>(TService implementationInstance, IInterceptronInterceptor[] interceptors) where TService : class
        {
            return interceptors.Aggregate(implementationInstance, InterfaceProxyWithTarget);
        }

        public TService CreateInterfaceProxy<TService>(TService implementationInstance, InterceptronProxyGenerationOptions proxyGenerationOptions, IInterceptronInterceptor[] interceptors) where TService : class
        {
            return CreateInterfaceProxy(implementationInstance, interceptors);
        }

        private static TService InterfaceProxyWithTarget<TService>(TService implementationInstance, IInterceptronInterceptor interceptor) where TService : class
        {
            var proxy = System.Reflection.DispatchProxy.Create<TService, DispatcherProxyInterceptorAdapter>();
            var dispatcherProxyInterceptorAdapter = proxy as DispatcherProxyInterceptorAdapter;

            dispatcherProxyInterceptorAdapter.Interceptor = interceptor;
            dispatcherProxyInterceptorAdapter.Target = implementationInstance;

            return proxy;
        }
    }
}
