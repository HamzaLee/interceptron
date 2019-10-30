using System;
using Castle.DynamicProxy;
using Interceptron.Core;
using Interceptron.DynamicProxy.Adapters;
using Interceptron.DynamicProxy.Helpers;
using Moq;
using NUnit.Framework;

namespace Interceptron.DynamicProxy.Tests
{
    public class DynamicProxyGeneratorHelperTests
    {
        [Test]
        public void ToDynamicProxyInterceptors_WhenInterceptorsIsNull_ThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DynamicProxyGeneratorHelper.ToDynamicProxyInterceptors(null));
        }

        [Test]
        public void ToDynamicProxyInterceptors_WhenInterceptorsIsNotNull_ThenThrowArgumentNullException()
        {
            var interceptorMock = new Mock<IInterceptronInterceptor>();
            var interceptors = new[] { interceptorMock.Object };
            var transformedInterceptors = DynamicProxyGeneratorHelper.ToDynamicProxyInterceptors(interceptors);

            Assert.NotNull(transformedInterceptors);
            Assert.IsInstanceOf<IInterceptor[]>(transformedInterceptors);

            var transformedInterceptor = transformedInterceptors[0] as DynamicProxyInterceptorAdapter;
            Assert.IsNotNull(transformedInterceptor);

            Assert.AreEqual(interceptorMock.Object, transformedInterceptor.Interceptor);
        }
    }
}
