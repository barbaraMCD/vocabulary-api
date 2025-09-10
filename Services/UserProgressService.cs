using Microsoft.EntityFrameworkCore;
using VocabularyAPI.Data;
using VocabularyAPI.Models;
using VocabularyAPI.Services.Interfaces;

namespace VocabularyAPI.Services;

public class UserProgressService(AppDbContext context) : IUserProgressService
{
    public async Task<UserProgress?> RetrieveUserProgressByWordIdAsync(int userId, int wordId)
    {
        return await context.UserProgress
            .FirstOrDefaultAsync(up => up.UserId == userId && up.WordId == wordId);
    }
}