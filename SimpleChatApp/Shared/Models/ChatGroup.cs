namespace SimpleChatApp.Shared.Models
{
    public class ChatGroup
    {
        public long Id { get; set; }
        /// <summary>
        /// If its set to true then the chatusers max is 2
        /// </summary>
        public bool IsPrivate { get; set; }
        public virtual List<Message> ChatMessages { get; set; } = new List<Message>();
        public virtual List<AppUser> ChatUsers { get; set; } = new List<AppUser>();
    }
}
