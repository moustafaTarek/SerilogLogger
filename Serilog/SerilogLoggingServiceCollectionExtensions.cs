using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using SerilogLib.Configurations;
using SerilogLib.Interfaces;
using SerilogLib.LoggerPlugins;
using SerilogLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerilogLib
{
    public static class SerilogLoggingServiceCollectionExtensions
    {
        private static IServiceCollection AddSerilogLoggerServices(this IServiceCollection ServiceCollection)
        {
            ServiceCollection.AddSingleton<ISerilogPlugin, SerilogConsolePlugin>();
            ServiceCollection.AddSingleton<ISerilogPlugin, SerilogDataBasePlugin>();
            ServiceCollection.AddSingleton<ISerilogPlugin, SerilogFilePlugin>();
            ServiceCollection.AddSingleton<ISerilogPlugin, SerilogMailPlugin>();
            ServiceCollection.AddSingleton<SerilogService>();

            return ServiceCollection;
        }

        public static ILoggingBuilder AddSerilogLoggerBuilder(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
        {
            loggingBuilder.ClearProviders();

            loggingBuilder.Services.AddSerilogLoggerServices();

            loggingBuilder.Services.AddSingleton<ILoggerProvider>(sp=>
            {
                SerilogService serilogService = sp.GetRequiredService<SerilogService>();
                return new SerilogLoggerProvider(serilogService);
            });

            
            loggingBuilder.Services.AddSingleton<SerilogConfigurations>(sp =>
            {
                return GetSerilogConfigurations(configuration);
            });

            return loggingBuilder;
        }


        public static ILoggingBuilder AddSerilogLoggerBuilder(this ILoggingBuilder loggingBuilder, IConfiguration configuration, string SerilogConnectionsString)
        {
            loggingBuilder.ClearProviders();

            loggingBuilder.Services.AddSerilogLoggerServices();

            loggingBuilder.Services.AddSingleton<ILoggerProvider>(sp =>
            {
                SerilogService serilogService = sp.GetRequiredService<SerilogService>();
                return new SerilogLoggerProvider(serilogService);
            });


            loggingBuilder.Services.AddSingleton<SerilogConfigurations>(sp =>
            {
                var serilogConfigurations = GetSerilogConfigurations(configuration);

                serilogConfigurations.DataBaseConfigurations.DbConnection = SerilogConnectionsString;

                return serilogConfigurations;
            });

            return loggingBuilder;
        }

        public static ILoggingBuilder AddSerilogLoggerBuilder(this ILoggingBuilder loggingBuilder, Func<IServiceProvider, SerilogConfigurations> SerilogCongFunc)
        {
            loggingBuilder.ClearProviders();

            loggingBuilder.Services.AddSerilogLoggerServices();

            loggingBuilder.Services.AddSingleton<ILoggerProvider>(sp =>
            {
                SerilogService serilogService = sp.GetRequiredService<SerilogService>();
                return new SerilogLoggerProvider(serilogService);
            });

            loggingBuilder.Services.AddSingleton<SerilogConfigurations>(sp =>
            {
                var serilogConfigurations = SerilogCongFunc(sp);
                return serilogConfigurations;
            });

            return loggingBuilder;
        }

        private static SerilogConfigurations GetSerilogConfigurations(IConfiguration configuration)
        {
            var serilogConfigurations = configuration.GetSection(nameof(SerilogConfigurations)).Get<SerilogConfigurations>();

            return serilogConfigurations;
        }

    }
}
