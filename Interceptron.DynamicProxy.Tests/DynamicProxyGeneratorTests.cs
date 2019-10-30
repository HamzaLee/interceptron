using Castle.DynamicProxy;
using Interceptron.Core;
using Interceptron.DynamicProxy.Adapters;
using Interceptron.Tests;
using Moq;
using NUnit.Framework;

namespace Interceptron.DynamicProxy.Tests
{
    public class DynamicProxyGeneratorTests
    {
        [Test]
        public void CreateClassProxyWithoutOptions()
        {
            var dynamicProxyGenerator = new DynamicProxyGenerator();
            var implementationInstance = new TestService();
            var interceptorMock = new Mock<IInterceptronInterceptor>();
            var interceptors = new[] { interceptorMock.Object };

            var testService = dynamicProxyGenerator.CreateClassProxy(implementationInstance, interceptors);

            Assert.IsInstanceOf<TestService>(testService);

            AssertInterceptors(testService, interceptors);
        }

        [Test]
        public void CreateClassProxyWithOptions()
        {
            var dynamicProxyGenerator = new DynamicProxyGenerator();
            var implementationInstance = new TestService();
            var interceptorMock = new Mock<IInterceptronInterceptor>();
            var interceptors = new[] { interceptorMock.Object };
            var proxyGenerationOptions = new InterceptronProxyGenerationOptions();

            var testService = dynamicProxyGenerator.CreateClassProxy(implementationInstance, proxyGenerationOptions, interceptors);

            Assert.IsInstanceOf<TestService>(testService);

            var generationOptions = testService.GetProxyGenerationOptions();
            Assert.IsInstanceOf<DynamicProxyGenerationOptionAdapter>(generationOptions);

            AssertInterceptors(testService, interceptors);
        }

        [Test]
        public void CreateInterfaceProxyWithoutOptions()
        {
            var dynamicProxyGenerator = new DynamicProxyGenerator();
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
            var dynamicProxyGenerator = new DynamicProxyGenerator();
            ITestService implementationInstance = new TestService();
            var interceptorMock = new Mock<IInterceptronInterceptor>();
            var interceptors = new[] { interceptorMock.Object };
            var proxyGenerationOptions = new InterceptronProxyGenerationOptions();

            var testService = dynamicProxyGenerator.CreateInterfaceProxy(implementationInstance, proxyGenerationOptions, interceptors);

            Assert.IsInstanceOf<ITestService>(testService);

            var generationOptions = testService.GetProxyGenerationOptions();
            Assert.IsInstanceOf<DynamicProxyGenerationOptionAdapter>(generationOptions);

            AssertInterceptors(testService, interceptors);
        }

        private static void AssertInterceptors<TTarget>(TTarget testService, IInterceptronInterceptor[] interceptors)
        {
            var proxyTargetAccessor = testService as IProxyTargetAccessor;
            Assert.IsNotNull(proxyTargetAccessor);

            var actualInterceptors = proxyTargetAccessor.GetInterceptors();
            Assert.AreEqual(actualInterceptors.Length, interceptors.Length);

            var actualInterceptor = actualInterceptors[0] as DynamicProxyInterceptorAdapter;
            Assert.IsNotNull(actualInterceptor);
            Assert.AreEqual(actualInterceptor.Interceptor, interceptors[0]);

            var proxyTarget = proxyTargetAccessor.DynProxyGetTarget();
            Assert.IsInstanceOf<TTarget>(proxyTarget);
        }
    }
}
