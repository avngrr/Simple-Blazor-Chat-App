namespace SimpleChatApp.Shared.Models
{
    public class ChatGroup
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public virtual List<Message> ChatMessages { get; set; } = new List<Message>();
        public virtual List<AppUser> ChatUsers { get; set; } = new List<AppUser>();
    }
}
