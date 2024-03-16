using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NpgsqlTypes;
using Serilog.Context;
using SerilogLib.Dtos;
using SerilogLib.Entities;

namespace SerilogLib.SerilogMiddlewares
{
    public class SerilogLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public SerilogLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Guid CorrelationId = Guid.NewGuid();
            HttpContextParameters httpContextParameters = ParseHttpContext(context);

            LogContext.PushProperty(nameof(LogEntry.CorrelationId), CorrelationId);
            LogContext.PushProperty(nameof(LogEntry.RequestMethod), httpContextParameters.RequestMethod);
            LogContext.PushProperty(nameof(LogEntry.RequestPath), httpContextParameters.RequestPath);
            LogContext.PushProperty(nameof(LogEntry.QueryParameters), httpContextParameters.RequestQuery);
            LogContext.PushProperty(nameof(LogEntry.ClientIpAddress), httpContextParameters.IpAddress);

            await _next(context);
        }

        private static HttpContextParameters ParseHttpContext(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress;
            var request = context.Request;
            string requestPath = request.Path.ToString();
            string requestMethod = request.Method;
            string? requestQuery = request.QueryString.Value;
            requestQuery = string.IsNullOrWhiteSpace(requestQuery) ? null : requestQuery;

            return new HttpContextParameters(requestPath, requestMethod, requestQuery, ipAddress?.ToString());
        }
    }

    public  static class SerilogLoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseSerilogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SerilogLoggerMiddleware>();
        }
    }
}
