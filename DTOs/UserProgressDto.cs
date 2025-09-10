namespace VocabularyAPI.DTOs;

public class UserProgressDto
{
        public int WordId { get; set; }
        public int UserId { get; set; }
        public int Level { get; set; }
        public int CorrectCount { get; set; }
}