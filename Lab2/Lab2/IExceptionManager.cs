using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    interface IExceptionManager
    {
        static int FailedRequestsConter { get; set; } = 0;
        bool IsExceptionCritical(Exception exception);
        void SendToLogger(Exception ex);
    }
}
