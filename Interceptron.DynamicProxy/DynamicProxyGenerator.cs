using Castle.DynamicProxy;
using Interceptron.Core;
using Interceptron.DynamicProxy.Adapters;
using Interceptron.DynamicProxy.Helpers;

namespace Interceptron.DynamicProxy
{
    public class DynamicProxyGenerator : IInterceptronProxyGenerator
    {
        private readonly ProxyGenerator generator = new ProxyGenerator();

        public TService CreateClassProxy<TService>(TService implementationInstance, IInterceptronInterceptor[] interceptors) where TService : class
        {
            return generator.CreateClassProxyWithTarget(implementationInstance, DynamicProxyGeneratorHelper.ToDynamicProxyInterceptors(interceptors));
        }

        public TService CreateClassProxy<TService>(TService implementationInstance, InterceptronProxyGenerationOptions proxyGenerationOptions, IInterceptronInterceptor[] interceptors) where TService : class
        {
            return generator.CreateClassProxyWithTarget(implementationInstance, new DynamicProxyGenerationOptionAdapter(proxyGenerationOptions), DynamicProxyGeneratorHelper.ToDynamicProxyInterceptors(interceptors));
        }

        public TService CreateInterfaceProxy<TService>(TService implementationInstance, IInterceptronInterceptor[] interceptors) where TService : class
        {
            return generator.CreateInterfaceProxyWithTarget(implementationInstance, DynamicProxyGeneratorHelper.ToDynamicProxyInterceptors(interceptors));
        }

        public TService CreateInterfaceProxy<TService>(TService implementationInstance, InterceptronProxyGenerationOptions proxyGenerationOptions, IInterceptronInterceptor[] interceptors) where TService : class
        {
            return generator.CreateInterfaceProxyWithTarget(implementationInstance, new DynamicProxyGenerationOptionAdapter(proxyGenerationOptions), DynamicProxyGeneratorHelper.ToDynamicProxyInterceptors(interceptors));
        }
    }
}
