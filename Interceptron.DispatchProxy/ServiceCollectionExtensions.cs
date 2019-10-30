using Interceptron.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Interceptron.DispatchProxy
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDispatchProxyGenerator(this IServiceCollection services)
        {
            services.AddSingleton<IInterceptronProxyGenerator, DispatchProxyGenerator>();
        }
    }
}
