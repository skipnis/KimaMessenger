using Contracts.Dtos;
using Contracts.Interfaces.Repositories;
using Core;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChatRepository : Repository<Chat, long>, IChatRepository
{
    private readonly DbSet<Chat> _chats;
    
    public ChatRepository(ApplicationDbContext context) : base(context)
    {
        _chats = context.Set<Chat>();
    }

    public async Task<IEnumerable<ChatDto>> GetChatsByUserAsync(long userId, CancellationToken cancellationToken)
    {
        var chats = await _chats
            .Where(chat => chat.UserChats
                .Any(userChat => userChat.UserId == userId))
            .Include(chat => chat.UserChats)
            .ThenInclude(userChat => userChat.User) 
            .Include(chat => chat.Messages)
            .ToListAsync(cancellationToken);

        var chatDtos = chats.Select(chat =>
        {
            var contactName = chat.UserChats.FirstOrDefault(userChat => userChat.UserId != userId)?.User?.Username;
            
            var lastMessage = chat.Messages?.OrderByDescending(message => message.Timestamp).FirstOrDefault()?.Content ?? "No messages yet";

            return new ChatDto(chat.Id, contactName, lastMessage);
        });
        
        return chatDtos;
    }
    
}