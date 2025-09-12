using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using VocabularyAPI.Data;
using VocabularyAPI.Services;

namespace VocabularyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService, AppDbContext appDbContext) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = appDbContext.Users.SingleOrDefault(u => u.Email == request.Email);
        if (user == null)
        {
            return Unauthorized();
        }
        
        if (request.Email == user.Email && request.Password == user.PasswordHash)
        {
            var token = authService.GenerateJwtToken(request.Email);
            return Ok(new { token });
        }
        return Unauthorized();
    }
}