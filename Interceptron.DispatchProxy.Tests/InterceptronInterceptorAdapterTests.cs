using System;
using Moq;
using NUnit.Framework;

namespace Interceptron.DispatchProxy.Tests
{
    public class InterceptronInterceptorAdapterTests
    {
        [Test]
        public void Ctor_WhenInterceptorIsNull_ThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new InterceptronInterceptorAdapter(null));
        }

        [Test]
        public void Ctor_WhenInterceptorIsNotNull_ThenReturnNewInterceptronInterceptorAdapter()
        {
            var interceptorMock = new Mock<DispatchProxyInterceptor>();

            var interceptronInterceptorAdapter = new InterceptronInterceptorAdapter(interceptorMock.Object);

            Assert.IsNotNull(interceptronInterceptorAdapter);
        }

        [Test]
        public void Intercept_WhenInvocationIsNull_ThenThrowArgumentNullException()
        {
            var interceptorMock = new Mock<DispatchProxyInterceptor>();

            var interceptronInterceptorAdapter = new InterceptronInterceptorAdapter(interceptorMock.Object);

            Assert.Throws<ArgumentNullException>(() => interceptronInterceptorAdapter.Intercept(null));
        }

        [Test]
        public void Intercept_WhenInvocationIsNotDispatchProxyInvocation_ThenThrowInvalidCastExceptiona()
        {
            var expectedReturnedValue = "Returned value";
            var interceptorMock = new Mock<DispatchProxyInterceptor>();
            interceptorMock.Setup(i => i.Intercept(It.IsAny<DispatchProxyInvocation>()))
                .Returns(expectedReturnedValue);
            var dispatchProxyInvocation = new DispatchProxyInvocation(null, null, null);
            var invocation = new InterceptronInvocationAdapter(dispatchProxyInvocation);

            var interceptronInterceptorAdapter = new InterceptronInterceptorAdapter(interceptorMock.Object);

            var actualReturnedValue = interceptronInterceptorAdapter.Intercept(invocation);

            Assert.AreEqual(expectedReturnedValue, actualReturnedValue);
            Assert.AreEqual(interceptorMock.Object, interceptronInterceptorAdapter.Interceptor);
            interceptorMock.Verify(i => i.Intercept(dispatchProxyInvocation), Times.Once);
        }
    }
}
