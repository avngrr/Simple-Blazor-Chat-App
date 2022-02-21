using Microsoft.AspNetCore.SignalR;

namespace SimpleChatApp.Server
{
    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(Message message, string groupName)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, groupName);
        }
        public async Task ChatNotificationAsync(string message, long groupId, string senderUserId)
        {
            await Clients.All.SendAsync("ReceiveChatNotification", message, groupId, senderUserId);
        }
    }
}
