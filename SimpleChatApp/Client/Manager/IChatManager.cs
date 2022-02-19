namespace SimpleChatApp.Client.Manager
{
    public interface IChatManager
    {
        Task<List<AppUser>> GetUsersAsync();
        Task SaveMessageAsync(Message message);
        Task<List<Message>> GetConversationAsync(long groupId);
        Task<AppUser> GetUserDetailsAsync(string userId);
    }
}