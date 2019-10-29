using System;
using Interceptron.Core;
using NUnit.Framework;

namespace Interceptron.DynamicProxy.Tests
{
    public class DynamicProxyGenerationOptionAdapterTests
    {
        [Test]
        public void Ctor_whenProxyGenerationOptionsIsNull_ThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new DynamicProxyGenerationOptionAdapter(null));
        }

        [Test]
        public void Ctor_whenProxyGenerationOptionsIsNotNull_ThenReturnNewDynamicProxyGenerationOptionAdapter()
        {
            var dynamicProxyGenerationOptionAdapter = new DynamicProxyGenerationOptionAdapter(new InterceptronProxyGenerationOptions());
            Assert.NotNull(dynamicProxyGenerationOptionAdapter);
        }
    }
}
