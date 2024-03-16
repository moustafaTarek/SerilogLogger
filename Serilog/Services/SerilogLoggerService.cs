using Microsoft.Extensions.Logging;
using SerilogLib.Interfaces;
using SerilogLogger = Serilog.ILogger;
using Serilog;
using SerilogLib.Extensions;
using Serilog.Events;
using SerilogLib.Entities;
using SerilogLib.Util;

namespace SerilogLib.Services
{
    internal class SerilogService : Microsoft.Extensions.Logging.ILogger
    {
        private SerilogLogger _serilogLogger;

        public SerilogService(IEnumerable<ISerilogPlugin> serilogPlugins)
        {
            LoggerConfiguration loggerConfiguration = new LoggerConfiguration();

            foreach (var newPlugin in serilogPlugins)
            {
                loggerConfiguration.Plug(newPlugin);
            }

            _serilogLogger = loggerConfiguration
                             .MinimumLevel.Verbose()
                             .Enrich
                             .FromLogContext()
                             .CreateLogger();
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return _serilogLogger.ForContext<TState>() as IDisposable;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _serilogLogger.IsEnabled(SerilogUtilities.ConvertMicrosoftLogLevelToSerilogLogLevel(logLevel));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            _serilogLogger = _serilogLogger
                            .ForContext(nameof(LogEntry.EventId), eventId.Id)
                            .ForContext(nameof(LogEntry.EventName), eventId.Name);

            switch (SerilogUtilities.ConvertMicrosoftLogLevelToSerilogLogLevel(logLevel))
            {
                case LogEventLevel.Verbose:
                    _serilogLogger.Verbose(exception, formatter(state, exception), logLevel, eventId); 
                    break;
                
                case LogEventLevel.Debug:
                    _serilogLogger.Debug(exception, formatter(state, exception), logLevel, eventId);
                    break;

                case LogEventLevel.Information:
                    _serilogLogger.Information(exception, formatter(state, exception), logLevel, eventId);
                    break;

                case LogEventLevel.Warning:
                    _serilogLogger.Warning(exception, formatter(state, exception), logLevel, eventId);
                    break;

                case LogEventLevel.Error:
                    _serilogLogger.Error(exception, formatter(state, exception), logLevel, eventId);
                    break;

                case LogEventLevel.Fatal:
                    _serilogLogger.Fatal(exception, formatter(state, exception), logLevel, eventId);
                    break;
            }
        }
    }
}
