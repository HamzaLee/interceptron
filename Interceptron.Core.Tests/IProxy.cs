namespace Interceptron.Core.Tests
{
    public interface IProxy<out TTarget>
    {
        TTarget GetTarget();

        IInterceptor[] GetInterceptors();
    }
}