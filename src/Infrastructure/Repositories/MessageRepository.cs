using Contracts.Dtos;
using Contracts.Interfaces.Repositories;
using Core;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MessageRepository : Repository<Message, long>, IMessageRepository
{
    private readonly DbSet<Message> _messages;
    
    public MessageRepository(ApplicationDbContext context) : base(context)
    {
        _messages = context.Set<Message>();
    }

    public async Task<IEnumerable<MessageDto>> GetMessagesByChatAsync(long chatId, CancellationToken cancellationToken)
    {
        return await _messages
            .Where(m => m.ChatId == chatId)
            .OrderBy(m => m.Timestamp)
            .Include(m => m.Sender)
            .Select(m => new MessageDto(
                m.Id,
                m.ChatId,
                m.Sender.Username,
                m.Content,
                m.Timestamp
            ))
            .ToListAsync(cancellationToken);
    }

}