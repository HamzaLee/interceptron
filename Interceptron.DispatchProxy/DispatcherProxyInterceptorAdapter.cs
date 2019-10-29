using Interceptron.Core;

namespace Interceptron.DispatchProxy
{
    public class DispatcherProxyInterceptorAdapter : DispatchProxyInterceptor
    {
        public IInterceptronInterceptor Interceptor { get; set; }

        public override object Intercept(DispatchProxyInvocation invocation)
        {
            if (this.Interceptor == null)
            {
                throw new InterceptorNullException("Interceptor is null, you should assign a value before calling Intercept.");
            }

            invocation.Target = this.Target;
            return this.Interceptor.Intercept(invocation);
        }
    }
}