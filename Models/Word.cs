using System.ComponentModel.DataAnnotations;

namespace VocabularyAPI.Models;

public class Word
{
    public int Id { get; set; }
    
    [StringLength(50)]
    public string English { get; set; } = string.Empty;
    [StringLength(50)]
    public string French { get; set; } = string.Empty;
    [StringLength(50)]
    public string Category { get; set; } = string.Empty;
}