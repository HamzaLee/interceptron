using System.Reflection;

namespace Interceptron.DispatchProxy
{
    public class DispatchProxyInvocation
    {
        public DispatchProxyInvocation(MethodInfo targetMethod, object[] args, object target)
        {
            MethodInfo = targetMethod;
            Arguments = args;
            Target = target;
        }

        public object Target { get; set; }

        public MethodInfo MethodInfo { get; set; }

        public object[] Arguments { get; set; }

        public object Invoke()
        {
            return MethodInfo.Invoke(Target, Arguments);
        }
    }
}
