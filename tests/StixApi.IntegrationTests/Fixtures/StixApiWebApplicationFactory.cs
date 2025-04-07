using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StixApi.Persistance;

namespace StixApi.IntegrationTests.Fixtures;

public class StixApiWebApplicationFactory : WebApplicationFactory<Program>
{
    public IConfiguration? Configuration { get; private set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
            {
                Configuration = new ConfigurationBuilder()
                  .AddJsonFile("settings.json")
                  .Build();

                config.AddConfiguration(Configuration);
            });

        builder.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var dbContext = serviceProvider.GetService<StixDbContext>();

                dbContext?.Database.Migrate();
            });
    }
}
