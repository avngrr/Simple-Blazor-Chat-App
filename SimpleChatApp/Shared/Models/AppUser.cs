using Microsoft.AspNetCore.Identity;

namespace SimpleChatApp.Shared.Models
{
    public class AppUser : IdentityUser
    {
        public virtual List<Message> SentMessages { get; set; } = new List<Message>();
        public virtual List<ChatGroup> Chats { get; set; } = new List<ChatGroup>();
        public virtual List<ChatGroup> StartedChats { get; set; } = new List<ChatGroup>();
    }
}
