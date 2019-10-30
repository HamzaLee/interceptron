namespace Interceptron.Sample.Shared
{
    public interface ICustomConfiguration
    {
        string GetValue();
    }

    public class CustomConfiguration : ICustomConfiguration
    {
        private int counter;

        public virtual string GetValue() => $"{nameof(CustomConfiguration)}[{counter++}]";
    }
}