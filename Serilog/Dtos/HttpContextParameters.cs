using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerilogLib.Dtos
{
    public class HttpContextParameters
    {
        public string RequestMethod { get; }
        public string RequestPath { get; }
        public string? RequestQuery { get; }
        public string? IpAddress { get; }

        public HttpContextParameters(string requestPath, string requestMethod, string? requestQuery, string? ipAddress)
        {
            RequestMethod = requestMethod;
            RequestPath = requestPath;
            RequestQuery = requestQuery;
            IpAddress = ipAddress;
        }
    }
}
