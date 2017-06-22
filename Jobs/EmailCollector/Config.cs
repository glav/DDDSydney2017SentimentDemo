using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailCollector
{
    public class Config
    {
        public Config()
        {
            ParseConfig();

        }

        private void ParseConfig()
        {
            Hostname = ConfigurationManager.AppSettings["Email.hostname"];
            Username = ConfigurationManager.AppSettings["Email.username"];
            Password = ConfigurationManager.AppSettings["Email.password"];
            var portText = ConfigurationManager.AppSettings["Email.port"];
            var useSslText = ConfigurationManager.AppSettings["Email.useSsl"];
            if (!string.IsNullOrEmpty(useSslText) && useSslText.ToLowerInvariant() == "true")
            {
                UseSSL = true;
            }
            int portValue;
            if (!int.TryParse(portText, out portValue))
            {
                Port = 25;
            }
        }

        public string Hostname { get; private set; }
        public int Port { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool UseSSL { get; private set; }
    }
}
