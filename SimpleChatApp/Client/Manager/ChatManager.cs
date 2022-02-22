using System.Net.Http.Json;

namespace SimpleChatApp.Client.Manager
{
    public class ChatManager : IChatManager
    {
        private readonly HttpClient _httpClient;
        public ChatManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Message>> GetConversationAsync(long groupId)
        {
            return await _httpClient.GetFromJsonAsync<List<Message>>($"api/chat/{groupId}");
        }

        public async Task<ChatGroup> GetChatWithUserAsync(string userId)
        {
            return await _httpClient.GetFromJsonAsync<ChatGroup>($"api/chat/users/{userId}");
        }
        public async Task<List<ChatGroup>> GetChatGroupsFromUserAsync(string userId)
        {
            return await _httpClient.GetFromJsonAsync<List<ChatGroup>>($"api/chat/chatgroups/user/{userId}");
        }
        public async Task<List<ChatGroup>> GetChatGroupsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ChatGroup>>("api/chat/chatgroups");
        }
        public async Task<ChatGroup> GetChatGroupDetailAsync(long groupId)
        {
            return await _httpClient.GetFromJsonAsync<ChatGroup>($"api/chat/chatgroups/{groupId}");
        }
        public async Task<List<AppUser>> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AppUser>>("api/chat/users");
        }

        public async Task SaveMessageAsync(Message message)
        {
            await _httpClient.PostAsJsonAsync("api/chat", message);
        }

        public async Task CreateChatGroupAsync(ChatGroup chat)
        {
            await _httpClient.PostAsJsonAsync("api/chat/chatgroup", chat);
        }
    }
}
