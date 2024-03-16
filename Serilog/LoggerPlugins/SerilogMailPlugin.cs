using Microsoft.Extensions.Configuration;
using Serilog;
using SerilogLib.Configurations;
using SerilogLib.Interfaces;

namespace SerilogLib.LoggerPlugins
{
    internal class SerilogMailPlugin : ISerilogPlugin
    {
        public SerilogConfigurations _serilogConfigurations { get; init; }

        public SerilogMailPlugin(SerilogConfigurations serilogConfigurations)
        {
            _serilogConfigurations = serilogConfigurations;
        }

        public LoggerConfiguration PlugIn(LoggerConfiguration loggerConfiguration)
        {
            var mailConfigurations = _serilogConfigurations.MailConfigurations;

            if (mailConfigurations.LogToMail)
            {

            }

            return loggerConfiguration;
        }
    }
}
