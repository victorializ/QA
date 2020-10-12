using System;
using System.Collections.Generic;

namespace Lab2
{
    public class ExceptionManager : IExceptionManager
    {
        ILogger _logger;
        ICustomConfiguration _config;

        List<string> CriticalExceptions { get; set; }

        public int CriticalExceptionsCounter { get; set; }
        public int NonCriticalExceptionsCounter { get; set; }
        public int FailedRequestsCounter { get; set; }

        public ICustomConfiguration CustomConfiguration
        {
            get { return _config; }
            set { _config = value; }
        }

        public ExceptionManager()
        {
            CriticalExceptionsCounter = 0;
            NonCriticalExceptionsCounter = 0;

            _config = new CustomConfiguration();

            CriticalExceptions = new List<string>(_config.GetExceptionsFromConfig().Value.Split(','));
        }

        public ExceptionManager(ILogger logger) : this()
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

        public bool SendToLogger(Exception ex)
        {
            if (_logger.Log(ex))
            {
                return true;
            }
            else
            {
                FailedRequestsCounter++;
                return false;
            }
        }

    }
}
