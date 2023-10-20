using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    public class DbFixture : IDisposable
    {
        public DogCatcherContext Context { get; private set; }

        private readonly IServiceScope scope;
        private bool disposedValue;

        public DbFixture()
        {
            ConfigurationBuilder configBuilder = new();

            configBuilder.AddEnvironmentVariables();
            configBuilder.AddJsonFile("appSettings.json");

            IConfiguration configuration = configBuilder.Build();

            ServiceCollection collection = new();
            collection.AddDbContext<DogCatcherContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("db"));
            });

            ServiceProvider provider = collection.BuildServiceProvider();
            this.scope = provider.CreateScope();

            this.Context = this.scope.ServiceProvider.GetRequiredService<DogCatcherContext>();

            this.Context.Database.Migrate();

            this.Context.Database.EnsureCreated();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Context.Database.EnsureDeleted();
                    this.scope.Dispose();
                }

                disposedValue = true;
            }
        }



        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
