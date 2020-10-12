using System;

namespace Lab2
{
    interface IExceptionManager
    {
        
        bool IsExceptionCritical(Exception exception);
    }
}
