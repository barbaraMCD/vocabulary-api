using System.Text.Json.Serialization;

namespace VocabularyAPI.DTOs;

public class AnswerDto
{
    public int WordId { get; set; }
    public string UserAnswer { get; set; } = string.Empty;
}

public class AnswerResultDto
{
    public bool IsCorrect { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
    public string? CorrectAnswer { get; set; }
    public int NewLevel { get; set; }
    public int TotalReviews { get; set; }
}

public class UserStatsDto
{
    public int UserId { get; set; }
    public int TotalWords { get; set; }
    public int WordsLearned { get; set; }
    public int WordsMastered { get; set; }
    public int PercentageComplete { get; set; }
}