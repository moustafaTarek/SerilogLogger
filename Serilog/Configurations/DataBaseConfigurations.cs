using Microsoft.Extensions.Logging;
using SerilogLib.Enums;

namespace SerilogLib.Configurations
{
    public class DataBaseConfigurations
    {
        public bool LogToDataBase { get; set; }
        public DataBaseProvider DataBaseProvider { get; set; }
        public LogLevel MiniumunLogLevel { get; set; }
        public string DbConnection { get; set; }
    }
}
