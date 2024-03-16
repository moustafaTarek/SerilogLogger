using Microsoft.Extensions.Logging;
using SerilogLib.Entities;

namespace SerilogLib.Configurations
{
    public class ConsoleConfigurations
    {
        public bool LogToConsole { get; set; }
        
        public LogLevel MiniumunLogLevel { get; set; }

        public string OutputTemplete { get; set; } = $"IpAddress: [{{{nameof(LogEntry.ClientIpAddress)}}}] || Correlation: [{{{nameof(LogEntry.CorrelationId)}}}] || Method: {{{nameof(LogEntry.RequestMethod)}}} || Endpoint: {{{nameof(LogEntry.RequestPath)}}} || CreatedDate: [{{Timestamp:HH:mm:ss}} || Level:{{Level:u3}}] Messsage: {{Message:lj}} || Exception:{{{nameof(LogEntry.Exception)}}}{{NewLine}}{{NewLine}}{{NewLine}}{{NewLine}}";
    }
}
