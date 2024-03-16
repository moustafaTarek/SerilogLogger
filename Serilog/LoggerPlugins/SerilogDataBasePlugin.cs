using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using Serilog.Sinks.PostgreSQL;
using SerilogLib.Configurations;
using SerilogLib.Interfaces;
using Microsoft.Extensions.Configuration;
using SerilogLib.Enums;
using Serilog.Events;
using SerilogLib.Entities;
using SerilogLib.Util;
using SerilogLib.Exceptions;

namespace SerilogLib.LoggerPlugins
{
    internal class SerilogDataBasePlugin : ISerilogPlugin
    {
        public SerilogConfigurations _serilogConfigurations { get; init; }

        public SerilogDataBasePlugin(SerilogConfigurations serilogConfigurations)
        {
            _serilogConfigurations = serilogConfigurations;
        }

        public LoggerConfiguration PlugIn(LoggerConfiguration loggerConfiguration)
        {
            var databaseConfig = _serilogConfigurations.DataBaseConfigurations;
            
            if (databaseConfig.LogToDataBase)
            {
                switch (databaseConfig.DataBaseProvider)
                {
                    case DataBaseProvider.Postgres:
                        PlugInPostgreSql(loggerConfiguration, databaseConfig);
                        break;

                    default:
                        throw new NotSupportedDataBaseProvider($"DataBase : {databaseConfig.DataBaseProvider} is not yet supported in logging providers"); 
                }
            }

            return loggerConfiguration;
        }

        private LoggerConfiguration PlugInPostgreSql(LoggerConfiguration loggerConfiguration, DataBaseConfigurations dataBaseConfigurations)
        {
            LogEventLevel serilogLogLevel = SerilogUtilities.ConvertMicrosoftLogLevelToSerilogLogLevel(dataBaseConfigurations.MiniumunLogLevel);

            loggerConfiguration.WriteTo.PostgreSQL(connectionString: dataBaseConfigurations.DbConnection,
                                                   tableName: nameof(LogEntry),
                                                   columnOptions: GetDataBaseColumns(),
                                                   needAutoCreateTable: true,
                                                   restrictedToMinimumLevel: serilogLogLevel
                                                   );

            return loggerConfiguration;
        }

        private IDictionary<string, ColumnWriterBase> GetDataBaseColumns()
        {
            IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
            {
                {nameof(LogEntry.Id), new IdAutoIncrementColumnWriter()},
                {nameof(LogEntry.CorrelationId), new  SinglePropertyColumnWriter(nameof(LogEntry.CorrelationId), PropertyWriteMethod.Raw, NpgsqlDbType.Uuid)},
                {nameof(LogEntry.RequestPath), new SinglePropertyColumnWriter(nameof(LogEntry.RequestPath), PropertyWriteMethod.Raw, NpgsqlDbType.Text) },
                {nameof(LogEntry.ClientIpAddress), new SinglePropertyColumnWriter(nameof(LogEntry.ClientIpAddress), PropertyWriteMethod.Raw, NpgsqlDbType.Text, "l") },
                {nameof(LogEntry.QueryParameters), new SinglePropertyColumnWriter(nameof(LogEntry.QueryParameters), PropertyWriteMethod.Raw, NpgsqlDbType.Text) },
                {nameof(LogEntry.RequestMethod), new SinglePropertyColumnWriter(nameof(LogEntry.RequestMethod), PropertyWriteMethod.Raw, NpgsqlDbType.Text) },
                {nameof(LogEntry.Message), new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
                {nameof(LogEntry.EventId), new SinglePropertyColumnWriter(nameof(LogEntry.EventId), PropertyWriteMethod.Raw, NpgsqlDbType.Integer) },
                {nameof(LogEntry.EventName), new SinglePropertyColumnWriter(nameof(LogEntry.EventName), PropertyWriteMethod.Raw, NpgsqlDbType.Text) },
                {nameof(LogEntry.MessageTemplate), new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
                {nameof(LogEntry.Level), new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                {nameof(LogEntry.CreatedDate), new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
                {nameof(LogEntry.Exception), new ExceptionColumnWriter(NpgsqlDbType.Text) },
                {nameof(LogEntry.LogLocation), new SinglePropertyColumnWriter(nameof(LogEntry.LogLocation), PropertyWriteMethod.Raw, NpgsqlDbType.Text) },
                {nameof(LogEntry.Properties), new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
                {nameof(LogEntry.PropsTest), new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
            };
            return columnWriters;
        }
    }
}