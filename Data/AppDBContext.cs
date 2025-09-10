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
    }
}