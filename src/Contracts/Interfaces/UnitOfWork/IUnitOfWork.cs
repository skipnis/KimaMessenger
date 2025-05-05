using Contracts.Interfaces.Repositories;

namespace Contracts.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    
    public IMessageRepository MessageRepository { get; }
    
    public IChatRepository ChatRepository { get; }
    
    public IUserChatRepository UserChatRepository { get; }
    
    public IContactRepository ContactRepository { get; } 
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}