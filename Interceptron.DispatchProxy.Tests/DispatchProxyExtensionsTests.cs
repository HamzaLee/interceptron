using System;
using Interceptron.Core;
using Interceptron.DispatchProxy.Adapters;
using Moq;
using NUnit.Framework;

namespace Interceptron.DispatchProxy.Tests
{
    public class DispatchProxyExtensionsTests
    {
        [Test]
        public void ToInterceptronInterceptor_WhenInterceptorIsNull_ThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DispatchProxyExtensions.ToInterceptronInterceptor(null));
        }

        [Test]
        public void ToInterceptronInterceptor_WhenInterceptorIsNotNull_ThenReturnNewInterceptronInterceptorAdapter()
        {
            var interceptorMock = new Mock<DispatcherProxyInterceptorAdapter>();
         
            var interceptronInterceptor = interceptorMock.Object.ToInterceptronInterceptor();

            Assert.IsInstanceOf<IInterceptronInterceptor>(interceptronInterceptor);
        }
    }
}
