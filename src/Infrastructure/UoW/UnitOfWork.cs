using Contracts.Interfaces.Repositories;
using Contracts.Interfaces.UnitOfWork;
using Infrastructure.Context;

namespace Infrastructure.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository, IMessageRepository messageRepository, IChatRepository chatRepository, IUserChatRepository userChatRepository)
    {
        _context = context;
        UserRepository = userRepository;
        MessageRepository = messageRepository;
        ChatRepository = chatRepository;
        UserChatRepository = userChatRepository;
    }

    public IUserRepository UserRepository { get; }
    public IMessageRepository MessageRepository { get; }
    public IChatRepository ChatRepository { get; }
    public IUserChatRepository UserChatRepository { get; }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}