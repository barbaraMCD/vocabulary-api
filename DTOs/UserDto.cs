namespace VocabularyAPI.DTOs;

public class UserDto
{
    public string Email { get; set; } = string.Empty;
    
    public List<UserProgressDto> UserProgress { get; set; } = new();
}

public class GetUsersDto
{
    public string Email { get; set; } = string.Empty;
    public List<int> UserProgressIds { get; set; } = new();
}