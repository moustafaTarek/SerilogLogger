using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerilogLib.Entities
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string ClientIpAddress { get; set; }
        public string RequestMethod { get; set; }
        public string RequestPath { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string Exception { get; set; }
        public string QueryParameters { get; set; }
        public Guid CorrelationId { get; set; }
        public string LogLocation { get; set; }
        [Column(TypeName = "jsonb")]
        public string Properties { get; set; }
        [Column(TypeName = "jsonb")]
        public string PropsTest { get; set; }
    }
}
