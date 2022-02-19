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

        public async Task<AppUser> GetUserDetailsAsync(string userId)
        {
            return await _httpClient.GetFromJsonAsync<AppUser>($"api/chat/users/{userId}");
        }

        public async Task<List<AppUser>> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AppUser>>("api/chat/users");
        }

        public async Task SaveMessageAsync(Message message)
        {
            await _httpClient.PostAsJsonAsync("api/chat", message);
        }
    }
}
