using Interceptron.Core;

namespace Interceptron.Tests
{
    public class TestProxyService : TestService, IProxy<ITestService>
    {
        private readonly ITestService innerService;
        private readonly IInterceptronInterceptor[] interceptors;

        public TestProxyService(ITestService service, IInterceptronInterceptor[] interceptors = null)
        {
            this.innerService = service;
            this.interceptors = interceptors;
        }

        public IInterceptronInterceptor[] GetInterceptors() => this.interceptors;

        public ITestService GetTarget() => this.innerService;
    }
}