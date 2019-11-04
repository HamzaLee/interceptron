using System;

namespace Interceptron.DispatchProxy
{
    public static class DispatchProxyUtils
    {
        public static object CreateDispatchProxy(Type serviceType, Type proxyType)
        {
            var methodInfo = typeof(System.Reflection.DispatchProxy).GetMethod("Create");
            var makeGenericMethod = methodInfo.MakeGenericMethod(serviceType, proxyType);
            var returnValue = makeGenericMethod.Invoke(null, null);
            return returnValue;
        }
    }
}
