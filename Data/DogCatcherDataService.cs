using Data.Models;

namespace Data
{
    public interface IDogCatcherDataService
    {
        Task<DogContext> CreateDogAsync(string name);

        Task<CatcherContext> CreateCatcherAsync(string name);

        Task<PoundContext> CreatePoundAsync(string name);
    }

    public class DogCatcherDataService : IDogCatcherDataService
    {
        private readonly DogCatcherContext context;

        public DogCatcherDataService(DogCatcherContext context)
        {
            this.context = context;
        }

        public async Task<CatcherContext> CreateCatcherAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            DogCatcher catcher = new()
            {
                Name = name
            };

            this.context.Catchers.Add(catcher);

            await this.context.SaveChangesAsync();

            return new(catcher.Id, catcher.Name);
        }

        public async Task<DogContext> CreateDogAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Dog doggo = new()
            {
                Name = name,
                Status = SystemState.Unknown
            };

            this.context.Dogs.Add(doggo);

            await this.context.SaveChangesAsync();

            return new(doggo.Id, doggo.Name, null, doggo.Status, false);
        }

        public async Task<PoundContext> CreatePoundAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Pound pound = new()
            {
                Name = name
            };

            this.context.Pounds.Add(pound);

            await this.context.SaveChangesAsync();

            return new(pound.Id, pound.Name);
        }
    }
}
