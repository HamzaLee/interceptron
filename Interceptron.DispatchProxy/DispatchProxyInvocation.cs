using System.Reflection;

namespace Interceptron.DispatchProxy
{
    public class DispatchProxyInvocation
    {
        public DispatchProxyInvocation(MethodInfo targetMethod, object[] args, object target)
        {
            MethodInfo = targetMethod;
            Args = args;
            Target = target;
        }

        public object Target { get; set; }

        public MethodInfo MethodInfo { get; set; }

        public object[] Args { get; set; }

        public object Invoke()
        {
            return MethodInfo.Invoke(Target, Args);
        }
    }
}
