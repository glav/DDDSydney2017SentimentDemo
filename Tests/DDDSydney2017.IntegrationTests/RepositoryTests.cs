using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jobs.Core;
using System.Collections.Generic;
using System.Linq;
using Jobs.Core.Data;

namespace DDDSydney2017.IntegrationTests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void ShouldBeAbleToWriteASimpleFrigginRecord()
        {
            var repo = new MailJobRepository.MailJobRepository();

            var msgs = new List<EmailInformation>();

            //repo.ClearAllMailMessagesAsync().Wait();
            msgs.Add(new EmailInformation { Body = "test", From = "unit@test.com", TimeOfMail = DateTime.Now, partitionKey = JobPartitionKeys.NewMail });
            repo.StoreMailMessagesAsync(msgs).Wait();
            var result = repo.GetMailCollectedToBeAnalysed().Result.FirstOrDefault();
            Assert.IsNotNull(result);

        }
    }
}
