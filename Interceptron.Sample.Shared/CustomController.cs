namespace Interceptron.Sample.Shared
{
    public interface ICustomController
    {
        string GetValue();
    }

    public class CustomController : ICustomController
    {
        private readonly ICustomService service;

        public CustomController(ICustomService service)
        {
            this.service = service;
        }

        public virtual string GetValue() => $"{nameof(CustomController)}({service.GetValue()})";
    }
}