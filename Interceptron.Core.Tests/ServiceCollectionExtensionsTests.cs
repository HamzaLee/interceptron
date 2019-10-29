using System;
using Interceptron.Tests;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Interceptron.Core.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        #region Private types

        private enum ProxyTargetType
        {
            /// <summary>
            /// Choose this when the proxy's target is an interface.
            /// </summary>
            Interface,

            /// <summary>
            ///  Choose this when the proxy's target is a class.
            /// </summary>
            Class
        }

        #endregion

        #region Add

        [Test]
        public void Add()
        {
            var services = new ServiceCollection();
            TestService ImplementationFactory(IServiceProvider sp) => new TestService();
            ITestService ProxyFactory(IInterceptronProxyGenerator generator, TestService s) => new TestProxyService(s);
            var generatorMock = new Mock<IInterceptronProxyGenerator>();
            services.AddSingleton(sp => generatorMock.Object);

            services.Add(ImplementationFactory, ServiceLifetime.Transient, ProxyFactory);

            var serviceProvider = services.BuildServiceProvider();
            var service = (TestProxyService)serviceProvider.GetService<ITestService>();

            Assert.IsNotNull(service);
            var testService = service.GetTarget();
            Assert.IsNotNull(testService);
            Assert.IsInstanceOf<TestService>(testService);
        }

        [Test]
        public void Add_WhenServicesIsNull_ThenThrowArgumentNullException()
        {
            TestService ImplementationFactory(IServiceProvider sp) => new TestService();
            ITestService ProxyFactory(IInterceptronProxyGenerator generator, TestService s) => new TestProxyService(s);

            Assert.Throws<ArgumentNullException>(() =>
                ServiceCollectionExtensions.Add(
                  null,
                  ImplementationFactory,
                  ServiceLifetime.Transient,
                  ProxyFactory));
        }

        [Test]
        public void Add_WhenImplementationFactoryIsNull_ThenThrowArgumentNullException()
        {
            var services = new ServiceCollection();
            ITestService ProxyFactory(IInterceptronProxyGenerator generator, TestService s) => new TestProxyService(s);

            Assert.Throws<ArgumentNullException>(() =>
                services.Add<ITestService, TestService>(
                    null,
                    ServiceLifetime.Transient,
                    ProxyFactory));
        }

        [Test]
        public void Add_WhenProxyFactoryIsNull_ThenThrowArgumentNullException()
        {
            var services = new ServiceCollection();
            TestService ImplementationFactory(IServiceProvider sp) => new TestService();

            Assert.Throws<ArgumentNullException>(() =>
                services.Add<ITestService, TestService>(
                    ImplementationFactory,
                    ServiceLifetime.Transient,
                    proxyFactory: null));
        }

        [Test]
        public void Add_WhenImplementationReturnsNull_ThenThrowProxyGeneratorException()
        {
            var services = new ServiceCollection();
            TestService ImplementationFactory(IServiceProvider sp) => null;
            ITestService ProxyFactory(IInterceptronProxyGenerator generator, TestService s) => new TestProxyService(s);

            services.Add(ImplementationFactory, ServiceLifetime.Transient, ProxyFactory);

            var serviceProvider = services.BuildServiceProvider();
            Assert.Throws<InterceptronProxyGeneratorException>(() => serviceProvider.GetService<ITestService>());
        }

        [Test]
        public void Add_WhenGeneratorIsNull_ThenThrowGeneratorNotFoundException()
        {
            var services = new ServiceCollection();
            TestService ImplementationFactory(IServiceProvider sp) => new TestService();
            ITestService ProxyFactory(IInterceptronProxyGenerator generator, TestService s) => new TestProxyService(s);

            services.Add(ImplementationFactory, ServiceLifetime.Transient, ProxyFactory);

            var serviceProvider = services.BuildServiceProvider();
            Assert.Throws<InterceptronGeneratorNotFoundException>(() => serviceProvider.GetService<ITestService>());
        }

        #endregion

        #region Interface resolution

        #region AddProxyWithImplementationFactoryAndOptionsForInterfaceResolution

        [Test]
        public void AddProxyWithImplementationFactoryAndOptionsForInterfaceResolution()
        {
            var (services, ImplementationFactory, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Interface);
            services.Add<ITestService, TestService>(
                ImplementationFactory,
                ServiceLifetime.Transient,
                proxyGenerationOptions,
                interceptors);

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetService<ITestService>();

            Assert.IsNotNull(service);
            Assert.IsInstanceOf<ITestService>(service);

            AssertTestProxyService(service, interceptors);
        }

        [Test]
        public void AddProxyWithImplementationFactoryAndOptionsForInterfaceResolution_WhenProxyGenerationOptionsIsNull_ThenThrowException()
        {
            var services = new ServiceCollection();
            TestService ImplementationFactory(IServiceProvider sp) => new TestService();
            var interceptorMock = new Mock<IInterceptronInterceptor>();
            IInterceptronInterceptor[] interceptors = { interceptorMock.Object };

            Assert.Throws<ArgumentNullException>(() => services.Add<ITestService, TestService>(
                ImplementationFactory,
                ServiceLifetime.Transient,
                null,
                interceptors));
        }

        [Test]
        public void AddProxyWithImplementationFactoryAndOptionsForInterfaceResolution_WhenInterceptorsIsNull_ThenThrowException()
        {
            var services = new ServiceCollection();
            TestService ImplementationFactory(IServiceProvider sp) => new TestService();
            var proxyGenerationOptions = new InterceptronProxyGenerationOptions();

            Assert.Throws<ArgumentNullException>(() => services.Add<ITestService, TestService>(
                ImplementationFactory,
                ServiceLifetime.Transient,
                proxyGenerationOptions,
                null));
        }

        #endregion

        #region AddProxyWithImplementationFactoryAndWithoutOptionsForInterfaceResolution

        [Test]
        public void AddProxyWithImplementationFactoryAndWithoutOptionsForInterfaceResolution()
        {
            var (services, ImplementationFactory, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Interface);

            services.Add<ITestService, TestService>(
                ImplementationFactory,
                ServiceLifetime.Transient,
                interceptors);

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetService<ITestService>();

            Assert.IsNotNull(service);
            Assert.IsInstanceOf<ITestService>(service);

            AssertTestProxyService(service, interceptors);
        }

        [Test]
        public void AddProxyWithImplementationFactoryAndWithoutOptionsForInterfaceResolution_WhenInterceptorsIsNull_ThenThrowException()
        {
            var services = new ServiceCollection();
            TestService ImplementationFactory(IServiceProvider sp) => new TestService();

            Assert.Throws<ArgumentNullException>(() => services.Add<ITestService, TestService>(
                ImplementationFactory,
                ServiceLifetime.Transient,
                interceptors: null));
        }

        #endregion

        #region AddProxyWithOptionsForInterfaceResolution

        [Test]
        public void AddProxyWithOptionsForInterfaceResolution()
        {
            var (services, _, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Interface);

            services.Add<ITestService, TestService>(
                ServiceLifetime.Transient,
                proxyGenerationOptions,
                interceptors);

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetService<ITestService>();

            Assert.IsNotNull(service);
            Assert.IsInstanceOf<ITestService>(service);

            AssertTestProxyService(service, interceptors);
        }

        #endregion

        #region AddProxyWithoutOptionsForInterfaceResolution

        [Test]
        public void AddProxyWithoutOptionsForInterfaceResolution()
        {
            var (services, _, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Interface);

            services.Add<ITestService, TestService>(
                ServiceLifetime.Transient,
                interceptors);

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetService<ITestService>();

            Assert.IsNotNull(service);
            Assert.IsInstanceOf<ITestService>(service);

            AssertTestProxyService(service, interceptors);
        }


        #endregion

        #endregion

        #region Class resolution

        #region AddProxyWithImplementationFactoryAndOptionsForClassResolution

        [Test]
        public void AddProxyWithImplementationFactoryAndOptionsForClassResolution()
        {
            var (services, ImplementationFactory, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Class);

            services.Add(
                ImplementationFactory,
                ServiceLifetime.Transient,
                proxyGenerationOptions,
                interceptors);

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetService<TestService>();

            Assert.IsNotNull(service);
            Assert.IsInstanceOf<IProxy<ITestService>>(service);
            AssertTestProxyService(service, interceptors);

        }

        [Test]
        public void AddProxyWithImplementationFactoryAndOptionsForClassResolution_WhenProxyGenerationOptionsIsNull_ThenThrowArgumentNullException()
        {
            var (services, ImplementationFactory, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Class);

            Assert.Throws<ArgumentNullException>(() => services.Add(
                ImplementationFactory,
                ServiceLifetime.Transient,
                null,
                interceptors));
        }

        [Test]
        public void AddProxyWithImplementationFactoryAndOptionsForClassResolution_WhenInterceptorsIsNull_ThenThrowArgumentNullException()
        {
            var (services, ImplementationFactory, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Class);

            Assert.Throws<ArgumentNullException>(() => services.Add(
                ImplementationFactory,
                ServiceLifetime.Transient,
                proxyGenerationOptions,
                null));
        }

        #endregion

        #region AddProxyWithImplementationFactoryAndWithoutOptionsForClassResolution

        [Test]
        public void AddProxyWithImplementationFactoryAndWithoutOptionsForClassResolution()
        {
            var (services, ImplementationFactory, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Class);
            services.Add(ImplementationFactory, ServiceLifetime.Transient, interceptors);

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetService<TestService>();

            Assert.IsNotNull(service);
            Assert.IsInstanceOf<TestService>(service);

            AssertTestProxyService(service, interceptors);
        }

        [Test]
        public void AddProxyWithImplementationFactoryAndWithoutOptionsForClassResolution_WhenInterceptorsIsNull_ThenThrowArgumentNullException()
        {
            var services = new ServiceCollection();
            TestService ImplementationFactory(IServiceProvider sp) => new TestService();

            Assert.Throws<ArgumentNullException>(() => services.Add(
                ImplementationFactory,
                ServiceLifetime.Transient,
                null));
        }

        #endregion

        #region AddProxyWithOptionsForClassResolution

        [Test]
        public void AddProxyWithOptionsForClassResolution()
        {
            var (services, _, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Class);

            services.Add<TestService>(
                ServiceLifetime.Transient,
                proxyGenerationOptions,
                interceptors);

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetService<TestService>();

            Assert.IsNotNull(service);
            Assert.IsInstanceOf<TestService>(service);

            AssertTestProxyService(service, interceptors);
        }

        #endregion

        #region AddProxyWithoutOptionsForClassResolution

        [Test]
        public void AddProxyWithoutOptionsForClassResolution()
        {
            var (services, _, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Class);

            services.Add<TestService>(
                ServiceLifetime.Transient,
                interceptors);

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetService<TestService>();

            Assert.IsNotNull(service);
            Assert.IsInstanceOf<TestService>(service);

            AssertTestProxyService(service, interceptors);
        }

        #endregion

        #endregion

        #region AddTransient

        [Test]
        public void AddTransientWithoutImplementationFactoryAndOptionsForClass()
        {
            var (services, _, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Class);

            services.AddTransient<TestService>(interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Transient);
        }

        [Test]
        public void AddTransientWithoutImplementationFactoryAndWithOptionsForClass()
        {
            var (services, _, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Class);

            services.AddTransient<TestService>(proxyGenerationOptions, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Transient);
        }

        [Test]
        public void AddTransientWithoutImplementationFactoryAndOptionsForInterface()
        {
            var (services, _, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Interface);

            services.AddTransient<ITestService, TestService>(interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Transient);
        }

        [Test]
        public void AddTransientWithoutImplementationFactoryAndWithOptionsForInterface()
        {
            var (services, _, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Interface);

            services.AddTransient<ITestService, TestService>(proxyGenerationOptions, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Transient);
        }

        [Test]
        public void AddTransientWithImplementationFactoryAndWithoutOptionsForClass()
        {
            var (services, ImplementationFactory, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Class);

            services.AddTransient(ImplementationFactory, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Transient);
        }

        [Test]
        public void AddTransientWithImplementationFactoryAndOptionsForClass()
        {
            var (services, ImplementationFactory, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Class);

            services.AddTransient(ImplementationFactory, proxyGenerationOptions, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Transient);
        }

        [Test]
        public void AddTransientWithImplementationFactoryAndWithoutOptionsForInterface()
        {
            var (services, ImplementationFactory, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Interface);

            services.AddTransient<ITestService, TestService>(ImplementationFactory, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Transient);
        }

        [Test]
        public void AddTransientWithImplementationFactoryAndOptionsForInterface()
        {
            var (services, ImplementationFactory, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Interface);

            services.AddTransient<ITestService, TestService>(ImplementationFactory, proxyGenerationOptions, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Transient);
        }

        #endregion

        #region AddScoped

        [Test]
        public void AddScopedWithoutImplementationFactoryAndOptionsForClass()
        {
            var (services, _, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Class);

            services.AddScoped<TestService>(interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Scoped);
        }

        [Test]
        public void AddScopedWithoutImplementationFactoryAndWithOptionsForClass()
        {
            var (services, _, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Class);

            services.AddScoped<TestService>(proxyGenerationOptions, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Scoped);
        }

        [Test]
        public void AddScopedWithoutImplementationFactoryAndOptionsForInterface()
        {
            var (services, _, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Interface);

            services.AddScoped<ITestService, TestService>(interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Scoped);
        }

        [Test]
        public void AddScopedWithoutImplementationFactoryAndWithOptionsForInterface()
        {
            var (services, _, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Interface);

            services.AddScoped<ITestService, TestService>(proxyGenerationOptions, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Scoped);
        }

        [Test]
        public void AddScopedWithImplementationFactoryAndWithoutOptionsForClass()
        {
            var (services, ImplementationFactory, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Class);

            services.AddScoped(ImplementationFactory, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Scoped);
        }

        [Test]
        public void AddScopedWithImplementationFactoryAndOptionsForClass()
        {
            var (services, ImplementationFactory, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Class);

            services.AddScoped(ImplementationFactory, proxyGenerationOptions, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Scoped);
        }

        [Test]
        public void AddScopedWithImplementationFactoryAndWithoutOptionsForInterface()
        {
            var (services, ImplementationFactory, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Interface);

            services.AddScoped<ITestService, TestService>(ImplementationFactory, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Scoped);
        }

        [Test]
        public void AddScopedWithImplementationFactoryAndOptionsForInterface()
        {
            var (services, ImplementationFactory, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Interface);

            services.AddScoped<ITestService, TestService>(ImplementationFactory, proxyGenerationOptions, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Scoped);
        }

        #endregion

        #region AddSingleton

        [Test]
        public void AddSingletonWithoutImplementationFactoryAndOptionsForClass()
        {
            var (services, _, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Class);

            services.AddSingleton<TestService>(interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Singleton);
        }

        [Test]
        public void AddSingletonWithoutImplementationFactoryAndWithOptionsForClass()
        {
            var (services, _, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Class);

            services.AddSingleton<TestService>(proxyGenerationOptions, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Singleton);
        }

        [Test]
        public void AddSingletonWithoutImplementationFactoryAndOptionsForInterface()
        {
            var (services, _, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Interface);

            services.AddSingleton<ITestService, TestService>(interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Singleton);
        }

        [Test]
        public void AddSingletonWithoutImplementationFactoryAndWithOptionsForInterface()
        {
            var (services, _, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Interface);

            services.AddSingleton<ITestService, TestService>(proxyGenerationOptions, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Singleton);
        }

        [Test]
        public void AddSingletonWithImplementationFactoryAndWithoutOptionsForClass()
        {
            var (services, ImplementationFactory, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Class);

            services.AddSingleton(ImplementationFactory, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Singleton);
        }

        [Test]
        public void AddSingletonWithImplementationFactoryAndOptionsForClass()
        {
            var (services, ImplementationFactory, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Class);

            services.AddSingleton(ImplementationFactory, proxyGenerationOptions, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Singleton);
        }

        [Test]
        public void AddSingletonWithImplementationFactoryAndWithoutOptionsForInterface()
        {
            var (services, ImplementationFactory, interceptors) = PrepareServiceCollectionWithoutOptions(ProxyTargetType.Interface);

            services.AddSingleton<ITestService, TestService>(ImplementationFactory, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Singleton);
        }

        [Test]
        public void AddSingletonWithImplementationFactoryAndOptionsForInterface()
        {
            var (services, ImplementationFactory, proxyGenerationOptions, interceptors) = PrepareServiceCollectionWithOptions(ProxyTargetType.Interface);

            services.AddSingleton<ITestService, TestService>(ImplementationFactory, proxyGenerationOptions, interceptors);

            AssertServicesCountAndLifetime(services, ServiceLifetime.Singleton);
        }

        #endregion

        #region Private helper methods

        #region Generator registration

        private static void RegisterProxyGeneratorWithOptionsForInterface(IServiceCollection services)
        {
            var generatorMock = new Mock<IInterceptronProxyGenerator>();
            generatorMock.Setup(g => g.CreateInterfaceProxy(
                It.IsAny<ITestService>(),
                It.IsAny<InterceptronProxyGenerationOptions>(),
                It.IsAny<IInterceptronInterceptor[]>())).Returns(
                (ITestService s, InterceptronProxyGenerationOptions o, IInterceptronInterceptor[] i) =>
                    new TestProxyService(s, i));
            services.AddSingleton(sp => generatorMock.Object);
        }

        private static void RegisterProxyGeneratorWithoutOptionsForInterface(IServiceCollection services)
        {
            var generatorMock = new Mock<IInterceptronProxyGenerator>();
            generatorMock.Setup(g => g.CreateInterfaceProxy(
                It.IsAny<ITestService>(),
                It.IsAny<IInterceptronInterceptor[]>())).Returns(
                (ITestService s, IInterceptronInterceptor[] i) =>
                    new TestProxyService(s, i));
            services.AddSingleton(sp => generatorMock.Object);
        }

        private static void RegisterProxyGeneratorWithOptionsForClass(IServiceCollection services)
        {
            var generatorMock = new Mock<IInterceptronProxyGenerator>();
            generatorMock.Setup(g => g.CreateClassProxy(
                It.IsAny<ITestService>(),
                It.IsAny<InterceptronProxyGenerationOptions>(),
                It.IsAny<IInterceptronInterceptor[]>())).Returns(
                (ITestService s, InterceptronProxyGenerationOptions o, IInterceptronInterceptor[] i) =>
                    new TestProxyService(s, i));
            services.AddSingleton(sp => generatorMock.Object);
        }

        private static void RegisterProxyGeneratorWithoutOptionsForClass(IServiceCollection services)
        {
            var generatorMock = new Mock<IInterceptronProxyGenerator>();
            generatorMock.Setup(g => g.CreateClassProxy(
                It.IsAny<ITestService>(),
                It.IsAny<IInterceptronInterceptor[]>())).Returns(
                (ITestService s, IInterceptronInterceptor[] i) =>
                new TestProxyService(s, i));
            services.AddSingleton(sp => generatorMock.Object);
        }

        #endregion

        #region Assertion

        private static void AssertTestProxyService(ITestService service, IInterceptronInterceptor[] interceptors)
        {
            var testProxyService = service as IProxy<ITestService>;
            Assert.IsNotNull(testProxyService);

            var actualInterceptors = testProxyService.GetInterceptors();
            Assert.AreEqual(actualInterceptors.Length, interceptors.Length);
            Assert.AreEqual(actualInterceptors[0], interceptors[0]);

            Assert.IsInstanceOf<TestService>(testProxyService.GetTarget());
        }

        private static void AssertServicesCountAndLifetime(IServiceCollection services, ServiceLifetime serviceLifetime)
        {
            Assert.AreEqual(2, services.Count);
            Assert.AreEqual(serviceLifetime, services[1].Lifetime);
        }

        #endregion

        #region Service collection preparation

        private (IServiceCollection services, Func<IServiceProvider, TestService> implementationFactory, InterceptronProxyGenerationOptions proxyGenerationOptions, IInterceptronInterceptor[] interceptors) PrepareServiceCollectionWithOptions(ProxyTargetType targetType)
        {
            var services = new ServiceCollection();
            TestService ImplementationFactory(IServiceProvider sp) => new TestService();
            var proxyGenerationOptions = new InterceptronProxyGenerationOptions();
            var interceptorMock = new Mock<IInterceptronInterceptor>();
            IInterceptronInterceptor[] interceptors = { interceptorMock.Object };
            switch (targetType)
            {
                case ProxyTargetType.Interface:
                    RegisterProxyGeneratorWithOptionsForInterface(services);
                    break;
                case ProxyTargetType.Class:
                    RegisterProxyGeneratorWithOptionsForClass(services);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetType), targetType, null);
            }

            return (services, ImplementationFactory, proxyGenerationOptions, interceptors);
        }

        private (IServiceCollection services, Func<IServiceProvider, TestService> implementationFactory, IInterceptronInterceptor[] interceptors) PrepareServiceCollectionWithoutOptions(ProxyTargetType targetType)
        {
            var services = new ServiceCollection();
            TestService ImplementationFactory(IServiceProvider sp) => new TestService();
            var interceptorMock = new Mock<IInterceptronInterceptor>();
            IInterceptronInterceptor[] interceptors = { interceptorMock.Object };
            switch (targetType)
            {
                case ProxyTargetType.Interface:
                    RegisterProxyGeneratorWithoutOptionsForInterface(services);
                    break;
                case ProxyTargetType.Class:
                    RegisterProxyGeneratorWithoutOptionsForClass(services);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetType), targetType, null);
            }

            return (services, ImplementationFactory, interceptors);
        }

        #endregion

        #endregion
    }
}
