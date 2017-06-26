using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobs.Core
{
    public static class JobCommandsExtensions
    {
        public static JobCommand ToQueueCommand(this string textMessage)
        {
            if (string.IsNullOrWhiteSpace(textMessage))
            {
                return JobCommand.CollectEmail;
            }

            var normalisedMessage = textMessage.ToLowerInvariant();
            if (normalisedMessage == JobCommandValues.AddSampleData)
            {
                return JobCommand.AddSampleData;
            }
            if (normalisedMessage == JobCommandValues.ClearStore)
            {
                return JobCommand.ClearDataStore;
            }

            return JobCommand.CollectEmail;
        }
    }
}
