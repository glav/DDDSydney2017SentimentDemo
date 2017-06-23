using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagnostics
{
    public class TraceLogger : ILogger
    {
        public void LogError(string message)
        {
            System.Diagnostics.Trace.TraceError(message);
        }

        public void LogError(Exception ex, string message)
        {
            var baseEx = ex.GetBaseException();
            var msg = string.Format("Error: [{0}] {1}:{2} ==> {3}", message, baseEx.ToString(), baseEx.Message, baseEx.StackTrace);
            System.Diagnostics.Trace.TraceError(msg);
        }

        public void LogInfo(string message)
        {
            System.Diagnostics.Trace.TraceInformation(message);
        }
    }
}
