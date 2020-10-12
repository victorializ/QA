using System;

namespace Lab2
{
    public interface ILogger
    {
        void Log(string exceptionDetails);
        bool SendToLogger(Exception ex);
        static int FailedRequestsConter { get; set; } 
    }
}
