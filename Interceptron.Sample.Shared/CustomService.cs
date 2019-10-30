namespace Interceptron.Sample.Shared
{
    public interface ICustomService
    {
        string GetValue();
    }

    public class CustomService : ICustomService
    {
        private readonly ICustomRepository repository;

        public CustomService(ICustomRepository repository)
        {
            this.repository = repository;
        }

        public virtual string GetValue() => nameof(CustomService) + "(" + repository.GetValue() + ")";
    }
}