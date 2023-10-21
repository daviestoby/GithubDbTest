using Data;
using Data.Models;

namespace Tests
{

    [Collection("Database collection")]
    public class CrudTests
    {
        private readonly DbFixture fixture;

        public CrudTests(DbFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Dogs()
        {
            // Arrange
            IDogCatcherDataService service = this.CreateSut();

            // Act
            DogContext fido = await service.CreateDogAsync("fido");

            // Assert
            Assert.NotNull(fido);
        }

        [Fact]
        public async Task Catchers()
        {
            // Arrange
            IDogCatcherDataService service = this.CreateSut();

            // Act
            CatcherContext gerald = await service.CreateCatcherAsync("gerald");

            // Assert
            Assert.NotNull(gerald);
        }

        [Fact]
        public async Task Pounds()
        {
            // Arrange
            IDogCatcherDataService service = this.CreateSut();

            // Act
            PoundContext dpo = await service.CreatePoundAsync("dog pound one");

            // Assert
            Assert.NotNull(dpo);
        }

        private IDogCatcherDataService CreateSut()
            => new DogCatcherDataService(this.fixture.Context);
    }
}