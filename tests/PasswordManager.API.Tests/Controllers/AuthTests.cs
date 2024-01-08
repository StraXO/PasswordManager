using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PasswordManager.API.Controllers;
using PasswordManager.API.Core.Services;
using PasswordManager.Persistence.Domain.Models.Entities;
using PasswordManager.Persistence.Domain.Models.Requests;

namespace PasswordManager.API.Tests.Controllers;

public class AuthTests
{
    [Test]
    public async Task Register_ReturnsJwt()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var authController = new Mock<Auth>(mockUserService.Object);

        var authRequest = new AuthRequest
        {
            Email = "email@example.com",
            Password = "password"
        };
        
        // Act
        var result = await authController.Object.Register(authRequest);
        
        // Assert
        await result.ExecuteResultAsync(new ActionContext());
        mockUserService.Verify(x => x.AddUserAsync(authRequest), Times.Once);
        mockUserService.Verify(x => x.CreateToken(It.IsAny<User>()), Times.Once);
        
        Assert.That(result, Is.TypeOf<CreatedResult>());
    }
}