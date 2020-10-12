using System;

namespace Lab2
{
    interface IExceptionManager
    {
        bool SendToLogger(Exception ex);
        bool IsExceptionCritical(Exception exception);
        int FailedRequestsCounter { get; set; }
    }
}
