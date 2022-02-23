using Microsoft.AspNetCore.SignalR;

namespace SimpleChatApp.Server
{
    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(Message message, string senderName)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, senderName);
        }
        public async Task ChatNotificationAsync(string message, ChatGroup chat, string senderName, string senderUserId)
        {
            await Clients.All.SendAsync("ReceiveChatNotification", message, chat, senderName, senderUserId);
        }
    }
}
