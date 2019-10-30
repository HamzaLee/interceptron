using System;
using System.Diagnostics.CodeAnalysis;
using Interceptron.Core;
using Interceptron.DynamicProxy.Adapters;
using Moq;
using NUnit.Framework;

namespace Interceptron.DynamicProxy.Tests
{
    [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "No need to a variable, Ctor will throw an exception")]
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
            interceptorMock.Verify(i => i.Intercept(It.IsAny<IInterceptronInvocation>()), Times.Once);
        }
    }
}
