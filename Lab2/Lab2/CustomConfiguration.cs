using Microsoft.Extensions.Configuration;

namespace Lab2
{
    public class CustomConfiguration: ICustomConfiguration
    {
        public IConfigurationSection GetExceptionsFromConfig()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            return builder.Build().GetSection("CriticalExceptions");
        }
    }
}
