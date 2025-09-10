using Microsoft.EntityFrameworkCore;
using VocabularyAPI.Models;

namespace VocabularyAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Word> Words { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserProgress> UserProgress { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(e => e.Email)
            .IsUnique();
        
        modelBuilder.Entity<Word>().HasData(
            new Word { Id = 1, English = "cat", French = "chat", Category = "animals" },
            new Word { Id = 2, English = "dog", French = "chien", Category = "animals" }
        );
        
        modelBuilder.Entity<User>().HasData(
            new User { 
                Id = 1, 
                Email = "bb@gmail.com", 
                PasswordHash = "test"
            },
            new User { 
                Id = 2, 
                Email = "bbr@gmail.com", 
                PasswordHash = "test"
            }
        );
        
        modelBuilder.Entity<UserProgress>().HasData(
            new UserProgress { Id = 1, UserId = 1, WordId = 1, Level = 1, CorrectCount = 2 },
            new UserProgress { Id = 2, UserId = 1, WordId = 2, Level = 0, CorrectCount = 0 },
            new UserProgress { Id = 3, UserId = 2, WordId = 1, Level = 2, CorrectCount = 3 }
        );
    }
}