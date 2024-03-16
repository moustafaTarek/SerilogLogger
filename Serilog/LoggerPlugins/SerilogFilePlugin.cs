using Serilog;
using SerilogLib.Configurations;
using SerilogLib.Interfaces;
using SerilogLib.Util;

namespace SerilogLib.LoggerPlugins
{
    internal class SerilogFilePlugin : ISerilogPlugin
    {
        public SerilogConfigurations _serilogConfigurations { get; init; }

        public SerilogFilePlugin(SerilogConfigurations serilogConfigurations)
        {
            _serilogConfigurations = serilogConfigurations;
        }

        public LoggerConfiguration PlugIn(LoggerConfiguration loggerConfiguration)
        {
            var fileConfigurations = _serilogConfigurations.FileConfigurations;


            if (fileConfigurations.LogToFile)
            {
                string filePath = string.IsNullOrWhiteSpace(fileConfigurations.FilePath)? fileConfigurations.FileName : Path.Combine(fileConfigurations.FilePath, fileConfigurations.FileName);


                loggerConfiguration
                    .WriteTo
                    .File(path: filePath,
                          restrictedToMinimumLevel: SerilogUtilities.ConvertMicrosoftLogLevelToSerilogLogLevel(fileConfigurations.MiniumunLogLevel), 
                          rollingInterval: fileConfigurations.RollingInterval,
                          rollOnFileSizeLimit: fileConfigurations.rollOnFileSizeLimit, 
                          fileSizeLimitBytes: fileConfigurations.FileSizeLimitBytes, 
                          retainedFileCountLimit: fileConfigurations.MaximumNumberOfLogFiles,
                          outputTemplate: fileConfigurations.OutputTemplete 
                          );  
            }

            return loggerConfiguration;
        }
    }
}
