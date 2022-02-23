using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SimpleChatApp.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<AppUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatGroup> Chats { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>(entity =>
            {
                entity.HasOne(message => message.From)
                    .WithMany(user => user.SentMessages)
                    .HasForeignKey(m => m.FromId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(message => message.Chat)
                    .WithMany(c => c.ChatMessages)
                    .HasForeignKey(m => m.ChatId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            builder.Entity<AppUser>(entity =>
            {
                entity.HasMany(u => u.Chats)
                    .WithMany(c => c.ChatUsers)
                    .UsingEntity("UserChats");
            });
        }
    }
}