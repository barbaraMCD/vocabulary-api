namespace VocabularyAPI.Models;

public class UserProgress
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int WordId { get; set; }
    public int Level { get; set; } = 0; // 0: New, 1: Learning, 2: Mastered
    public int ReviewCount { get; set; } = 0;
    public int CorrectCount { get; set; } = 0;
    public DateTime LastReviewed { get; set; } = DateTime.UtcNow;
    public User User { get; set; } = null!;
    public Word Word { get; set; } = null!;
}