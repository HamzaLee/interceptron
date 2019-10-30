using System;
using Interceptron.Core;
using Interceptron.Sample.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Interceptron.DispatchProxy.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var interceptronInterceptors = new IInterceptronInterceptor[] { new SimpleInterceptronInterceptor() };
            var dispatchProxyInterceptors = new[] { new SimpleDispatchProxyInterceptor().ToInterceptronInterceptor() };

            var services = new ServiceCollection();
            services.AddDispatchProxyGenerator();

            AddTransient(services, interceptronInterceptors, dispatchProxyInterceptors);
            AddScoped(services, interceptronInterceptors, dispatchProxyInterceptors);
            AddSingleton(services, interceptronInterceptors, dispatchProxyInterceptors);

            var serviceProvider = services.BuildServiceProvider();

            var customConfiguration = serviceProvider.GetService<ICustomConfiguration>();
            var customConfigurationValue = customConfiguration.GetValue();
            Console.WriteLine($"CustomConfiguration : {customConfigurationValue}");

            var customController = serviceProvider.GetService<ICustomController>();
            var customControllerValue = customController.GetValue();
            Console.WriteLine($"CustomController : {customControllerValue}");
        }

        private static void AddTransient(IServiceCollection services, IInterceptronInterceptor[] interceptronInterceptors, IInterceptronInterceptor[] dispatchProxyInterceptors)
        {
            services.AddTransient<ICustomContext, CustomContext>(dispatchProxyInterceptors);
            services.AddTransient<ICustomConfiguration, CustomConfiguration>(sp => new CustomConfiguration(), interceptronInterceptors);

            services.AddTransient<ICustomRepository, CustomRepository>(
                sp =>
                {
                    var customContext = sp.GetRequiredService<ICustomContext>();
                    var customConfiguration = sp.GetRequiredService<ICustomConfiguration>();
                    return new CustomRepository(customContext, customConfiguration);
                },
                interceptronInterceptors);
            services.AddTransient<ICustomService, CustomService>(interceptronInterceptors);
            services.AddTransient<ICustomController, CustomController>(interceptronInterceptors);
        }

        private static void AddScoped(IServiceCollection services, IInterceptronInterceptor[] interceptronInterceptors, IInterceptronInterceptor[] dispatchProxyInterceptors)
        {
            services.AddScoped<ICustomContext, CustomContext>(dispatchProxyInterceptors);
            services.AddScoped<ICustomConfiguration, CustomConfiguration>(sp => new CustomConfiguration(), interceptronInterceptors);

            services.AddScoped<ICustomRepository, CustomRepository>(
                sp =>
                {
                    var customContext = sp.GetRequiredService<ICustomContext>();
                    var customConfiguration = sp.GetRequiredService<ICustomConfiguration>();
                    return new CustomRepository(customContext, customConfiguration);
                },
                interceptronInterceptors);
            services.AddScoped<ICustomService, CustomService>(interceptronInterceptors);
            services.AddScoped<ICustomController, CustomController>(interceptronInterceptors);
        }

        private static void AddSingleton(IServiceCollection services, IInterceptronInterceptor[] interceptronInterceptors, IInterceptronInterceptor[] dispatchProxyInterceptors)
        {
            services.AddSingleton<ICustomContext, CustomContext>(dispatchProxyInterceptors);
            services.AddSingleton<ICustomConfiguration, CustomConfiguration>(sp => new CustomConfiguration(), interceptronInterceptors);

            services.AddSingleton<ICustomRepository, CustomRepository>(
                sp =>
                {
                    var customContext = sp.GetRequiredService<ICustomContext>();
                    var customConfiguration = sp.GetRequiredService<ICustomConfiguration>();
                    return new CustomRepository(customContext, customConfiguration);
                },
                interceptronInterceptors);
            services.AddSingleton<ICustomService, CustomService>(interceptronInterceptors);
            services.AddSingleton<ICustomController, CustomController>(interceptronInterceptors);
        }
    }
}
