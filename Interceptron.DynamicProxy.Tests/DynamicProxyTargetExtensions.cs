using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace Interceptron.DynamicProxy.Tests
{
    public static class DynamicProxyTargetExtensions
    {
        public static ProxyGenerationOptions GetProxyGenerationOptions(this object target)
        {
            var type = target.GetType() as TypeInfo;
            var typeDeclaredFields = type?.DeclaredFields;
            var fieldInfo = typeDeclaredFields?.First(ft => ft.FieldType == typeof(ProxyGenerationOptions));
            return fieldInfo?.GetValue(target) as ProxyGenerationOptions;
        }
    }
}
