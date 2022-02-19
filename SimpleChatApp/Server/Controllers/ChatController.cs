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
        public async Task<IActionResult> GetUserDetailsAsync(string userId)
        {
            var user = await _context.Users.Where(user => user.Id == userId).FirstOrDefaultAsync();
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> SaveMessageAsync(Message message)
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            message.FromId = userId;
            message.CreatedDate = DateTime.Now;
            message.To = await _context.ChatGroups.Where(group => group.Id == message.ToId).FirstOrDefaultAsync();
            await _context.Messages.AddAsync(message);
            return Ok(await _context.SaveChangesAsync());
        }
        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetConversationAsync(long groupId)
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            var messages = await _context.Messages
                    .Where(m => (m.ToId == groupId))
                    .OrderBy(a => a.CreatedDate)
                    .Include(a => a.From)
                    .Include(a => a.To)
                    .Select(x => new Message
                    {
                        From = x.From,
                        MessageText = x.MessageText,
                        CreatedDate = x.CreatedDate,
                        Id = x.Id,
                        ToId = x.ToId,
                        To = x.To,
                        FromId = x.FromId
                    }).ToListAsync();
            return Ok(messages);
        }
    }
}
