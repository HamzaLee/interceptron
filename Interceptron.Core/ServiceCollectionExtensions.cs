using System;
using Microsoft.Extensions.DependencyInjection;

namespace Interceptron.Core
{
    public static class ServiceCollectionExtensions
    {
        #region Generic

        public static IServiceCollection Add<TService>(
            this IServiceCollection services,
            ServiceLifetime serviceLifetime,
            IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            TService ImplementationFactory(IServiceProvider serviceProvider) => ActivatorUtilities.CreateInstance<TService>(serviceProvider);

            return Add(services, ImplementationFactory, serviceLifetime, interceptors);
        }

        public static IServiceCollection Add<TService>(
            this IServiceCollection services,
            ServiceLifetime serviceLifetime,
            InterceptronProxyGenerationOptions proxyGenerationOptions,
            IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            TService ImplementationFactory(IServiceProvider serviceProvider) => ActivatorUtilities.CreateInstance<TService>(serviceProvider);

            return Add(services, ImplementationFactory, serviceLifetime, proxyGenerationOptions, interceptors);
        }

        public static IServiceCollection Add<TService, TImplementation>(
            this IServiceCollection services,
            ServiceLifetime serviceLifetime,
            IInterceptronInterceptor[] interceptors)
           where TService : class
           where TImplementation : class, TService
        {
            TImplementation ImplementationFactory(IServiceProvider serviceProvider) =>
                ActivatorUtilities.GetServiceOrCreateInstance<TImplementation>(serviceProvider);

            return Add<TService, TImplementation>(services, ImplementationFactory, serviceLifetime, interceptors);
        }

        public static IServiceCollection Add<TService, TImplementation>(
            this IServiceCollection services,
            ServiceLifetime serviceLifetime,
            InterceptronProxyGenerationOptions proxyGenerationOptions,
            IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            TImplementation ImplementationFactory(IServiceProvider serviceProvider) =>
                ActivatorUtilities.GetServiceOrCreateInstance<TImplementation>(serviceProvider);

            return Add<TService, TImplementation>(services, ImplementationFactory, serviceLifetime, proxyGenerationOptions, interceptors);
        }

        public static IServiceCollection Add<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory,
            ServiceLifetime serviceLifetime,
            IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            if (interceptors == null)
            {
                throw new ArgumentNullException(nameof(interceptors));
            }


            TService ProxyFactory(IInterceptronProxyGenerator generator, TService implementationInstance)
            {
                return generator.CreateClassProxy(implementationInstance, interceptors);
            }

            return Add(services, implementationFactory, serviceLifetime, ProxyFactory);
        }

        public static IServiceCollection Add<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory,
            ServiceLifetime serviceLifetime,
            InterceptronProxyGenerationOptions proxyGenerationOptions,
            IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            if (proxyGenerationOptions == null)
            {
                throw new ArgumentNullException(nameof(InterceptronProxyGenerationOptions));
            }

            if (interceptors == null)
            {
                throw new ArgumentNullException(nameof(interceptors));
            }

            TService ProxyFactory(IInterceptronProxyGenerator generator, TService implementationInstance) =>
                generator.CreateClassProxy(implementationInstance, proxyGenerationOptions, interceptors);

