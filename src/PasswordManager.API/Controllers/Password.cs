using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.API.Core.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Password(IPasswordService passwordService) : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "Successful retrieved passwords", typeof(Password))]
    public async Task<ActionResult> GetPasswords()
    {
        var passwords = await passwordService.GetUserPasswordsAsync();

        return Ok(passwords);
    }

    [HttpGet("{id:long}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successful retrieved password")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden to access this password")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Password not found")]
    public async Task<ActionResult> GetPassword(long id)
    {
        var password = await passwordService.GetPasswordAsync(id);

        if (password == null)
        {
            return NotFound();
        }

        return Ok(password);
    }
}