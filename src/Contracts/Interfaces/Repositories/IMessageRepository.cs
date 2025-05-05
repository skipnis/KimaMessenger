using Contracts.Dtos;
using Core;

namespace Contracts.Interfaces.Repositories;

public interface IMessageRepository : IRepository<Message, long>
{
    Task<IEnumerable<MessageDto>> GetMessagesByChatAsync(long chatId, CancellationToken cancellationToken);
    
    Task<IEnumerable<MessageDto>> GetMessagesByChatIdPaginatedAsync(
        long chatId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);

    Task<int> GetTotalMessagesCountByChatIdAsync(long chatId, CancellationToken cancellationToken);
}