using Interceptron.Core;

namespace Interceptron.DispatchProxy.Adapters
{
    public class DispatcherProxyInterceptorAdapter : DispatchProxyInterceptor
    {
        public IInterceptronInterceptor Interceptor { get; set; }

        public override object Intercept(DispatchProxyInvocation invocation)
        {
            if (this.Interceptor == null)
            {
                throw new InterceptronInterceptorNullException("Interceptor is null, you should assign a value before calling Intercept.");
            }

            invocation.Target = this.Target;
            return this.Interceptor.Intercept(new InterceptronInvocationAdapter(invocation));
        }
    }
}