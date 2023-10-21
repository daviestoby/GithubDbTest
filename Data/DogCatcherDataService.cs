using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public interface IDogCatcherDataService
    {
        Task<DogContext> CreateDogAsync(string name);

        Task<CatcherContext> CreateCatcherAsync(string name);

        Task<PoundContext> CreatePoundAsync(string name);

        Task<IEnumerable<PoundContext>> GetPoundsAsync(string name);

        Task ApprehendDogAsync(int dogId, int catcherId, int poundId);
    }

    public class DogCatcherDataService : IDogCatcherDataService
    {
        private readonly DogCatcherContext context;

        public DogCatcherDataService(DogCatcherContext context)
        {
            this.context = context;
        }

        public Task ApprehendDogAsync(int dogId, int catcherId, int poundId)
        {
            throw new NotImplementedException();
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

        public async Task<IEnumerable<PoundContext>> GetPoundsAsync(string name)
        {
            var query = this.context.Pounds.AsNoTracking().Select(s => new { s.Id, s.Name });

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(s => s.Name == name);

            return await query.OrderBy(s => s.Id)
                .Take(10)
                .Select(s => new PoundContext(s.Id, s.Name))
                .ToListAsync();
        }
    }
}
