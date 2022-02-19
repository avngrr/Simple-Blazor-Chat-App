namespace SimpleChatApp.Shared.Models
{
    public class Message
    {
        public long Id { get; set; }
        public string FromId { get; set; }
        public long ToId { get; set; }
        public string MessageText { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public virtual AppUser From { get; set; }
        public virtual ChatGroup To { get; set; }
    }
}
