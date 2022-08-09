using Beacons.Data;
using Beacons.Options;
using Beacons.Services.Beacons;
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

            services.Configure<DatabaseOptions>(configuration.GetSection("Database"))
                .AddTransient<IBeaconService, BeaconService>();

            AddDatabase(services);

            return services;
        }

        private static void AddDatabase(IServiceCollection services)
        {
            services.AddDbContext<Context>((provider, options) =>
            {
                var dbOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                options.UseCosmos(dbOptions.AccountEndpoint, dbOptions.AccountKey, dbOptions.DatabaseName);
            });
        }
    }
}
