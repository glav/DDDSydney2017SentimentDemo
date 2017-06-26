using EmailCollector.CommandProcessors;
using Jobs.Common.Diagnostics;
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
        private const string QueueName = "emailqueue";

        public static void ProcessQueueMessage([QueueTrigger(QueueName)]string message, TextWriter logger)
        {

            var appConfig = new Config();
            var jobLogger = new JobLogger(logger);

            jobLogger.WriteLine($"Message received in the [{QueueName}], Content: [{message}]");

            var collector = new MailCollectionProcessor(appConfig, null, jobLogger);
            collector.GetLatestMail();

        }
    }
}
