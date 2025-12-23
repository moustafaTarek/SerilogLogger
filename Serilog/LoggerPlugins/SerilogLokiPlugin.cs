
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using SerilogLib.Configurations;
using SerilogLib.Interfaces;
using SerilogLib.Util;

namespace SerilogLib.LoggerPlugins
{
    internal class SerilogLokiPlugin : ISerilogPlugin
    {
        public SerilogConfigurations _serilogConfigurations { get; init; }

        public SerilogLokiPlugin(SerilogConfigurations serilogConfigurations)
        {
            _serilogConfigurations = serilogConfigurations;
        }

        public LoggerConfiguration PlugIn(LoggerConfiguration logger)
        {
            if (_serilogConfigurations.LokiConfigurations.LogToLoki)
            {
                var logLevel = _serilogConfigurations.LokiConfigurations.MiniumunLogLevel;

                logger.WriteTo.GrafanaLoki(
                    uri: _serilogConfigurations.LokiConfigurations.Url,
                    restrictedToMinimumLevel: SerilogUtilities.ConvertMicrosoftLogLevelToSerilogLogLevel(logLevel),
                    textFormatter: _serilogConfigurations.LokiConfigurations.LokiTextFormatter);
            }

            return logger;
        }
    }
}
