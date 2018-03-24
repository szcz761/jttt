using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JTTT
{
    class Log
    {
        public static void WriteToLog(string message)
        {
            StreamWriter log = File.AppendText("JTTT.log");
            log.WriteLine(DateTime.Now.ToString() + " " + message);
            log.Close();
        }
    }
}
