namespace Interceptron.Sample.Shared
{
    public interface ICustomRepository
    {
        string GetValue();
    }

    public class CustomRepository : ICustomRepository
    {
        private readonly ICustomContext context;
        private readonly ICustomConfiguration configuration;

        public CustomRepository(ICustomContext context, ICustomConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public virtual string GetValue() => $"{nameof(CustomRepository)}({context.GetValue()} + {configuration.GetValue()})";
    }
}