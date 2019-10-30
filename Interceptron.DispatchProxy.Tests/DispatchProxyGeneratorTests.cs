using System;
using Interceptron.Core;
using Interceptron.DispatchProxy.Adapters;
using Interceptron.Tests;
using Moq;
using NUnit.Framework;

namespace Interceptron.DispatchProxy.Tests
{
    public class DispatchProxyGeneratorTests
    {
        [Test]
        public void CreateClassProxyWithoutOptions_ThenThrowNotSupportedException()
        {
            var dynamicProxyGenerator = new DispatchProxyGenerator();

            Assert.Throws<NotSupportedException>(() => dynamicProxyGenerator.CreateClassProxy<TestService>(null, null));
        }

        [Test]
        public void CreateClassProxyWithOptions_ThenThrowNotSupportedException()
        {
            var dynamicProxyGenerator = new DispatchProxyGenerator();

            Assert.Throws<NotSupportedException>(() => dynamicProxyGenerator.CreateClassProxy<TestService>(null, null));
        }

        [Test]
        public void CreateInterfaceProxyWithoutOptions()
        {
            var dynamicProxyGenerator = new DispatchProxyGenerator();
            ITestService implementationInstance = new TestService();
            var interceptorMock = new Mock<IInterceptronInterceptor>();
            var interceptors = new[] { interceptorMock.Object };

            var testService = dynamicProxyGenerator.CreateInterfaceProxy(implementationInstance, interceptors);

            Assert.IsInstanceOf<ITestService>(testService);
            AssertInterceptors(testService, interceptors);
        }

        [Test]
        public void CreateInterfaceProxyWithOptions()
        {
            var dynamicProxyGenerator = new DispatchProxyGenerator();
            ITestService implementationInstance = new TestService();
            var interceptorMock = new Mock<IInterceptronInterceptor>();
            var interceptors = new[] { interceptorMock.Object };
            var proxyGenerationOptions = new InterceptronProxyGenerationOptions();

            var testService = dynamicProxyGenerator.CreateInterfaceProxy(implementationInstance, proxyGenerationOptions, interceptors);

            Assert.IsInstanceOf<ITestService>(testService);

            AssertInterceptors(testService, interceptors);
        }

        private static void AssertInterceptors<TTarget>(TTarget testService, IInterceptronInterceptor[] interceptors)
        {
            var proxyTargetAccessor = testService as DispatcherProxyInterceptorAdapter;
            Assert.IsNotNull(proxyTargetAccessor);

            var actualInterceptor = proxyTargetAccessor.Interceptor;
            Assert.IsNotNull(actualInterceptor);
            Assert.AreEqual(actualInterceptor, interceptors[0]);

            var proxyTarget = proxyTargetAccessor.Target;
            Assert.IsInstanceOf<TTarget>(proxyTarget);
        }
    }
}
