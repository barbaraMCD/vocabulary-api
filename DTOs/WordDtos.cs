namespace VocabularyAPI.DTOs;

public class CreateWordDto
{
    public string English { get; set; } = string.Empty;
    public string French { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}

public class UpdateWordDto
{
    public string English { get; set; } = string.Empty;
    public string French { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}