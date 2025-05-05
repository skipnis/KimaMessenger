using Core;

namespace Contracts.Interfaces.Repositories;

public interface IUserRepository : IRepository<User, long>
{
    Task<bool> ExistsAsync(string username, CancellationToken cancellationToken);
    
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
}