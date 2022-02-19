using Microsoft.AspNetCore.SignalR;

namespace SimpleChatApp.Server
{
    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(Message message, string userName)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, userName);
        }
        public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId)
        {
            await Clients.All.SendAsync("ReceiveChatNotification", message, receiverUserId, senderUserId);
        }
    }
}
