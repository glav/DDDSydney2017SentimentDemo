using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailAnalyser
{
    public class Config
    {
        public Config()
        {
            ParseConfig();

        }

        private void ParseConfig()
        {
            TextanalyticApiKey = ConfigurationManager.AppSettings["ApiKeys.TextAnalytic"];
        }

        public string TextanalyticApiKey { get; private set; }
    }
}
