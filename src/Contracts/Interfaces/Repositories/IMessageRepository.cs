using Contracts.Dtos;
using Core;

namespace Contracts.Interfaces.Repositories;

public interface IMessageRepository : IRepository<Message, long>
{
    Task<IEnumerable<MessageDto>> GetMessagesByChatAsync(long chatId, CancellationToken cancellationToken);
}