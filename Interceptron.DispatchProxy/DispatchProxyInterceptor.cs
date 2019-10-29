
using System.Reflection;

namespace Interceptron.DispatchProxy
{
    public abstract class DispatchProxyInterceptor : System.Reflection.DispatchProxy
    {
        public object Target { get; set; }

        public abstract object Intercept(DispatchProxyInvocation dispatchProxyInvocation);

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            var dispatchProxyInvocation = new DispatchProxyInvocation(targetMethod, args, null);
            return this.Intercept(dispatchProxyInvocation);
        }
    }
}