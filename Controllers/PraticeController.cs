using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VocabularyAPI.Data;
using VocabularyAPI.DTOs;
using VocabularyAPI.Services;

namespace VocabularyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PracticeController(AppDbContext context, UserProgressService userProgressService) : ControllerBase
{
    [Authorize]
    [HttpPost("answer")]
    public async Task<ActionResult<AnswerResultDto>> CheckAnswer(AnswerDto answer)
    {
        var word = await context.Words.FindAsync(answer.WordId);
        
        if (word == null)
        {
            return NotFound();
        }

        var isCorrect = word.French.Equals(answer.UserAnswer, StringComparison.OrdinalIgnoreCase);
        
        var userProgress = await userProgressService.RetrieveUserProgressByWordIdAsync(1, answer.WordId);
        
        if (userProgress == null)
        {
            return NotFound();
        }
        
        if (isCorrect)
        {
            userProgress.CorrectCount++;
            if (userProgress.CorrectCount >= 3 && userProgress.Level < 2)
            {
                userProgress.Level++;
            }
        }
        
        return Ok(new AnswerResultDto
        {
            IsCorrect = isCorrect,
            CorrectAnswer = isCorrect ? null : word.French,
            NewLevel = userProgress.Level,
        });
    }
}