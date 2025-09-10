using Microsoft.AspNetCore.Mvc;
using VocabularyAPI.DTOs;
using VocabularyAPI.Models;

namespace VocabularyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordsController : ControllerBase
{
    private readonly ILogger<WordsController> _logger;

    public WordsController(ILogger<WordsController> logger)
    {
        _logger = logger;
    }
    // Liste temporaire en mémoire (on remplacera par la DB plus tard)
    private static readonly List<Word> Words = new()
    {
        new Word { Id = 1, English = "cat", French = "chat", Category = "animals" },
        new Word { Id = 2, English = "dog", French = "chien", Category = "animals" },
        new Word { Id = 3, English = "quickly", French = "rapidement", Category = "adverbs" }
    };
    
    [HttpGet]
    public ActionResult<IEnumerable<Word>> GetWords()
    {
        return Ok(Words);
    }
    
    [HttpGet("{id}")]
    public ActionResult<Word> GetWord(int id)
    {
        var word = Words.FirstOrDefault(w => w.Id == id);
        if (word == null)
        {
            return NotFound(new { message = "Mot non trouvé" });
        }
        return Ok(word);
    }
    
    [HttpGet("category/{category}")]
    public ActionResult<IEnumerable<Word>> GetWordsByCategory(string category)
    {
        var words = Words.Where(w => w.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        return Ok(words);
    }
    
    [HttpPost]
    public ActionResult<Word> CreateWord(CreateWordDto wordDto)
    {
        var word = new Word
        {
            Id = Words.Max(w => w.Id) + 1,
            English = wordDto.English,
            French = wordDto.French,
            Category = wordDto.Category
        };
        
        Words.Add(word);
        // ajoute dans le header location le lien vers la ressource créé, pratique pour le front
        return CreatedAtAction(nameof(GetWord), new { id = word.Id }, word);
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateWord(int id, UpdateWordDto wordDto)
    {
        var word = Words.FirstOrDefault(w => w.Id == id);
        if (word == null)
        {
            return NotFound();
        }

        word.English = wordDto.English;
        word.French = wordDto.French;
        word.Category = wordDto.Category;

        return Ok(new { message = "Le mot a bien été modifié" });
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteWord(int id)
    {
        var word = Words.FirstOrDefault(w => w.Id == id);
        if (word == null)
        {
            return NotFound();
        }

        Words.Remove(word);
        return Ok(new {message = "mot supprimé"});
    }
}