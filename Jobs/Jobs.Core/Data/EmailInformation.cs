﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobs.Core.Data
{
    public class EmailInformation
    {
        public EmailInformation()
        {
            id = Guid.NewGuid().ToString();
            partitionKey = "newmail";
        }
        public string id { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public DateTime TimeOfMail { get; set; }
        public bool HasBeenAnalysed { get; set; }
        public DateTime DateTimeProcessed { get; set; }
        public string partitionKey { get; set; }

        public double SentimentScore { get; set; }
        public string KeyPhrases { get; set; }
        
    }
}
