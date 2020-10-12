using System;
using System.IO;

namespace Lab2
{
    public class Logger : ILogger
    {
        public static int FailedRequestsCounter { get; set; }

        public Logger()
        {
            FailedRequestsCounter = 0;
        }

        public bool SendToLogger(Exception ex)
        {
            try
            {
                Log($"Exception: {ex.Message}; Time: {DateTime.UtcNow}");
                return true;
            }
            catch
            {
                FailedRequestsCounter++;
                return false;
            }
        }

        public void Log(string exceptionDetails)
        {

            using (StreamWriter writetext = new StreamWriter("log.txt"))
            {
                writetext.WriteLine(exceptionDetails + Environment.NewLine + exceptionDetails.Length + Environment.NewLine);
            }

        }
    }
}
