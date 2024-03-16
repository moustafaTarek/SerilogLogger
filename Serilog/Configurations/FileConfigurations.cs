using Microsoft.Extensions.Logging;
using Serilog;
using SerilogLib.Entities;

namespace SerilogLib.Configurations
{
    public class FileConfigurations
    {
        public bool LogToFile { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; } = "log-.txt";
        public LogLevel MiniumunLogLevel { get; set; }
        public RollingInterval RollingInterval { get; set; } = RollingInterval.Day;
        public int? MaximumNumberOfLogFiles { get; set; } = null;
        public long FileSizeLimitBytes { get; set; } = 2_000_000_000;
        public bool rollOnFileSizeLimit { get; set; } = false;
        public string OutputTemplete { get; set; } = $"IpAddress: [{{{nameof(LogEntry.ClientIpAddress)}}}] || Correlation: [{{{nameof(LogEntry.CorrelationId)}}}] || Method: {{{nameof(LogEntry.RequestMethod)}}} || Endpoint: {{{nameof(LogEntry.RequestPath)}}} || CreatedDate: [{{Timestamp:HH:mm:ss}} || Level:{{Level:u3}}] Messsage: {{Message:lj}} || Exception:{{{nameof(LogEntry.Exception)}}}{{NewLine}}{{NewLine}}{{NewLine}}{{NewLine}}";

    }
}
