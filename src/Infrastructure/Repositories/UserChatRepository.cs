using Contracts.Interfaces.Repositories;
using Core;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class UserChatRepository : Repository<UserChat, (long UserId, long ChatId)>, IUserChatRepository
{
    public UserChatRepository(ApplicationDbContext context) : base(context)
    {
        
    }

    public new async Task<UserChat?> GetByIdAsync((long ChatId, long UserId) id, CancellationToken cancellationToken)
    {
        return await base.GetByIdAsync((id.UserId, id.ChatId), cancellationToken);
    }
}