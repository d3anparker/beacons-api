using Beacons.Data;
using Beacons.Mapping;
using Beacons.Options;
using Beacons.Services.Beacons;
using Beacons.Services.Dates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Beacons.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBeaconServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
             {
                 options.AddDefaultPolicy(policy =>
                 {
                     var origins = configuration["AllowedOrigins"].Split(";", StringSplitOptions.RemoveEmptyEntries);

                     policy.WithOrigins(origins)
                         .AllowAnyHeader()
                         .AllowAnyMethod()
                         .WithExposedHeaders("*");
                 });
             });

            services.AddTransient<IBeaconService, BeaconService>()
                .AddTransient<IBeaconMapper, BeaconMapper>()
                .AddSingleton<IDateTime, DefaultDateTime>();

            AddDatabase(services, configuration);
            AddBeaconOptions(services, configuration);

            return services;
        }

        private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<DatabaseOptions>()
                .ValidateDataAnnotations()
                .Bind(configuration.GetSection("Database"));

            services.AddDbContext<Context>((provider, options) =>
            {
                var dbOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                options.UseCosmos(dbOptions.AccountEndpoint, dbOptions.AccountKey, dbOptions.DatabaseName);
            });
        }

        private static void AddBeaconOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<BeaconOptions>()
                .ValidateDataAnnotations()
                .Bind(configuration.GetSection("Beacons"));

            services.AddSingleton(provider => provider.GetRequiredService<IOptions<BeaconOptions>>().Value);
        }
    }
}
