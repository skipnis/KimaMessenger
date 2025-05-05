using Contracts.Interfaces.Repositories;
using Core;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<User, long>, IUserRepository
{
    private readonly DbSet<User> _users;
    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _users = context.Set<User>();
    }

    public async Task<bool> ExistsAsync(string username, CancellationToken cancellationToken)
    {
        return await _users.AnyAsync(x => x.Username == username, cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await _users.FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
    }
}