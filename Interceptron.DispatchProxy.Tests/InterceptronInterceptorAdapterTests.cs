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
        public void Intercept_WhenInvocationIsNotDispatchProxyInvocation_ThenThrowInvalidCastException()
        {
            var interceptorMock = new Mock<DispatchProxyInterceptor>();

            var interceptronInterceptorAdapter = new InterceptronInterceptorAdapter(interceptorMock.Object);

            Assert.Throws<InvalidCastException>(() => interceptronInterceptorAdapter.Intercept("Not an invocation"));
        }

        [Test]
        public void Intercept_WhenInvocationIsNotDispatchProxyInvocation_ThenThrowInvalidCastExceptiona()
        {
            var expectedReturnedValue = "Returned value";
            var interceptorMock = new Mock<DispatchProxyInterceptor>();
            interceptorMock.Setup(i => i.Intercept(It.IsAny<DispatchProxyInvocation>()))
                .Returns(expectedReturnedValue);
            var invocation = new DispatchProxyInvocation(null, null, null);
            var interceptronInterceptorAdapter = new InterceptronInterceptorAdapter(interceptorMock.Object);

            var actualReturnedValue = interceptronInterceptorAdapter.Intercept(invocation);

            Assert.AreEqual(expectedReturnedValue, actualReturnedValue);
            Assert.AreEqual(interceptorMock.Object, interceptronInterceptorAdapter.Interceptor);
            interceptorMock.Verify(i => i.Intercept(invocation), Times.Once);
        }
    }
}
