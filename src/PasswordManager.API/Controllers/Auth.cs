using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.API.Core.Security.TokenServices;
using PasswordManager.API.Core.Services;
using PasswordManager.Persistence.Domain.Models.Requests;
using PasswordManager.Persistence.Domain.Models.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class Auth(IUserService userService, ITokenService tokenService) : ControllerBase
{
    /// <summary>
    ///     POST api/auth/login
    ///     Authenticate a user.
    /// </summary>
    /// <param name="request">The request body</param>
    [HttpPost("login")]
    [SwaggerResponse(StatusCodes.Status200OK, "User authenticated.", typeof(string))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "User not found or validation failed.")]
    public void Login([FromBody] AuthRequest request)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     POST api/auth/register
    ///     Register a new user.
    /// </summary>
    /// <param name="request">The request body</param>
    [HttpPost("register")]
    [SwaggerResponse(StatusCodes.Status201Created, "Registered successfully.", typeof(AuthResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Email already in use or validation failed.")]
    public async Task<ActionResult> Register([FromBody] AuthRequest request)
    {
        // Check if user already exists
        if (userService.AnyByEmailAsync(request.Email).Result)
        {
            return BadRequest($"Email already in use: {request.Email}");
        }

        // Add user to database
        var user = await userService.AddUserAsync(request);

        // Create and send response
        var token = new AuthResponse
        {
            Token = tokenService.CreateToken(user)
        };

        return Created($"/api/users/{user.Id}", token);
    }
}