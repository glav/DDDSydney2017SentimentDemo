using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Data
{
    public static class Config
    {
        public static string DocumentDbName { get; private set; }
        public static string DocumentDbCollection { get; private set; }
        public static string DocumentDbAccessKey { get; private set; }
        public static string DocumentDbEndpoint { get; private set; }
        public static void SetConfig(string dbName, string dbCollection, string dbAccessKey, string dbEndpoint)
        {
            DocumentDbName = dbName;
            DocumentDbCollection = dbCollection;
            DocumentDbAccessKey = dbAccessKey;
            DocumentDbEndpoint = dbEndpoint;
        }
    }
}
