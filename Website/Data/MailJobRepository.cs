using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace Website.Data
{
    public class MailJobRepository
    {
        private readonly DocumentClient _client;

        public MailJobRepository()
        {
           _client = new DocumentClient(new Uri(Config.DocumentDbEndpoint), Config.DocumentDbAccessKey);
        }

        public Task ClearAllMailMessagesAsync()
        {
            return _client.DeleteDocumentCollectionAsync(Config.DocumentDbEndpoint);
        }

        private Uri DocumentCollectionUri => UriFactory.CreateDocumentCollectionUri(Config.DocumentDbName, Config.DocumentDbCollection);

        public async Task<IEnumerable<EmailInformation>> GetAnalysedMailAsync()
        {
            await EnsureSetup();
            var query = _client.CreateDocumentQuery<EmailInformation>(DocumentCollectionUri, new FeedOptions { EnableCrossPartitionQuery = false, PartitionKey = new PartitionKey(JobPartitionKeys.AnalysedMail) })
               .Where(f => f.HasBeenAnalysed == true)
               .OrderByDescending(o => o.TimeOfMail)
               .ToList();
            return query;
        }

        private async Task EnsureSetup()
        {
            await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = Config.DocumentDbName });
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Config.DocumentDbName), new DocumentCollection { Id = Config.DocumentDbCollection });
        }
    }
}
