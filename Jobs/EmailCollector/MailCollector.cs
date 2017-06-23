using Diagnostics;
using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailCollector
{
    public class MailCollector
    {
        private readonly Config _config;

        public MailCollector(Config config)
        {
            _config = config;
        }

        public void GetLatestMail()
        {
            using (var client = new Pop3Client())
            {
                // Connect to the server
                client.Connect(_config.Hostname, _config.Port, _config.UseSSL);

                // Authenticate ourselves towards the server
                client.Authenticate(_config.Username, _config.Password);

                // Get the number of messages in the inbox
                int messageCount = client.GetMessageCount();

                // We want to download all messages
                var allMessages = new List<Message>(messageCount);

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                for (int i = messageCount; i > 0; i--)
                {
                    allMessages.Add(client.GetMessage(i));
                }

                // Now do something with the messages
                //return allMessages;
            }
        }
    }
}
