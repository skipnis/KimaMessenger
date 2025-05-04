using Contracts.Interfaces.Repositories;
using Core;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<User, long>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}