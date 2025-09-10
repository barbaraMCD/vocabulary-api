using Microsoft.AspNetCore.Mvc;
using VocabularyAPI.DTOs;
using VocabularyAPI.Models;
using Microsoft.EntityFrameworkCore;
using VocabularyAPI.Data;  

namespace VocabularyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly AppDbContext _context;
    
    // Constructeur avec injection de dépendances
    public UserController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("stats/{userId}")]
    public async Task<ActionResult<UserStatsDto>> GetStats(int userId)
    {
        // Vérifie que l'utilisateur existe
        var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
        if (!userExists)
            return NotFound("Utilisateur non trouvé");
    
        var totalWords = await _context.Words.CountAsync();
    
        var userProgress = await _context.UserProgress
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