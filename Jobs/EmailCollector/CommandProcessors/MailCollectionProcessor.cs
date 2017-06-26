using Jobs.Core;
using Jobs.Core.Data;
using Jobs.Core.Diagnostics;
using MailJobRepository;
using OpenPop.Pop3;
using System.Collections.Generic;

namespace EmailCollector.CommandProcessors
{
    public class MailCollectionProcessor
    {
        private readonly Config _config;
        private readonly JobLogger _logger;
        private readonly IMailJobRepository _repository;

        public MailCollectionProcessor(Config config, IMailJobRepository repository, JobLogger logger)
        {
            _config = config;
            _logger = logger;
            _repository = repository;
        }

        public void CollectMail()
        {
            var messages = GetLatestMail();
            StoreCollectedMail(messages);
        }

        private List<EmailInformation> GetLatestMail()
        {
            _logger.WriteLine("Getting latest mail");

            using (var client = new Pop3Client())
            {
                _logger.WriteLine($"Connecting to host:[{_config.Hostname}], port:[{_config.Port}]");
                // Connect to the server
                client.Connect(_config.Hostname, _config.Port, _config.UseSSL);

                _logger.WriteLine($"Authenticating with user:[{_config.Username}]");
                // Authenticate ourselves towards the server
                client.Authenticate(_config.Username, _config.Password);

                // Get the number of messages in the inbox
                int messageCount = client.GetMessageCount();
                _logger.WriteLine($"#{messageCount} messages are pending collection");

                // We want to download all messages
                var allMessages = new List<EmailInformation>(messageCount);

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                for (int i = messageCount; i > 0; i--)
                {
                    var headers = client.GetMessageHeaders(i);
                    var msg = client.GetMessage(i);
                    allMessages.Add(new EmailInformation { From = headers.From?.Address, Body = msg.FindFirstPlainTextVersion().GetBodyAsText(), TimeOfMail = headers.DateSent.Date, partitionKey="newmail" });
                }

                _logger.WriteLine($"#{messageCount} messages have been collected");

                // Now do something with the messages
                return allMessages;
            }
        }

        private async void StoreCollectedMail(List<EmailInformation> messages)
        {
            if (messages== null)
            {
                return;
            }

            _logger.WriteLine($"Storing #{messages.Count} messages in the repository");

            await _repository.StoreMailMessagesAsync(messages);
        }
    }
}
