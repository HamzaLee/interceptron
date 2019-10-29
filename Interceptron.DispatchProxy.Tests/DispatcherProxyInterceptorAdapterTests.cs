using Interceptron.Core;
using Moq;
using NUnit.Framework;

namespace Interceptron.DispatchProxy.Tests
{
    public class DispatcherProxyInterceptorAdapterTests
    {
        [Test]
        public void Intercept_WhenInterceptorIsNull_ThenThrowInterceptorNullException()
        {
            var dispatcherProxyInterceptorAdapter = new DispatcherProxyInterceptorAdapter();
            var invocation = new DispatchProxyInvocation(null, null, null);
            Assert.Throws<InterceptorNullException>(() => dispatcherProxyInterceptorAdapter.Intercept(invocation));
        }

        [Test]
        public void Intercept()
        {
            var expectedReturnedValue = "Returned value";
            var dispatcherProxyInterceptorAdapter = new DispatcherProxyInterceptorAdapter();
            var interceptorMock = new Mock<IInterceptronInterceptor>();
            interceptorMock.Setup(i => i.Intercept(It.IsAny<object>())).Returns(expectedReturnedValue);
            dispatcherProxyInterceptorAdapter.Interceptor = interceptorMock.Object;
            var invocation = new DispatchProxyInvocation(null, null, null);

            var actualReturnedValue = dispatcherProxyInterceptorAdapter.Intercept(invocation);

            Assert.AreEqual(expectedReturnedValue, actualReturnedValue);
            interceptorMock.Verify(i => i.Intercept(invocation), Times.Once);
        }
    }
}
