﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.API.Core.Security;
using PasswordManager.API.Core.Services;
using PasswordManager.Persistence.Domain.Models.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class Password(IPasswordService passwordService) : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "Successful retrieved passwords", typeof(IEnumerable<PasswordResponse>))]
    public async Task<ActionResult> GetPasswords()
    {
        var passwords = await passwordService.GetUserPasswordsAsync();

        return Ok(passwords);
    }

    [HttpGet("{id:long}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successful retrieved password", typeof(PasswordResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Password not found or not owned by user")]
    public async Task<ActionResult> GetPassword(long id)
    {
        var password = await passwordService.GetPasswordAsync(id);

        if (password == null)
        {
            return BadRequest();
        }

        return Ok(password);
    }
}