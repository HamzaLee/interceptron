namespace Interceptron.Sample.Shared
{
    public interface ICustomContext
    {
        string GetValue();
    }

    public class CustomContext : ICustomContext
    {
        public virtual string GetValue() => nameof(CustomContext);
    }
}