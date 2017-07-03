using Jobs.Core;
using Jobs.Core.Diagnostics;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailCollector
{
    public class EmailCollectedEventTrigger
    {
        private readonly JobLogger _logger;
        public EmailCollectedEventTrigger(JobLogger logger)
        {
            _logger = logger;
        }
        public void Signal()
        {
            _logger.WriteLine("Signaling Email collected");

            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings[ConfigKeys.CloudStorageConnectionString]);
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(CloudStorageAssets.AnalyseQueueName);
            queue.CreateIfNotExists();

            var message = new CloudQueueMessage(JobCommandValues.AnalyseCollectedEmail);
            queue.AddMessage(message);
        }
    }
}
