namespace Interceptron.Core
{
    public interface IProxyGenerator
    {
        TService CreateClassProxy<TService>(TService implementationInstance, IInterceptor[] interceptors) where TService : class;
        
        TService CreateClassProxy<TService>(TService implementationInstance, ProxyGenerationOptions proxyGenerationOptions, IInterceptor[] interceptors) where TService : class;
        
        TService CreateInterfaceProxy<TService>(TService implementationInstance, IInterceptor[] interceptors) where TService : class;
        
        TService CreateInterfaceProxy<TService>(TService implementationInstance, ProxyGenerationOptions proxyGenerationOptions, IInterceptor[] interceptors) where TService : class;
    }
}
