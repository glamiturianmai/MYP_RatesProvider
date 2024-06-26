using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYP_RatesProvider.Core
{
    public class MyService
    {
        private static readonly string logFilePath = @"C:\Users\dns\Desktop\MiraculousBackend\MYP_RatesProvider:logs.txt";

        public void WriteLog(string message)
        {
            try
            {
                using (FileStream fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine($"{DateTime.Now} - {message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    
}   }
