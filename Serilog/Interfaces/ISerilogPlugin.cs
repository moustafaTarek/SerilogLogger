using Serilog;
using SerilogLib.Configurations;

namespace SerilogLib.Interfaces
{
    public interface ISerilogPlugin
    {
        public SerilogConfigurations _serilogConfigurations { get; init; }
        public LoggerConfiguration PlugIn(LoggerConfiguration logger);
    }
}
