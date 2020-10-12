using Microsoft.Extensions.Configuration;

namespace Lab2
{
    public interface ICustomConfiguration
    {
        IConfigurationSection GetExceptionsFromConfig();
    }
}
