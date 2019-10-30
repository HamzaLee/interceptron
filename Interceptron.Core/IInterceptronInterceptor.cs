namespace Interceptron.Core
{
    public interface IInterceptronInterceptor
    {
        object Intercept(IInterceptronInvocation invocation);
    }
}