            return Add(services, implementationFactory, serviceLifetime, ProxyFactory);
        }

        public static IServiceCollection Add<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory,
            ServiceLifetime serviceLifetime,
            IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            if (interceptors == null)
            {
                throw new ArgumentNullException(nameof(interceptors));
            }

            TService ProxyFactory(IInterceptronProxyGenerator generator, TImplementation implementationInstance) =>
                generator.CreateInterfaceProxy<TService>(implementationInstance, interceptors);

            return Add(services, implementationFactory, serviceLifetime, ProxyFactory);
        }

        public static IServiceCollection Add<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory,
            ServiceLifetime serviceLifetime,
            InterceptronProxyGenerationOptions proxyGenerationOptions,
            IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            if (proxyGenerationOptions == null)
            {
                throw new ArgumentNullException(nameof(InterceptronProxyGenerationOptions));
            }

            if (interceptors == null)
            {
                throw new ArgumentNullException(nameof(interceptors));
            }

            TService ProxyFactory(IInterceptronProxyGenerator generator, TImplementation implementationInstance) =>
                generator.CreateInterfaceProxy<TService>(implementationInstance, proxyGenerationOptions, interceptors);

            return Add(services, implementationFactory, serviceLifetime, ProxyFactory);
        }

        public static IServiceCollection Add<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory,
            ServiceLifetime serviceLifetime,
            Func<IInterceptronProxyGenerator, TImplementation, TService> proxyFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            if (proxyFactory == null)
            {
                throw new ArgumentNullException(nameof(proxyFactory));
            }

            var serviceDescriptor = new ServiceDescriptor(
                typeof(TService),
                provider =>
                {
                    var implementationInstance = implementationFactory(provider);
                    if (implementationInstance == null)
                    {
                        throw new InterceptronProxyGeneratorException("The result of the implementation factory should not be null.");
                    }
                    
                    var generator = provider.GetService<IInterceptronProxyGenerator>();
                    if (generator == null)
                    {
                        throw new InterceptronGeneratorNotFoundException("Cannot resolve IGenerator, you should register a type that implements IGenerator.");
                    }

                    return proxyFactory(generator, implementationInstance);
                },
                serviceLifetime);

            services.Add(serviceDescriptor);
            return services;
        }

        #endregion

        #region AddTransient

        public static IServiceCollection AddTransient<TService>(this IServiceCollection services, IInterceptronInterceptor[] interceptors)
         where TService : class
        {
            return Add<TService>(services, ServiceLifetime.Transient, interceptors);
        }

        public static IServiceCollection AddTransient<TService>(this IServiceCollection services, InterceptronProxyGenerationOptions proxyGenerationOptions, IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            return Add<TService>(services, ServiceLifetime.Transient, proxyGenerationOptions, interceptors);
        }

        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services, IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Add<TService, TImplementation>(services, ServiceLifetime.Transient, interceptors);
        }

        public static IServiceCollection AddTransient<TService, TImplementation>(
            this IServiceCollection services,
            InterceptronProxyGenerationOptions proxyGenerationOptions,
            IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Add<TService, TImplementation>(services, ServiceLifetime.Transient, proxyGenerationOptions, interceptors);
        }

        public static IServiceCollection AddTransient<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory,
            IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            return Add(services, implementationFactory, ServiceLifetime.Transient, interceptors);
        }

        public static IServiceCollection AddTransient<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory,
            InterceptronProxyGenerationOptions proxyGenerationOptions,
            IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            return Add(services, implementationFactory, ServiceLifetime.Transient, proxyGenerationOptions, interceptors);
        }

        public static IServiceCollection AddTransient<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory,
            IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Add<TService, TImplementation>(services, implementationFactory, ServiceLifetime.Transient, interceptors);
        }

        public static IServiceCollection AddTransient<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory,
            InterceptronProxyGenerationOptions proxyGenerationOptions,
            IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Add<TService, TImplementation>(services, implementationFactory, ServiceLifetime.Transient, proxyGenerationOptions, interceptors);
        }

        #endregion

        #region AddScoped

        public static IServiceCollection AddScoped<TService>(this IServiceCollection services, IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            return Add<TService>(services, ServiceLifetime.Scoped, interceptors);
        }

        public static IServiceCollection AddScoped<TService>(this IServiceCollection services, InterceptronProxyGenerationOptions proxyGenerationOptions, IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            return Add<TService>(services, ServiceLifetime.Scoped, proxyGenerationOptions, interceptors);
        }

        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Add<TService, TImplementation>(services, ServiceLifetime.Scoped, interceptors);
        }

        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, InterceptronProxyGenerationOptions proxyGenerationOptions, IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Add<TService, TImplementation>(services, ServiceLifetime.Scoped, proxyGenerationOptions, interceptors);
        }

        public static IServiceCollection AddScoped<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory,
            IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            return Add(services, implementationFactory, ServiceLifetime.Scoped, interceptors);
        }

        public static IServiceCollection AddScoped<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory,
            InterceptronProxyGenerationOptions proxyGenerationOptions,
            IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            return Add(services, implementationFactory, ServiceLifetime.Scoped, proxyGenerationOptions, interceptors);
        }

        public static IServiceCollection AddScoped<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory,
            IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Add<TService, TImplementation>(services, implementationFactory, ServiceLifetime.Scoped, interceptors);
        }

        public static IServiceCollection AddScoped<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory,
            InterceptronProxyGenerationOptions proxyGenerationOptions,
            IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Add<TService, TImplementation>(services, implementationFactory, ServiceLifetime.Scoped, proxyGenerationOptions, interceptors);
        }

        #endregion

        #region AddSingleton

        public static IServiceCollection AddSingleton<TService>(this IServiceCollection services, IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            return Add<TService>(services, ServiceLifetime.Singleton, interceptors);
        }

        public static IServiceCollection AddSingleton<TService>(this IServiceCollection services, InterceptronProxyGenerationOptions proxyGenerationOptions, IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            return Add<TService>(services, ServiceLifetime.Singleton, proxyGenerationOptions, interceptors);
        }

        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Add<TService, TImplementation>(services, ServiceLifetime.Singleton, interceptors);
        }

        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, InterceptronProxyGenerationOptions proxyGenerationOptions, IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Add<TService, TImplementation>(services, ServiceLifetime.Singleton, proxyGenerationOptions, interceptors);
        }

        public static IServiceCollection AddSingleton<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory,
            IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            return Add(services, implementationFactory, ServiceLifetime.Singleton, interceptors);
        }

        public static IServiceCollection AddSingleton<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory,
            InterceptronProxyGenerationOptions proxyGenerationOptions,
            IInterceptronInterceptor[] interceptors)
            where TService : class
        {
            return Add(services, implementationFactory, ServiceLifetime.Singleton, proxyGenerationOptions, interceptors);
        }

        public static IServiceCollection AddSingleton<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory,
            IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Add<TService, TImplementation>(services, implementationFactory, ServiceLifetime.Singleton, interceptors);
        }

        public static IServiceCollection AddSingleton<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory,
            InterceptronProxyGenerationOptions proxyGenerationOptions,
            IInterceptronInterceptor[] interceptors)
            where TService : class
            where TImplementation : class, TService
        {
            return Add<TService, TImplementation>(services, implementationFactory, ServiceLifetime.Singleton, proxyGenerationOptions, interceptors);
        }

        #endregion
    }
}
