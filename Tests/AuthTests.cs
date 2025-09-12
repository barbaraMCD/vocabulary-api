using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VocabularyAPI.Controllers;
using VocabularyAPI.Data;
using VocabularyAPI.Models;
using VocabularyAPI.Services;
using Xunit;

namespace VocabularyAPI.Tests;

public class AuthControllerTests
{
    [Fact]
    public void LoginValidCredentialsReturnsToken()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())  
            .Options;
        
        var dbContext = new AppDbContext(options);
        
        dbContext.Users.Add(new User 
        { 
            Email = "b@gmail.com", 
            PasswordHash = "test" 
        });
        dbContext.SaveChanges();
        
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json", optional: false)
            .Build();
        
        var authService = new AuthService(config);
        var controller = new AuthController(authService, dbContext);
        
        var result = controller.Login(new LoginRequest { Email = "b@gmail.com", Password = "test" });
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        
        var tokenProperty = okResult.Value.GetType().GetProperty("token");
        Assert.NotNull(tokenProperty);
    }
}