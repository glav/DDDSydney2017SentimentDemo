﻿using EmailCollector.CommandProcessors;
using Jobs.Core.Diagnostics;
using Microsoft.Azure.WebJobs;
using System.IO;

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
            collector.CollectMail();

        }
    }
}
