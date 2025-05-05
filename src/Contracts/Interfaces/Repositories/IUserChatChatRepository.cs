using Core;

namespace Contracts.Interfaces.Repositories;

public interface IUserChatRepository : IRepository<UserChat, (long ChatId, long UserId)>
{
    
}