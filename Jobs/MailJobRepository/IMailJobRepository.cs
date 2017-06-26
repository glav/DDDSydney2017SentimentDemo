using Jobs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailJobRepository
{
    public interface IMailJobRepository
    {
        void StoreMailMessages(IEnumerable<EmailInformation> mailMessages);
        void ClearAllMailMessages();
    }
}
