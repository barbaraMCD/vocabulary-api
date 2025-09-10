using Microsoft.AspNetCore.Mvc;
using VocabularyAPI.DTOs;
using VocabularyAPI.Models;

namespace VocabularyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PracticeController : ControllerBase
{
    private static readonly List<Word> Words = new()
    {
        new Word { Id = 1, English = "cat", French = "chat", Category = "animals" },
        new Word { Id = 2, English = "dog", French = "chien", Category = "animals"},
        new Word { Id = 3, English = "quickly", French = "rapidement", Category = "adverbs" }
    };

    private static readonly Dictionary<int, UserProgress> Progress = new();
    private static int _sessionId = 1;
    
    [HttpPost("answer")]
    public ActionResult<AnswerResultDto> CheckAnswer(AnswerDto answer)
    {
        var word = Words.FirstOrDefault(w => w.Id == answer.WordId);
        if (word == null)
        {
            return NotFound();
        }

        var isCorrect = word.French.Equals(answer.UserAnswer, StringComparison.OrdinalIgnoreCase);
        
        // Mettre à jour la progression (simplifié pour l'instant)
        if (!Progress.ContainsKey(answer.WordId))
        {
            Progress[answer.WordId] = new UserProgress { WordId = answer.WordId };
        }
        
        var progress = Progress[answer.WordId];
        progress.ReviewCount++;
        
        if (isCorrect)
        {
            progress.CorrectCount++;
            if (progress.CorrectCount >= 3 && progress.Level < 2)
            {
                progress.Level++;
            }
        }
        
        return Ok(new AnswerResultDto
        {
            IsCorrect = isCorrect,
            CorrectAnswer = isCorrect ? null : word.French,
            NewLevel = progress.Level,
            TotalReviews = progress.ReviewCount
        });
    }
}