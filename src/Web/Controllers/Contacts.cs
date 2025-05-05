using Contracts.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetContacts(long userId, CancellationToken cancellationToken)
    {
        var contacts = await _unitOfWork.ContactRepository.GetContactsByUserIdAsync(userId, cancellationToken);
        var result = contacts.Select(u => new
        {
            u.Id,
            u.Username
        });

        return Ok(result);
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> AddContact(long userId, [FromBody] string contactUsername, CancellationToken cancellationToken)
    {
        var contactUser = await _unitOfWork.UserRepository.GetByUsernameAsync(contactUsername, cancellationToken);
        if (contactUser == null)
            return NotFound("Пользователь не найден");

        if (await _unitOfWork.ContactRepository.ContactExistsAsync(userId, contactUser.Id, cancellationToken))
            return BadRequest("Контакт уже существует");

        await _unitOfWork.ContactRepository.AddContactAsync(userId, contactUser.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}