using System;
using Interceptron.Core;
using Moq;
using NUnit.Framework;

namespace Interceptron.DynamicProxy.Tests
{
    public class DynamicProxyInterceptorAdapterTests
    {
        [Test]
        public void Ctor_WhenInterceptorIsNull_ThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new DynamicProxyInterceptorAdapter(null));
        }

        [Test]
        public void Ctor_WhenInterceptorIsNotNull_ThenReturnDynamicProxyInterceptorAdapter()
        {
            var interceptorMock = new Mock<IInterceptronInterceptor>();

            var dynamicProxyInterceptorAdapter = new DynamicProxyInterceptorAdapter(interceptorMock.Object);

            Assert.NotNull(dynamicProxyInterceptorAdapter);
        }

        [Test]
        public void Intercept()
        {
            var interceptorMock = new Mock<IInterceptronInterceptor>();
            var invocationMock = new Mock<Castle.DynamicProxy.IInvocation>();

            var dynamicProxyInterceptorAdapter = new DynamicProxyInterceptorAdapter(interceptorMock.Object);

            dynamicProxyInterceptorAdapter.Intercept(invocationMock.Object);
            interceptorMock.Verify(i => i.Intercept(invocationMock.Object), Times.Once);
        }
    }
}
