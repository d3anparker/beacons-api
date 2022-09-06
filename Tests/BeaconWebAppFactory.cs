using Beacons.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    public class BeaconWebAppFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<Context>));

                if(descriptor is null)
                {
                    throw new InvalidOperationException();
                }

                services.Remove(descriptor);

                services.AddDbContext<Context>(options =>
                {
                    options.UseInMemoryDatabase("IntegrationTest");
                });

                var provider = services.BuildServiceProvider();

                using var scope = provider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<Context>();

                SetUpDatabase(context);
            });
        }

        private void SetUpDatabase(Context context)
        {
            context.Database.EnsureCreated();

            context.Beacons.Add(new Beacon()
            {
                Id = Guid.Parse("9b338478-b739-4f2a-8c7d-b99038600b81"),
                Latitude = 50,
                Longitude = 50,
                Created = DateTime.Parse("1 Jan 2022 10:00"),
                Expiry = DateTime.Parse("1 Jan 2022 12:00")
            });

            context.SaveChanges();
        }
    }
}
