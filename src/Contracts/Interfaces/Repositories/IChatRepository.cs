using Contracts.Dtos;
using Core;

namespace Contracts.Interfaces.Repositories;

public interface IChatRepository : IRepository<Chat, long>
{
    Task<IEnumerable<ChatDto>> GetChatsByUserAsync(long userId, CancellationToken cancellationToken);
}