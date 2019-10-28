namespace Interceptron.Core
{
    public interface IInterceptor
    {
        object Intercept(object invocation);
    }
}
