using System;
using System.Collections.Generic;
using System.Text;

namespace Lab01
{
    class ExceptionManager
    {
        public ExceptionManager()
        {
            CriticalExceptionsCounter = 0;
            NonCriticalExceptionsCounter = 0;
        }

        public bool IsExceptionCritical(Exception exception)
        {
            if (!(exception is null) && CriticalExceptionsList.Contains(exception.GetType()))
            {
                return true;
            }
            return false;
        }

        public void HandleException(Exception exception)
        {
            if(exception is null)
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
        
        public uint CriticalExceptionsCounter { get; set; }
        public uint NonCriticalExceptionsCounter { get; set; }

        private static readonly List<Type> CriticalExceptionsList = new List<Type>()
        {
            typeof(IndexOutOfRangeException),
            typeof(NullReferenceException),
            typeof(ArgumentException),
            typeof(ArgumentNullException),
            typeof(ArgumentOutOfRangeException)
        };
    }
}
