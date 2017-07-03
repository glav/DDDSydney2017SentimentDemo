using EmailCollector.CommandProcessors;
using Jobs.Core;
using Jobs.Core.Diagnostics;
using Microsoft.Azure.WebJobs;
using System.IO;

namespace EmailCollector
{
    public class QueueTriggerCollection
    {
        public static void ProcessQueueMessage([QueueTrigger(CloudStorageAssets.EmailQueueName)]string message, TextWriter logger)
        {

            var appConfig = new Config();
            var jobLogger = new JobLogger(logger);
            var repo = new MailJobRepository.MailJobRepository();

            jobLogger.WriteLine($"Message received in the [{CloudStorageAssets.EmailQueueName}], Content: [{message}]");

            var jobCommand = message.ToQueueCommand();

            if (jobCommand == JobCommand.CollectEmail)
            {
                jobLogger.WriteLine($"Collecting email");
                var collector = new MailCollectionProcessor(appConfig, repo, jobLogger);
                collector.CollectMail();
            } else
            {
                jobLogger.WriteLine($"Doing nothing as queue command is not one requiring collection action");
            }
        

        }
    }
}
