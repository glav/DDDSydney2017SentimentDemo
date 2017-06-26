using Jobs.Common;
using Jobs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailCollector
{
    public class QueueCommandHandler
    {
        public QueueCommandHandler(JobCommand jobCommand)
        {
            Command = jobCommand;
        }

        public JobCommand Command { get; private set; }
    }
}
