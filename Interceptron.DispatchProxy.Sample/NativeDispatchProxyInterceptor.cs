using System;
using System.Reflection;

namespace Interceptron.DispatchProxy.Sample
{
    public class NativeDispatchProxyInterceptor : System.Reflection.DispatchProxy
    {
        private object target;

        public void SetTarget(object target)
        {
            this.target = target;
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine($"*** Intercept from NativeDispatchProxy {target.GetType().Name}");
            return targetMethod.Invoke(this.target, args);
        }
    }
}