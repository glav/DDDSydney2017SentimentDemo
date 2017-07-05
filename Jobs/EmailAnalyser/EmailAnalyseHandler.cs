using MailJobRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glav.CognitiveServices.FluentApi.Core.Configuration;
using Glav.CognitiveServices.FluentApi.TextAnalytic;
using Glav.CognitiveServices.FluentApi.Core;
using Glav.CognitiveServices.FluentApi.Core.Diagnostics;
using Glav.CognitiveServices.FluentApi.TextAnalytic.Domain;
using Jobs.Core.Diagnostics;

namespace EmailAnalyser
{
    public class EmailAnalyseHandler
    {
        private readonly IMailJobRepository _repository;
        private readonly Config _config;
        private readonly JobLogger _logger;

        public EmailAnalyseHandler(IMailJobRepository repository, Config config, JobLogger logger)
        {
            _repository = repository;
            _config = config;
            _logger = logger;
        }

        public async Task AnalyseAsync()
        {
            _logger.WriteLine("In AnalyseAsync");

            var mails = await _repository.GetMailCollectedToBeAnalysedAsync();

            _logger.WriteLine($"Collected #{mails?.Count()} mail messages");

            if (mails != null && mails.Count() > 0)
            {

                foreach (var mailMsg in mails)
                {
                    _logger.WriteLine($"Processing mail: [{mailMsg.From}]");

                    var result = await TextAnalyticConfigurationSettings.CreateUsingConfigurationKeys(_config.TextanalyticApiKey, LocationKeyIdentifier.WestUs)
                        .AddConsoleDiagnosticLogging()
                        .UsingHttpCommunication()
                        .WithTextAnalyticAnalysisActions()
                        .AddSentimentAnalysis(mailMsg.Body)
                        .AnalyseAllSentimentsAsync();
                    if (result.TextAnalyticSentimentAnalysis.AnalysisResult.ActionSubmittedSuccessfully)
                    {
                        _logger.WriteLine("Mail processed ok");
                        var mailClassification = result.TextAnalyticSentimentAnalysis.AnalysisResult.ResponseData.documents[0].score;
                        mailMsg.HasBeenAnalysed = true;
                        mailMsg.partitionKey = "analysedmail";
                        mailMsg.SentimentScore = mailClassification;
                        await _repository.UpdateMessageAsync(mailMsg);
                    } else
                    {
                        _logger.WriteLine("Mail did not process successfully.");
                    }
                }
            }
        }
    }
}
