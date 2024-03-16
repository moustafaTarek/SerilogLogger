using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace SerilogLib.Util
{
    public static class SerilogUtilities
    {
        internal static LogEventLevel ConvertMicrosoftLogLevelToSerilogLogLevel(LogLevel microSoftLogLevel)
        {
            switch (microSoftLogLevel)
            {
                case LogLevel.Trace:
                    return LogEventLevel.Verbose;

                case LogLevel.Debug:
                    return LogEventLevel.Debug;

                case LogLevel.Information:
                    return LogEventLevel.Information;

                case LogLevel.Warning:
                    return LogEventLevel.Warning;

                case LogLevel.Error:
                    return LogEventLevel.Error;

                case LogLevel.Critical:
                case LogLevel.None:
                    return LogEventLevel.Fatal;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
