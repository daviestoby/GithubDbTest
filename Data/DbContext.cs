using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DogCatcherContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }

        public DbSet<DogCatcher> Catchers { get; set; }

        public DbSet<Pound> Pounds { get; set; }

        public DogCatcherContext(DbContextOptions<DogCatcherContext> options) : base(options)
        {
        }
    }

    public enum SystemState
    {
        Unknown = 0,
        AtLarge = 1,
        Apprehended = 2
    }

    [Table("Dog")]
    public class Dog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Name { get; set; }

        public DateTime? DoB { get; set; }

        public SystemState Status { get; set; }

        public Pound? Pound { get; set; }
    }

    [Table("Catcher")]
    public class DogCatcher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Name { get; set; }
    }

    [Table("Pound")]
    public class Pound
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Name { get; set; }

        public ICollection<Dog> Dogs { get; set; } = new HashSet<Dog>();
    }


}