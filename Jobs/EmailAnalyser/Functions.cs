using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Jobs.Core;

namespace EmailAnalyser
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger(CloudStorageAssets.AnalyseQueueName)] string message, TextWriter log)
        {
            log.WriteLine($"received message on{CloudStorageAssets.AnalyseQueueName} queue ");
            log.WriteLine(message);
            var testclass = new DDD2017DemoAnalyser.Class1();
            testclass.Test();
        }
    }
}
