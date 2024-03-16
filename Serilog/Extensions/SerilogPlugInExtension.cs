using Serilog;
using SerilogLib.Interfaces;
using SeriloLogger = Serilog.Core.Logger;

namespace SerilogLib.Extensions
{
    public static class SerilogPlugInExtension
    {
        public static LoggerConfiguration Plug(this LoggerConfiguration seriloLogger, ISerilogPlugin serilogPlugin)
        {
            return serilogPlugin.PlugIn(seriloLogger);
        }
    }
}
