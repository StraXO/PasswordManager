using Microsoft.AspNetCore.Mvc;
using PasswordManager.API.Core.Services;
using PasswordManager.Persistence.Domain.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth(IUserService userService) : ControllerBase
    {
        /// <summary>
        ///     POST api/auth/login
        ///     Authenticate a user.
        /// </summary>
        /// <param name="request">The request body</param>
        [HttpPost("login")]
        [SwaggerResponse(StatusCodes.Status200OK, "User authenticated.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "User not found or validation failed.")]
        public void Login([FromBody] UserAuthenticationRequest request)
        {
            // TODO: Implement
            throw new NotImplementedException();
        }
        
        /// <summary>
        ///     POST api/auth/register
        ///     Register a new user.
        /// </summary>
        /// <param name="request">The request body</param>
        [HttpPost("register")]
        [SwaggerResponse(StatusCodes.Status201Created, "Registered successfully.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Email already in use or validation failed.")]
        public async Task<ActionResult>Register([FromBody] UserAuthenticationRequest request)
        {
            // Check if user already exists
            if (userService.AnyByEmailAsync(request.Email).Result)
            {
                return BadRequest($"Email already in use: {request.Email}");
            }
            
            var user = await userService.AddUserAsync(request);

            // TODO: Return JWT token
            return Created($"/api/users/{user.Id}", "Registered successfully.");
        }
    }
}
