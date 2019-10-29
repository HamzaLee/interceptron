using System;
using Castle.DynamicProxy;
using Interceptron.Core;

namespace Interceptron.DynamicProxy
{
    public class DynamicProxyGenerationOptionAdapter : ProxyGenerationOptions
    {
        private InterceptronProxyGenerationOptions proxyGenerationOptions;

        public DynamicProxyGenerationOptionAdapter(InterceptronProxyGenerationOptions proxyGenerationOptions)
        {
            this.proxyGenerationOptions = proxyGenerationOptions ?? throw new ArgumentNullException(nameof(proxyGenerationOptions));
        }
    }
}