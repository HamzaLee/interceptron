using System;
using Castle.DynamicProxy;
using Interceptron.Core;
using Moq;
using NUnit.Framework;

namespace Interceptron.DynamicProxy.Tests
{
    public class DynamicProxyExtensionsTests
    {

        [Test]
        public void ToInterceptronInterceptor_WhenInterceptorIsNull_ThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DynamicProxyExtensions.ToInterceptronInterceptor(null));
        }

        [Test]
        public void ToInterceptronInterceptor_WhenInterceptorIsNotNull_ThenReturnIInterceptronInterceptor()
        {
            var interceptorMock = new Mock<IInterceptor>();
            var interceptronInterceptor = interceptorMock.Object.ToInterceptronInterceptor();
           
            Assert.IsInstanceOf<IInterceptronInterceptor>(interceptronInterceptor);
        }
    }
}