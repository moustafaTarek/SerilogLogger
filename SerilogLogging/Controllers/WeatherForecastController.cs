using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using Serilog.Sinks.PostgreSQL;
using SerilogLib.Interfaces;
using SerilogLib.Services;

namespace SerilogLogging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Microsoft.Extensions.Logging.ILogger<WeatherForecastController> _logger;

        EventId eventId = new EventId(10000, "info");
        EventId eventId2 = new EventId(20000, "warn");
        EventId eventId3 = new EventId(30000, "debug");
        EventId eventId4 = new EventId(40000, "trace");
        EventId eventId5 = new EventId(50000, "Error");
        EventId eventId6 = new EventId(60000, "Crtiical");


        public WeatherForecastController(Microsoft.Extensions.Logging.ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<string> Get()
        {
            var position = new { Latitude = 25, Longitude = 134 };
            var elapsedMs = 34;


            _logger.LogInformation(eventId, "information message Processed {@Position} in {Elapsed} ms", position, elapsedMs);
            _logger.LogWarning(eventId2, "warning Processed {@Position} in {Elapsed} ms", position, elapsedMs);

            _logger.LogDebug(eventId3, "debugging Processed {@Position} in {Elapsed} ms", position, elapsedMs);

            _logger.LogTrace(eventId4, "tracing Processed {@Position} in {Elapsed} ms", position, elapsedMs);
            _logger.LogError(eventId5, "message");
            _logger.LogCritical(eventId6, "Criticall");

            return "Done";
        }

        [HttpGet("Throws")]
        public async Task ThrowError()
        {
            try
            {
                throw new Exception();
            }
            catch (Exception ex) 
            {
                _logger.LogError(eventId5,ex, "message") ;
            }
        }
    }
}
