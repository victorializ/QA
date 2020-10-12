using System;
using System.IO;

namespace Lab2
{
    public class Logger : ILogger
    {

        public bool Log(Exception ex)
        {
            try
            {
                using (StreamWriter writetext = new StreamWriter("log.txt"))
                {
                    writetext.WriteLine($"Exception: {ex.Message}; Time: {DateTime.UtcNow}");
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
