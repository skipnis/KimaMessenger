using Application.Commands;
using Application.Queries;
using Contracts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("for-user/{userId}")]
    public async Task<IActionResult> GetChatsByUser([FromRoute] long userId, CancellationToken cancellationToken)
    {
        var query = new GetChatsByUserQuery(userId);
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateChat(CreateChatDto dto)
    {
        var chatId = await _mediator.Send(new CreateChatCommand(dto));
        
        return Ok(new { chatId });
    }

    [HttpGet("secret")]
    [Authorize(AuthenticationSchemes = "KimaScheme")]
    public IActionResult GetSecretChats(CancellationToken cancellationToken)
    {
        return Ok("secret");
    }
}