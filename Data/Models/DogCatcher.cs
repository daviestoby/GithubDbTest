namespace Data.Models
{
    public record CatcherContext(int Id, string Name);
    
    public record PoundContext(int Id, string Name);

    public record DogContext(int Id, string Name, DateTime? Dob, SystemState State, bool Captured = false);
}
