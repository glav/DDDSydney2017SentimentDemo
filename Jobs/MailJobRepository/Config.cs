using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailJobRepository
{
    public class Config
    {
        public Config()
        {
            ParseConfig();

        }

        private void ParseConfig()
        {
            DocumentDbName = ConfigurationManager.AppSettings["Data.documentDbName"];
            DocumentDbCollection = ConfigurationManager.AppSettings["Data.documentDbCollection"];
            DocumentDbEndpoint = ConfigurationManager.AppSettings["Data.documentDbEndpoint"];
            DocumentDbAccessKey = ConfigurationManager.AppSettings["Data.documentDbAccessKey"];
        }

        public string DocumentDbEndpoint { get; private set; }
        public string DocumentDbAccessKey { get; private set; }
        public string DocumentDbName { get; private set; }
        public string DocumentDbCollection { get; private set; }
    }
}
