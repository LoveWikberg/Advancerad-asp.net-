using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inlämning_Love_Wikberg.Models
{
    public class MailConfiguration
    {
        public bool IsDev { get; set; }
        public string[] BlindEmails { get; set; }
        public bool IsSendEmail { get; set; }
    }
}
