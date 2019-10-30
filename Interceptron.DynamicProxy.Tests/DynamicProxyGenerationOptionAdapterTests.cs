using System;
using System.Diagnostics.CodeAnalysis;
using Interceptron.Core;
using Interceptron.DynamicProxy.Adapters;
using NUnit.Framework;

namespace Interceptron.DynamicProxy.Tests
{
    [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "No need to a variable, Ctor will throw an exception")]
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
