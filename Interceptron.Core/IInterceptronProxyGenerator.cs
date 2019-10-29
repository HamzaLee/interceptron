namespace Interceptron.Core
{
    public interface IInterceptronProxyGenerator
    {
        TService CreateClassProxy<TService>(TService implementationInstance, IInterceptronInterceptor[] interceptors) where TService : class;
        
        TService CreateClassProxy<TService>(TService implementationInstance, InterceptronProxyGenerationOptions proxyGenerationOptions, IInterceptronInterceptor[] interceptors) where TService : class;
        
        TService CreateInterfaceProxy<TService>(TService implementationInstance, IInterceptronInterceptor[] interceptors) where TService : class;
        
        TService CreateInterfaceProxy<TService>(TService implementationInstance, InterceptronProxyGenerationOptions proxyGenerationOptions, IInterceptronInterceptor[] interceptors) where TService : class;
    }
}
