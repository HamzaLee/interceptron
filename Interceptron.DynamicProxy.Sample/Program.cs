using System;
using Interceptron.Core;
using Interceptron.Sample.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Interceptron.DynamicProxy.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var interceptronInterceptors = new IInterceptronInterceptor[] { new SimpleInterceptronInterceptor() };
            var dynamicProxyInterceptors = new[] { new SimpleDynamicProxyInterceptor().ToInterceptronInterceptor() };

            var services = new ServiceCollection();
            services.AddDynamicProxyGenerator();

            //AddTransient(services, interceptronInterceptors, dynamicProxyInterceptors);
            //AddScoped(services, interceptronInterceptors, dynamicProxyInterceptors);
            AddSingleton(services, interceptronInterceptors, dynamicProxyInterceptors);

            var serviceProvider = services.BuildServiceProvider();

            var customConfiguration = serviceProvider.GetService<CustomConfiguration>();
            var customConfigurationValue = customConfiguration.GetValue();
            Console.WriteLine($"CustomConfiguration : {customConfigurationValue}");

            var customController = serviceProvider.GetService<ICustomController>();
            var customControllerValue = customController.GetValue();
            Console.WriteLine($"CustomController : {customControllerValue}");
        }

        private static void AddTransient(IServiceCollection services, IInterceptronInterceptor[] interceptronInterceptors, IInterceptronInterceptor[] dynamicProxyInterceptors)
        {
            services.AddTransient<CustomContext>(dynamicProxyInterceptors);
            services.AddTransient(sp => new CustomConfiguration(), interceptronInterceptors);

            services.AddTransient<ICustomRepository, CustomRepository>(
                sp =>
                {
                    var customContext = sp.GetRequiredService<CustomContext>();
                    var customConfiguration = sp.GetRequiredService<CustomConfiguration>();
                    return new CustomRepository(customContext, customConfiguration);
                },
                interceptronInterceptors);
            services.AddTransient<ICustomService, CustomService>(interceptronInterceptors);
            services.AddTransient<ICustomController, CustomController>(interceptronInterceptors);
        }

        private static void AddScoped(IServiceCollection services, IInterceptronInterceptor[] interceptronInterceptors, IInterceptronInterceptor[] dynamicProxyInterceptors)
        {
            services.AddScoped<CustomContext>(dynamicProxyInterceptors);
            services.AddScoped(sp => new CustomConfiguration(), interceptronInterceptors);

            services.AddScoped<ICustomRepository, CustomRepository>(
                sp =>
                {
                    var customContext = sp.GetRequiredService<CustomContext>();
                    var customConfiguration = sp.GetRequiredService<CustomConfiguration>();
                    return new CustomRepository(customContext, customConfiguration);
                },
                interceptronInterceptors);
            services.AddScoped<ICustomService, CustomService>(interceptronInterceptors);
            services.AddScoped<ICustomController, CustomController>(interceptronInterceptors);
        }

        private static void AddSingleton(IServiceCollection services, IInterceptronInterceptor[] interceptronInterceptors, IInterceptronInterceptor[] dynamicProxyInterceptors)
        {
            services.AddSingleton<CustomContext>(dynamicProxyInterceptors);
            services.AddSingleton(sp => new CustomConfiguration(), interceptronInterceptors);

            services.AddSingleton<ICustomRepository, CustomRepository>(
                sp =>
                {
                    var customContext = sp.GetRequiredService<CustomContext>();
                    var customConfiguration = sp.GetRequiredService<CustomConfiguration>();
                    return new CustomRepository(customContext, customConfiguration);
                },
                interceptronInterceptors);
            services.AddSingleton<ICustomService, CustomService>(interceptronInterceptors);
            services.AddSingleton<ICustomController, CustomController>(interceptronInterceptors);
        }
    }
}
