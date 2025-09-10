using VocabularyAPI.Models;

namespace VocabularyAPI.Services.Interfaces;

public interface IUserProgressService
{
    Task<UserProgress?> RetrieveUserProgressByWordIdAsync(int userId, int wordId);
}