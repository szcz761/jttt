using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace JTTT
{
    class Log
    {
        static List<string> logList;
        static Log()
        {
            logList = new List<string>();
        }
        public static void WriteToLog(string message)
        {
            logList.Add(DateTime.Now.ToString() + " " + message);   
        }

        public static void throwToLog()
        {
            while (true)
            {
                StreamWriter log = File.AppendText("JTTT.log");
                if (logList.Count > 0)
                {
                    foreach (string s in logList)
                        log.WriteLine(s);
                }
                log.Close();
                logList.Clear();
                Thread.Sleep(1000);
            }
        }
    }
}
