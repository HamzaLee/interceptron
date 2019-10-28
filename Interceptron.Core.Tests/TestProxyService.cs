namespace Interceptron.Core.Tests
{
    public class TestProxyService : TestService, IProxy<ITestService>
    {
        private readonly ITestService innerService;
        private readonly IInterceptor[] interceptors;

        public TestProxyService(ITestService service, IInterceptor[] interceptors = null)
        {
            this.innerService = service;
            this.interceptors = interceptors;
        }

        public IInterceptor[] GetInterceptors() => this.interceptors;

        public ITestService GetTarget() => this.innerService;
    }
}