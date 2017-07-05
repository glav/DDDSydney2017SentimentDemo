using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Data;

namespace Website.Domain
{
    public class ViewModelEngine
    {
        private MailJobRepository _repo = new MailJobRepository();

        public async Task<List<EmailInformation>> GetSentimentViewModelsAsync()
        {
            var emails = await _repo.GetAnalysedMailAsync();
            foreach (var mail in emails)
            {
                SetSentimentProps(mail);
            }
            return emails.ToList();

        }

        private void SetSentimentProps(EmailInformation email)
        {
            if (email.SentimentScore > 0.75)
            {
                email.SentimentCss = "positive";
                email.SentimentText = "Really positive";
                return;
            }
            if (email.SentimentScore > 0.5 && email.SentimentScore <= 0.75)
            {
                email.SentimentCss = "mild-positive";
                email.SentimentText = "Somewhat positive";
                return;
            }
            if (email.SentimentScore == 0.5)
            {
                email.SentimentCss = "neutral";
                email.SentimentText = "Neutral";
                return;
            }
            if (email.SentimentScore < 0.5 && email.SentimentScore > 0.25)
            {
                email.SentimentCss = "mild-negative";
                email.SentimentText = "Mildly negative";
                return;
            }
            email.SentimentCss = "negative";
            email.SentimentText = "Really negative";

        }
    }
}
