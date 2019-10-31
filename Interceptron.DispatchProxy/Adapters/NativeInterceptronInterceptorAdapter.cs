using System;
using Interceptron.Core;

namespace Interceptron.DispatchProxy.Adapters
{
    public class NativeInterceptronInterceptorAdapter : IInterceptronInterceptor
    {
        public NativeInterceptronInterceptorAdapter(System.Reflection.DispatchProxy interceptor, Func<System.Reflection.DispatchProxy, Action<object>> targetSetter)
        {
            this.Interceptor = interceptor;
            this.TargetSetter = targetSetter;
        }

        public System.Reflection.DispatchProxy Interceptor { get; }

        public Func<System.Reflection.DispatchProxy, Action<object>> TargetSetter { get; }

        public object Intercept(IInterceptronInvocation invocation)
        {
            throw new InterceptronInvocationException("This method should not be called, the class should only carry Interceptor and TargetSetter properties.");
        }
    }

}
