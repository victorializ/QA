
namespace Lab2
{
    public interface ILoggerFactory
    {
        ILogger CreateLogger();
        void SetLogger(ILogger logger);
    }
}
