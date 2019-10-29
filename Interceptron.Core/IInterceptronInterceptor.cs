namespace Interceptron.Core
{
    public interface IInterceptronInterceptor
    {
        object Intercept(object invocation);
    }
}
