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

        private IDogCatcherDataService CreateSut()
            => new DogCatcherDataService(this.fixture.Context);
    }
}