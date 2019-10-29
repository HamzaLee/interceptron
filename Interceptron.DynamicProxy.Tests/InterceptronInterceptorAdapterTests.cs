using System;
using Castle.DynamicProxy;
using Moq;
using NUnit.Framework;

namespace Interceptron.DynamicProxy.Tests
{
    public class InterceptronInterceptorAdapterTests
    {
        [Test]
        public void Ctor_WhenInterceptorIsNull_ThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new InterceptronInterceptorAdapter(null));
        }

        [Test]
        public void Ctor_WhenInterceptorIsNotNull_ThenReturnInterceptronInterceptorAdapter()
        {
            var interceptorMock = new Mock<IInterceptor>();

            var interceptronInterceptorAdapter = new InterceptronInterceptorAdapter(interceptorMock.Object);

            Assert.NotNull(interceptronInterceptorAdapter);
        }

        [Test]
        public void Intercept_WhenInvocationIsNull_ThenThrowArgumentNullException()
        {
            var interceptorMock = new Mock<IInterceptor>();

            var interceptronInterceptorAdapter = new InterceptronInterceptorAdapter(interceptorMock.Object);

            Assert.Throws<ArgumentNullException>(() => interceptronInterceptorAdapter.Intercept(null));
        }

        [Test]
        public void Intercept_WhenInvocationIsNotIInvocation_ThenThrowArgumentException()
        {
            var interceptorMock = new Mock<IInterceptor>();

            var interceptronInterceptorAdapter = new InterceptronInterceptorAdapter(interceptorMock.Object);

            Assert.Throws<InvalidCastException>(() => interceptronInterceptorAdapter.Intercept("Not an invocation"));
        }

        [Test]
        public void Intercept()
        {
            var expectedReturnedValue = "Returned value";
            var interceptorMock = new Mock<IInterceptor>();
            var invocationMock = new Mock<Castle.DynamicProxy.IInvocation>();
            invocationMock.Setup(i => i.ReturnValue).Returns(expectedReturnedValue);
            var interceptronInterceptorAdapter = new InterceptronInterceptorAdapter(interceptorMock.Object);

            var actualReturnedValue = interceptronInterceptorAdapter.Intercept(invocationMock.Object);

            Assert.AreEqual(expectedReturnedValue, actualReturnedValue);
            interceptorMock.Verify(i => i.Intercept(invocationMock.Object), Times.Once);
        }
    }
}
