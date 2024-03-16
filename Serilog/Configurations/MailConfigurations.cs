using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerilogLib.Configurations
{
    public class MailConfigurations
    {
        public bool LogToMail { get; set; }
        public string SendFrom { get; set; }
        public List<string> SendTo { get; set; }
    }
}
