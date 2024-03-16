using Serilog;
using SerilogLib.Configurations;
using SerilogLib.Interfaces;
using SerilogLib.Util;

namespace SerilogLib.LoggerPlugins
{
    internal class SerilogConsolePlugin : ISerilogPlugin
    {
        public SerilogConfigurations _serilogConfigurations { get; init; }

        public SerilogConsolePlugin(SerilogConfigurations serilogConfigurations)
        {
             _serilogConfigurations = serilogConfigurations;
        }

        public LoggerConfiguration PlugIn(LoggerConfiguration loggerConfiguration)
        {
            var consoleConfiguration = _serilogConfigurations.ConsoleConfigurations;

            if (consoleConfiguration.LogToConsole)
            {
                loggerConfiguration.WriteTo.Console(restrictedToMinimumLevel: SerilogUtilities.ConvertMicrosoftLogLevelToSerilogLogLevel(consoleConfiguration.MiniumunLogLevel),
                                                    outputTemplate: consoleConfiguration.OutputTemplete);
            }
            
            return loggerConfiguration;
        }
    }
}
