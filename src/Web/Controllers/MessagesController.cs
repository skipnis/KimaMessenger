using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{  
   private readonly IMediator _mediator;

   public MessagesController(IMediator mediator)
   {
      _mediator = mediator;
   }
   
   [HttpGet("for-chat/{chatId}")]
   public async Task<IActionResult> GetMessages([FromRoute] long chatId, CancellationToken cancellationToken)
   {
        var query = new GetMessagesByChatQuery(chatId);
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
   }
   
   [HttpGet("chat/{chatId}")]
   public async Task<IActionResult> GetMessagesByChat(
       [FromRoute] long chatId, 
       CancellationToken cancellationToken,
       [FromQuery] int pageNumber = 1, 
       [FromQuery] int pageSize = 10)
   {
       var query = new GetMessagesByChatPaginatedQuery(chatId, pageNumber, pageSize);

       var result = await _mediator.Send(query, cancellationToken);
       return Ok(result);
   }
}