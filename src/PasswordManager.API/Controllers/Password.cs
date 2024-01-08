using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.API.Core.Services;
using PasswordManager.Persistence.Domain.Models.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.API.Controllers;

/// <summary>
///     Controller responsible for the password endpoints
/// </summary>
/// <param name="passwordService">The <see cref="IPasswordService"/> that handles the business logic for the <see cref="Password"/> class</param>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class Password(IPasswordService passwordService) : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(IEnumerable<PasswordResponse>))]
    public async Task<ActionResult> GetPasswords()
    {
        var passwords = await passwordService.FindUserPasswordsAsync();

        return Ok(passwords);
    }

    [HttpGet("{id:long}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(PasswordResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Password not found")]
    public async Task<ActionResult> GetPassword(long id)
    {
        var password = await passwordService.FindPasswordAsync(id);

        if (password == null)
        {
            return BadRequest();
        }

        return Ok(password);
    }
}