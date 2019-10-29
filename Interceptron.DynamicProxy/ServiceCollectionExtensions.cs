using Interceptron.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Interceptron.DynamicProxy
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDynamicProxyGenerator(this IServiceCollection services)
        {
            services.AddSingleton<IInterceptronProxyGenerator, DynamicProxyGenerator>();
        }
    }
}
