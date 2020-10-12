using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    public class LoggerFactory : ILoggerFactory
    {
        ILogger _logger;

        public ILogger CreateLogger()
        {
            if (_logger != null)
            {
                return _logger;
            }
            return new Logger();
        }

        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }
    }
}
