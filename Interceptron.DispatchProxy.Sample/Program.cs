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
            var interceptors = new IInterceptronInterceptor[] { new SimpleDispatchProxyInterceptor() };

            var services = new ServiceCollection();
            services.AddDispatchProxyGenerator();
            
            // AddTransient(services, interceptors);
            // AddScoped(services, interceptors);
            AddSingleton(services, interceptors);

            var serviceProvider = services.BuildServiceProvider();

            var customConfiguration = serviceProvider.GetService<ICustomConfiguration>();
            var customConfigurationValue = customConfiguration.GetValue();
            Console.WriteLine($"CustomConfiguration : {customConfigurationValue}");

            var customController = serviceProvider.GetService<ICustomController>();
            var customControllerValue = customController.GetValue();
            Console.WriteLine($"CustomController : {customControllerValue}");
        }

        private static void AddTransient(IServiceCollection services, IInterceptronInterceptor[] interceptors)
        {
            services.AddTransient<ICustomContext, CustomContext>(interceptors);
            services.AddTransient<ICustomConfiguration, CustomConfiguration>(sp => new CustomConfiguration(), interceptors);

            services.AddTransient<ICustomRepository, CustomRepository>(
                sp =>
                {
                    var customContext = sp.GetRequiredService<ICustomContext>();
                    var customConfiguration = sp.GetRequiredService<ICustomConfiguration>();
                    return new CustomRepository(customContext, customConfiguration);
                },
                interceptors);
            services.AddTransient<ICustomService, CustomService>(interceptors);
            services.AddTransient<ICustomController, CustomController>(interceptors);
        }

        private static void AddScoped(IServiceCollection services, IInterceptronInterceptor[] interceptors)
        {
            services.AddScoped<ICustomContext, CustomContext>(interceptors);
            services.AddScoped<ICustomConfiguration, CustomConfiguration>(sp => new CustomConfiguration(), interceptors);

            services.AddScoped<ICustomRepository, CustomRepository>(
                sp =>
                {
                    var customContext = sp.GetRequiredService<ICustomContext>();
                    var customConfiguration = sp.GetRequiredService<ICustomConfiguration>();
                    return new CustomRepository(customContext, customConfiguration);
                },
                interceptors);
            services.AddScoped<ICustomService, CustomService>(interceptors);
            services.AddScoped<ICustomController, CustomController>(interceptors);
        }

        private static void AddSingleton(IServiceCollection services, IInterceptronInterceptor[] interceptors)
        {
            services.AddSingleton<ICustomContext, CustomContext>(interceptors);
            services.AddSingleton<ICustomConfiguration, CustomConfiguration>(sp => new CustomConfiguration(), interceptors);

            services.AddSingleton<ICustomRepository, CustomRepository>(
                sp =>
                {
                    var customContext = sp.GetRequiredService<ICustomContext>();
                    var customConfiguration = sp.GetRequiredService<ICustomConfiguration>();
                    return new CustomRepository(customContext, customConfiguration);
                },
                interceptors);
            services.AddSingleton<ICustomService, CustomService>(interceptors);
            services.AddSingleton<ICustomController, CustomController>(interceptors);
        }
    }
}
