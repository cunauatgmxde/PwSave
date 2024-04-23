using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gnCommon;
using LogLevel = gnCommon.LogLevel;

namespace gnLogger
{
    public class Logger : ILogger
    {
        public void Dispose()
        {
            //
        }

        public void Log(string message, LogLevel logLevel = LogLevel.DEFAULT)
        {
            // Meldung wegschreiben ...
        }

        public void Log(Exception ex, LogLevel logLevel = LogLevel.ERROR)
        {
            Log(ex.Message, logLevel);
        }
    }
}
