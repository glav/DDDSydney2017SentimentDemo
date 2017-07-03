using Jobs.Core;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailCollector
{
    public class EmailCollectedEventTrigger
    {
        public void Signal()
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("CloudStorage.StorageConnectionString"));
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(CloudStorageAssets.AnalyseQueueName);
            queue.CreateIfNotExists();

            var message = new CloudQueueMessage(JobCommandValues.AnalyseCollectedEmail);
            queue.AddMessage(message);
        }
    }
}
