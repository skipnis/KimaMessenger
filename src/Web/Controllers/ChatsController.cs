using Application.Queries;
using MediatR;
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
}