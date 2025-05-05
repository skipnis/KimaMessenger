using Core;

namespace Contracts.Interfaces.Repositories;

public interface IContactRepository : IRepository<Contact, long>
{
    Task<IEnumerable<User>> GetContactsByUserIdAsync(long userId, CancellationToken cancellationToken);
    Task AddContactAsync(long userId, long contactUserId, CancellationToken cancellationToken);
    Task<bool> ContactExistsAsync(long userId, long contactUserId, CancellationToken cancellationToken);
}