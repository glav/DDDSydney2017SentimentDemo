using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobs.Core
{
    public class EmailInformation
    {
        public string From { get; set; }
        public string Body { get; set; }
        public DateTime TimeOfMail { get; set; }
        
        //add in mail sentiment rating - positive, negative etc..
        // also add other emotions perhaps, anger, contempt etc.
    }
}
