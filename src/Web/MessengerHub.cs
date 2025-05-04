using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Web;

public class MessengerHub : Hub
{
    public async Task SendMessage(long chatId, string userName, string content)
    {
        var message = new
        {
            chatId,
            senderName = userName,
            content,
            timestamp = DateTime.UtcNow.ToString("o")
        };

        await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", message);
    }
    
    public async Task JoinChat(long chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }
}