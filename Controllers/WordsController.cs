using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VocabularyAPI.Data;
using VocabularyAPI.DTOs;
using VocabularyAPI.Models;

namespace VocabularyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordsController(AppDbContext context, ILogger<WordsController> logger) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Word>>> GetWords()
    {
        var words = await context.Words.ToListAsync();
        return Ok(words);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Word>> GetWord(int id)
    {
        var word = await context.Words.FindAsync(id);
        
        if (word == null)
        {
            return NotFound(new { message = "Mot non trouvé" });
        }
        
        return Ok(word);
    }
    
    [Authorize]
    [HttpGet("category/{category}")]
    public ActionResult<IEnumerable<Word>> GetWordsByCategory(string category)
    {
        var words = context.Words
            .Where(w => w.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();
        
        return Ok(words);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Word>> CreateWord(CreateWordDto wordDto)
    {
        var word = new Word
        {
            English = wordDto.English,
            French = wordDto.French,
            Category = wordDto.Category
        };
        
        context.Words.Add(word);
        await context.SaveChangesAsync();
        
        logger.LogInformation($"Nouveau mot créé: {word.English}");
        
        return CreatedAtAction(nameof(GetWord), new { id = word.Id }, word);
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWord(int id, UpdateWordDto wordDto)
    {
        var word = await context.Words.FindAsync(id);
        
        if (word == null)
        {
            return NotFound(new { message = "Mot non trouvé" });
        }

        word.English = wordDto.English;
        word.French = wordDto.French;
        word.Category = wordDto.Category;
        
        await context.SaveChangesAsync();
        
        logger.LogInformation($"Mot modifié: ID={id}");

        return Ok(new { message = "Le mot a bien été modifié" });
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWord(int id)
    {
        var word = await context.Words.FindAsync(id);
        
        if (word == null)
        {
            return NotFound(new { message = "Mot non trouvé" });
        }

        context.Words.Remove(word);
        await context.SaveChangesAsync();
        
        logger.LogInformation($"Mot supprimé: {word.English}");
        
        return Ok(new { message = "Mot supprimé" });
    }
}