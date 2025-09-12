using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VocabularyAPI.DTOs;
using VocabularyAPI.Data;  

namespace VocabularyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserProgressController(AppDbContext context): ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserProgressDto>>> GetUserProgresses()
    {
        var userProgresses = await context.UserProgress
            .ToListAsync();

        var userProgressDtos = userProgresses.Select(userProgress => new UserProgressDto
        {
            UserProgressId = userProgress.Id,
            UserId = userProgress.UserId,
            WordId = userProgress.WordId,
            Level = userProgress.Level,
            CorrectCount = userProgress.CorrectCount
        });

        return Ok(userProgressDtos);
    }
    
    [Authorize]
    [HttpGet("{userProgressId}")]
    public async Task<ActionResult<UserProgressDto>> GetUserProgress(int userProgressId)
    {
        var userProgress = await context.UserProgress
            .FindAsync(userProgressId);
        
        if (userProgress == null)
            return NotFound(new { message = "Progression utilisateur non trouvée" });
    
        return Ok(new UserProgressDto
        {
            UserProgressId = userProgress.Id,
            UserId = userProgress.UserId,
            WordId = userProgress.WordId,
            Level = userProgress.Level,
            CorrectCount = userProgress.CorrectCount
        });
    }
}