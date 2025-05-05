using Contracts.Interfaces.UnitOfWork;
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Web;

public class MessengerHub : Hub
{
    private readonly IUnitOfWork _unitOfWork;

    public MessengerHub(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SendMessage(long chatId, string userName, string content)
    {
        var sender = await _unitOfWork.UserRepository
            .GetByUsernameAsync(userName, CancellationToken.None);

        if (sender == null)
        {
            throw new HubException("Пользователь не найден");
        }
        
        var message = new Message
        {
            ChatId = chatId,
            SenderId = sender.Id,
            Content = content,
            Timestamp = DateTime.UtcNow
        };

        await _unitOfWork.MessageRepository.CreateAsync(message, CancellationToken.None);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);

        var messageDto = new
        {
            chatId = message.ChatId,
            senderName = sender.Username,
            content = message.Content,
            timestamp = message.Timestamp.ToString("yyyy-MM-ddTHH:mm")
        };

        await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", messageDto);
    }
    
    public async Task JoinChat(long chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }
}