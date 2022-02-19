using Microsoft.AspNetCore.Identity;

namespace SimpleChatApp.Shared.Models
{
    public class AppUser : IdentityUser
    {
        public virtual List<Message> ChatMessagesFrom { get; set; } = new List<Message>();
        public virtual List<Message> ChatMessagesTo { get; set; } = new List<Message>();
    }
}
