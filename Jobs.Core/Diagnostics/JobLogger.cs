using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobs.Core.Diagnostics
{
    public class JobLogger
    {
        private readonly TextWriter _logger;

        public JobLogger(TextWriter logger)
        {
            _logger = logger;
        }

        public void WriteLine(string message)
        {
            if (_logger != null)
            {
                _logger.WriteLine(message);
            }
        }
    }
}
