using System.ComponentModel.DataAnnotations;

namespace VocabularyAPI.Models;

public class User
{
    public int Id { get; set; }
    
    [Required, EmailAddress(ErrorMessage = "Invalid Email Address"), StringLength(100)]
    public string Email { get; set; } = string.Empty;
    
    [Required, StringLength(255)]
    public string PasswordHash { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<UserProgress> ProgressRecords { get; set; } = new List<UserProgress>();
}