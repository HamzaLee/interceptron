using System;
using System.Linq;
using Interceptron.Core;
using Interceptron.DispatchProxy.Adapters;

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

        // TODO : Improve code
        private static TService InterfaceProxyWithTarget<TService>(TService implementationInstance, IInterceptronInterceptor interceptor) where TService : class
        {
            TService proxy;
            if (interceptor is NativeInterceptronInterceptorAdapter nativeAdapter)
            {
                proxy = (TService)DispatchProxyUtils.CreateDispatchProxy(typeof(TService), nativeAdapter.Interceptor.GetType());
                var dispatchProxy = proxy as System.Reflection.DispatchProxy;
                nativeAdapter.TargetSetter(dispatchProxy)(implementationInstance);
            }
            else
            {
                proxy = System.Reflection.DispatchProxy.Create<TService, DispatcherProxyInterceptorAdapter>();
                if (proxy is DispatcherProxyInterceptorAdapter dispatcherProxyInterceptorAdapter)
                {
                    dispatcherProxyInterceptorAdapter.Interceptor = interceptor;
                    dispatcherProxyInterceptorAdapter.Target = implementationInstance;
                }
                else
                {
                    throw new DispatchProxyGeneratorException("Unable to create a proxy with DispatchProxy.");
                }
            }

            return proxy;
        }
    }
}
