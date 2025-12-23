using System.Text.Json;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog.Formatting;

namespace SerilogLib.Configurations
{
    public class LokiConfigurations
    {
        public bool LogToLoki { get; set; }
        public string Url { get; set; }
        public LogLevel MiniumunLogLevel { get; set; }
        public LokiTextFormatter LokiTextFormatter { get; set; } = new();
    }

    public class LokiTextFormatter : ITextFormatter
    {
        public void Format(LogEvent logEvent, TextWriter output)
        {
            LogEventPropertyValue logEventPropertyValue;

            var payload = new
            {
                traceId = logEvent.Properties.TryGetValue("CorrelationId", out logEventPropertyValue) == true ? logEventPropertyValue.ToString() : "",
                level = logEvent.Level.ToString(),
                message = logEvent.RenderMessage(),
                exception = logEvent.Exception?.ToString(),
            };

            output.Write(JsonSerializer.Serialize(payload));
        }
    }
}
