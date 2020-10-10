using System;
using System.IO;

namespace Lab2
{
    public class Logger: ILogger
    {
        public bool Log(string exceptionDetails)
        {
            try
            {
                using (StreamWriter writetext = new StreamWriter("log.txt"))
                {
                    writetext.WriteLine(exceptionDetails + Environment.NewLine);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
