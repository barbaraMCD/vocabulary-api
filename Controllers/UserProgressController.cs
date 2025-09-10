using Microsoft.AspNetCore.Mvc;
using VocabularyAPI.DTOs;
using VocabularyAPI.Data;  

namespace VocabularyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserProgressController(AppDbContext context): ControllerBase
{
    [HttpGet("{userProgressId}")]
    public async Task<ActionResult<UserProgressDto>> GetUserProgress(int userProgressId)
    {
        var userProgress = await context.UserProgress
            .FindAsync(userProgressId);
        
        if (userProgress == null)
            return NotFound(new { message = "Progression utilisateur non trouvée" });
    
        return Ok(new UserProgressDto
        {
            UserId = userProgress.UserId,
            WordId = userProgress.WordId,
            Level = userProgress.Level,
            CorrectCount = userProgress.CorrectCount
        });
    }
}