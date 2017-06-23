using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailCollector
{
    public class QueueTriggerCollection
    {
        public static void ProcessQueueMessage([QueueTrigger("mailqueue")]string logMessage, TextWriter logger)
        {

            var appConfig = new Config();
            var collector = new MailCollector(appConfig);
            collector.GetLatestMail();

        }
    }
}
