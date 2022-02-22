using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleChatApp.Server.Data;
using System.Security.Claims;

namespace SimpleChatApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        public ChatController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            var allUsers = await _context.Users.Where(user => user.Id != userId).ToListAsync();
            return Ok(allUsers);
        }
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetChatWithUserAsync(string userId)
        {
            var currentUser = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            ChatGroup c = await _context.Chats.Where(c => c.IsPrivate && c.ChatUsers.Any(u => u.Id == userId) && c.ChatUsers.Any(u => u.Id == currentUser)).FirstOrDefaultAsync();
            if (c == null)
            {
                c = new()
                {
                    IsPrivate = true,
                    Name = userId,
                    StartedById = currentUser
                };
                c.ChatUsers.Add(await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync());
                c.ChatUsers.Add(await _context.Users.Where(u => u.Id == currentUser).FirstOrDefaultAsync());
                await _context.Chats.AddAsync(c);
                _context.SaveChanges();
            }
            return Ok(c);
        }
        [HttpGet("chatgroups/user/{userId}")]
        public async Task<IActionResult> GetChatGroupsFromUserAsync(string userId)
        {
            var allChatgroups = await _context.Chats
                .Where(cg => cg.ChatUsers.Any(u => u.Id == userId))
                .ToListAsync();
            return Ok(allChatgroups);
        }
        [HttpGet("chatgroups")]
        public async Task<IActionResult> GetChatGroupsAsync()
        {
            var allChatgroups = await _context.Chats.Where(cg => cg.IsPrivate == false).ToListAsync();
            return Ok(allChatgroups);
        }
        [HttpGet("chatgroups/{groupId}")]
        public async Task<IActionResult> GetChatGroupDetailAsync(long groupId)
        {
            var chatGroup = await _context.Chats.Where(group => group.Id == groupId).FirstOrDefaultAsync();
            return Ok(chatGroup);
        }
        [HttpPost]
        public async Task<IActionResult> SaveMessageAsync(Message message)
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            if (!message.Chat.ChatUsers.Any(u => u.Id == userId))
            {
                message.Chat.ChatUsers.Add(await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync());
            }
            message.FromId = userId;
            message.CreatedDate = DateTime.Now;
            if (message.Chat.Id != 0)
            {
                message.Chat = await _context.Chats.Where(group => group.Id == message.ChatId).FirstOrDefaultAsync();
            }
            foreach (AppUser user in message.Chat.ChatUsers)
            {
                if (user.Id != null && user.Id != string.Empty)
                {
                    _context.Entry(user).State = EntityState.Unchanged;
                }
            }
            await _context.Messages.AddAsync(message);
            return Ok(await _context.SaveChangesAsync());
        }
        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetConversationAsync(long groupId)
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            var messages = await _context.Messages
                    .Where(m => (m.ChatId == groupId))
                    .OrderBy(a => a.CreatedDate)
                    .Include(a => a.From)
                    .Include(a => a.Chat)
                    .Select(x => new Message
                    {
                        From = x.From,
                        MessageText = x.MessageText,
                        CreatedDate = x.CreatedDate,
                        Id = x.Id,
                        ChatId = x.ChatId,
                        Chat = x.Chat,
                        FromId = x.FromId
                    }).ToListAsync();
            return Ok(messages);
        }
        [HttpPost("chatgroup")]
        public async Task<IActionResult> CreateChatGroup(ChatGroup group)
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            group.StartedById = userId;
            AppUser user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            group.ChatUsers.Add(user);
            await _context.Chats.AddAsync(group);
            return Ok(await _context.SaveChangesAsync());
        }
    }
}
