using System.Security.Claims;
using Application.Commands;
using Contracts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto, CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(dto);
        
        var result = await _mediator.Send(command, cancellationToken);

        if (result)
        {
            return Ok("Registered Successfully");
        }
        return BadRequest("Registering Failed");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var command = new LoginCommand(dto);

        try
        {
            await _mediator.Send(command);
            return Ok("Login successful");
        }
        catch (Exception)
        {
            return Unauthorized("Invalid credentials");
        }
    }
    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("Kima", new CookieOptions
        {
            Path = "/",
            HttpOnly = true,
        });

        return Ok();
    }
    
    [HttpGet("current-user")]
    [Authorize(AuthenticationSchemes = "KimaScheme")]
    public IActionResult GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = User.FindFirst(ClaimTypes.Name)?.Value;

        return Ok(new { userId, username });
    }
}