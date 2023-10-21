using System.ComponentModel.DataAnnotations;
using System.Text;
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
        public async Task Create_Dog()
        {
            // Arrange
            IDogCatcherDataService sut = this.CreateSut();

            // Act
            DogContext fido = await sut.CreateDogAsync("fido");

            // Assert
            Assert.NotNull(fido);
        }

        [Fact]
        public async Task Create_Catcher()
        {
            // Arrange
            IDogCatcherDataService sut = this.CreateSut();

            // Act
            CatcherContext gerald = await sut.CreateCatcherAsync("gerald");

            // Assert
            Assert.NotNull(gerald);
        }

        [Fact]
        public async Task Create_Pound()
        {
            // Arrange
            IDogCatcherDataService sut = this.CreateSut();

            // Act
            PoundContext dpo = await sut.CreatePoundAsync("dog pound one");

            // Assert
            Assert.NotNull(dpo);
        }

        [Fact]
        public async Task GetPounds()
        {
            // Arrange
            string ExpectedHouse = DateTime.UtcNow.Ticks.ToString();

            IDogCatcherDataService sut = this.CreateSut();

            await sut.CreatePoundAsync(ExpectedHouse);
            await sut.CreatePoundAsync("Carols House");
            await sut.CreatePoundAsync("Castle Wolfenstein");

            // Act
            IEnumerable<PoundContext> foundPounds = await sut.GetPoundsAsync(ExpectedHouse);

            // Assert
            Assert.NotNull(foundPounds);
            Assert.NotEmpty(foundPounds);
            Assert.Single(foundPounds);
        }

        private IDogCatcherDataService CreateSut()
            => new DogCatcherDataService(this.fixture.Context);
    }
}