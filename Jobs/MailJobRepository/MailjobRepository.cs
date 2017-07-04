using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobs.Core;
using Microsoft.Azure.Documents;
using Jobs.Core.Data;

namespace MailJobRepository
{
    public class MailJobRepository : IMailJobRepository
    {
        private readonly DocumentClient _client;
        private readonly Config _config = new Config();

        public MailJobRepository()
        {
            _client = new DocumentClient(new Uri(_config.DocumentDbEndpoint), _config.DocumentDbAccessKey);
        }

        public Task ClearAllMailMessagesAsync()
        {
            return _client.DeleteDocumentCollectionAsync(DocumentCollectionUri);
        }

        private Uri DocumentCollectionUri => UriFactory.CreateDocumentCollectionUri(_config.DocumentDbName, _config.DocumentDbCollection);
        public async Task StoreMailMessagesAsync(IEnumerable<EmailInformation> mailMessages)
        {
            await EnsureSetup();
            foreach (var msg in mailMessages)
            {
                var response = await _client.CreateDocumentAsync(DocumentCollectionUri, msg);

            }
        }

        public async Task UpdateMessageAsync(EmailInformation emailMsg)
        {
            await EnsureSetup();
            var query = _client.CreateDocumentQuery<EmailInformation>(DocumentCollectionUri)
               .Where(f => f.id == emailMsg.id).FirstOrDefault();
            if (query != null)
            {
                await _client.UpsertDocumentAsync(DocumentCollectionUri, emailMsg);
            }
        }

        public async Task<IEnumerable<EmailInformation>> GetMailCollectedToBeAnalysedAsync()
        {
            await EnsureSetup();
            var query = _client.CreateDocumentQuery<EmailInformation>(DocumentCollectionUri,new FeedOptions {EnableCrossPartitionQuery = false, PartitionKey = new PartitionKey(JobPartitionKeys.NewMail) })
               .Where(f => f.HasBeenAnalysed == false).ToList();
            return query;
        }

        private async Task EnsureSetup()
        {
            await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = _config.DocumentDbName });
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_config.DocumentDbName), new DocumentCollection { Id = _config.DocumentDbCollection });
        }
    }
}
