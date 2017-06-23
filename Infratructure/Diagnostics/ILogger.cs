using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagnostics
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message);
        void LogError(Exception ex, string message);
    }
}
