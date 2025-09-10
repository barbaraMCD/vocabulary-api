using Microsoft.AspNetCore.Mvc;
using VocabularyAPI.DTOs;
using VocabularyAPI.Models;
using Microsoft.EntityFrameworkCore;
using VocabularyAPI.Data;  

namespace VocabularyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(AppDbContext context): ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await context.Users
            .Include(u => u.ProgressRecords)
            .ToListAsync();

        var userDtos = users.Select(user => new GetUsersDto
        {
            Email = user.Email,
            UserProgressIds = user.ProgressRecords
                .Select(up => up.Id)
                .ToList()
        });

        return Ok(userDtos);
    }
    
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDto>> GetUser(int userId)
    {
        var user = await context.Users
            .Include(u => u.ProgressRecords)
            .FirstOrDefaultAsync(u => u.Id == userId);
        
        if (user == null)
            return NotFound(new { message = "Utilisateur non trouvé" });
    
        return Ok(new UserDto
        {
            Email = user.Email,
            UserProgress = user.ProgressRecords
                .Select(up => new UserProgressDto
                {
                    UserId = up.UserId,
                    WordId = up.WordId,
                    Level = up.Level,
                    CorrectCount = up.CorrectCount
                })
                .ToList()
        });
    }
    
    [HttpGet("stats/{userId}")]
    public async Task<ActionResult<UserStatsDto>> GetStats(int userId)
    {
        // Vérifie que l'utilisateur existe
        var userExists = await context.Users.AnyAsync(u => u.Id == userId);
        if (!userExists)
            return NotFound("Utilisateur non trouvé");
    
        var totalWords = await context.Words.CountAsync();
    
        var userProgress = await context.UserProgress
            .Where(up => up.UserId == userId)  // ← Stats de cet utilisateur
            .ToListAsync();
    
        var wordsLearned = userProgress.Count(p => p.Level > 0);
        var wordsMastered = userProgress.Count(p => p.Level == 2);
    
        return Ok(new UserStatsDto
        {
            UserId = userId,  // On ajoute l'ID dans la réponse
            TotalWords = totalWords,
            WordsLearned = wordsLearned,
            WordsMastered = wordsMastered,
            PercentageComplete = totalWords > 0 ? (wordsMastered * 100) / totalWords : 0
        });
    }
}