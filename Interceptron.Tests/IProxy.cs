using Interceptron.Core;

namespace Interceptron.Tests
{
    public interface IProxy<out TTarget>
    {
        TTarget GetTarget();

        IInterceptronInterceptor[] GetInterceptors();
    }
}