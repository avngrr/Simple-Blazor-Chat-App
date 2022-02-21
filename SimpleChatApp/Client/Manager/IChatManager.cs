namespace SimpleChatApp.Client.Manager
{
    public interface IChatManager
    {
        Task<List<AppUser>> GetUsersAsync();
        Task<ChatGroup> GetChatWithUserAsync(string userId);
        Task<List<Message>> GetConversationAsync(long groupId);
        Task<List<ChatGroup>> GetChatGroupsAsync();
        Task<ChatGroup> GetChatGroupDetailAsync(long groupId);
        Task SaveMessageAsync(Message message);
        Task CreateChatGroupAsync(ChatGroup chat);
    }
}