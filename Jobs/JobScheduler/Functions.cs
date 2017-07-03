using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Jobs.Core;
using Microsoft.WindowsAzure.Storage.Queue;

namespace JobScheduler
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessTimerEvent([TimerTrigger("0/30 * * * * *")] TimerInfo timer)
        {
            Console.WriteLine("Timer fired");

            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings[ConfigKeys.CloudStorageConnectionString]);
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(CloudStorageAssets.AnalyseQueueName);
            queue.CreateIfNotExists();

            var message = new CloudQueueMessage(JobCommandValues.CollectEmailCommand);
            queue.AddMessage(message);
        }
    }
}
