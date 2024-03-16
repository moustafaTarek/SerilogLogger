using Microsoft.Extensions.Logging;
namespace SerilogLib.Services
{
    internal class SerilogLoggerProvider : ILoggerProvider
    {
        private readonly SerilogService _logger;

        public SerilogLoggerProvider(SerilogService serilogService)
        {
            _logger = serilogService;
        }

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            return _logger;
        }

        public void Dispose()
        {
            (_logger as IDisposable)?.Dispose();
        }
    }
}