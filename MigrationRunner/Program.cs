using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostBuilder builder = new();
builder.ConfigureDefaults(args);

builder.ConfigureServices(services =>
{
    services.AddDbContext<DogCatcherContext>(options =>
    {
        options.UseSqlServer("server=localhost;database=localdb;integrated security=sspi");
    });
});

IHost host = builder.Build();

await host.RunAsync();