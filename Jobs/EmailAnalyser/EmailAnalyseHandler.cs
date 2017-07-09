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
                        .AddKeyPhraseAnalysis(mailMsg.Body)
                        .AnalyseAllSentimentsAsync();

                    await UpdateMailMessage(mailMsg, result);
                }
            }
        }

        private async Task UpdateMailMessage(Jobs.Core.Data.EmailInformation mailMsg, TextAnalyticAnalysisResults result)
        {
            if (result.TextAnalyticSentimentAnalysis.AnalysisResult.ActionSubmittedSuccessfully)
            {
                _logger.WriteLine("Mail processed ok");
                var mailClassification = result.TextAnalyticSentimentAnalysis.AnalysisResult.ResponseData.documents[0].score;
                if (result.TextAnalyticKeyPhraseAnalysis != null && result.TextAnalyticKeyPhraseAnalysis.AnalysisResult != null)
                {
                    _logger.WriteLine("Keyphrase: Has some results");
                    if (result.TextAnalyticKeyPhraseAnalysis.AnalysisResult.ActionSubmittedSuccessfully)
                    {
                        _logger.WriteLine("Action submitted successfully.");
                        var keyResult = result.TextAnalyticKeyPhraseAnalysis.AnalysisResult;
                        if (keyResult.ResponseData != null && keyResult.ResponseData.documents.Length > 0)
                        {
                            var keyPhrases = keyResult.ResponseData.documents[0]?.keyPhrases[0];
                            mailMsg.KeyPhrases = keyPhrases;
                        }
                        else
                        {
                            _logger.WriteLine("No data in keyphrase analysis");
                        }
                    } else
                    {
                        _logger.WriteLine("Key{Phrase analysis did not work.");
                        _logger.WriteLine($"Error: {result.TextAnalyticKeyPhraseAnalysis.AnalysisResult.ResponseData.errors.FirstOrDefault().message} ");
                    }
                }
                else
                {
                    _logger.WriteLine("KeyPhrase analysis did not work - no results.");
                }
                _logger.WriteLine("Updating mail store");
                mailMsg.HasBeenAnalysed = true;
                mailMsg.partitionKey = "analysedmail";
                mailMsg.SentimentScore = mailClassification;
                await _repository.UpdateMessageAsync(mailMsg);
            }
            else
            {
                _logger.WriteLine("Mail did not process successfully.");
            }
        }
    }
}
