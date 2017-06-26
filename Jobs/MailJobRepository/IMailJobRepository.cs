using Jobs.Core;
using Jobs.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailJobRepository
{
    public interface IMailJobRepository
    {
        Task StoreMailMessagesAsync(IEnumerable<EmailInformation> mailMessages);
        Task ClearAllMailMessagesAsync();
        Task<IEnumerable<EmailInformation>> GetMailCollectedToBeAnalysed();
        Task UpdateMessageAsync(EmailInformation emailMsg);
    }
}
