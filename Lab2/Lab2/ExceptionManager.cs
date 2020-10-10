using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Lab2
{
    public class ExceptionManager: IExceptionManager
    {
        ILogger _logger;
        List<string> CriticalExceptions { get; set; }

        public int CriticalExceptionsCounter { get; set; }
        public int NonCriticalExceptionsCounter { get; set; }
        public int FailedRequestsCounter { get; set; }

        public ExceptionManager()
        {
            CriticalExceptionsCounter = 0;
            NonCriticalExceptionsCounter = 0;
            FailedRequestsCounter = 0;

            GetExceptionsFromConfig();

            CriticalExceptions = new List<string>(GetExceptionsFromConfig().Value.Split(','));
        }

        public ExceptionManager(ILogger logger): this()
        {
            _logger = logger;
        }

        public bool IsExceptionCritical(Exception exception)
        {
            if (!(exception is null) && CriticalExceptions.Contains(exception.GetType().Name))
            {
                SendToLogger(exception);
                return true;
            }
            return false;
        }

        public void SendToLogger(Exception ex)
        {
            try
            {
                bool res = _logger.Log($"Exception: {ex}; Time: {DateTime.UtcNow}");
            }
            catch
            {
                FailedRequestsCounter++;
            }
        }

        public void HandleException(Exception exception)
        {
            if (exception is null)
            {
                return;
            }

            if (IsExceptionCritical(exception))
            {
                CriticalExceptionsCounter++;
            }
            else
            {
                NonCriticalExceptionsCounter++;
            }
        }

        private static IConfigurationSection GetExceptionsFromConfig()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            return builder.Build().GetSection("CriticalExceptions");
        }
    }
}
